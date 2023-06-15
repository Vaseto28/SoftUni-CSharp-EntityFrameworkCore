// ReSharper disable InconsistentNaming

namespace TeisterMask.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    using Data;
    using System.Text;
    using System.Xml.Serialization;
    using TeisterMask.DataProcessor.ImportDto;
    using TeisterMask.Data.Models;
    using System.Globalization;
    using TeisterMask.Data.Models.Enums;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            StringBuilder result = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(ImportProjectsDTO[]), new XmlRootAttribute("Projects"));

            using StringReader reader = new StringReader(xmlString);
            var dtos = (ImportProjectsDTO[])serializer.Deserialize(reader);

            List<Project> projects = new List<Project>();
            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime projectOpenDateTemp = new DateTime();
                DateTime projectDueDateTemp = new DateTime();

                bool isProjectOpenDateValid = DateTime.TryParseExact(dto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out projectOpenDateTemp);
                bool isProjectDueDateValid = DateTime.TryParseExact(dto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out projectDueDateTemp);

                if (!isProjectOpenDateValid)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                List<Task> tasks = new List<Task>();
                foreach (var task in dto.Tasks)
                {
                    if (!IsValid(task))
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime taskOpenDateTemp = new DateTime();
                    DateTime taskDueDateTemp = new DateTime();

                    bool isTaskOpenDateValid = DateTime.TryParseExact(task.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out taskOpenDateTemp);
                    bool isTaskDueDateValid = DateTime.TryParseExact(task.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out taskDueDateTemp);

                    if (!isTaskOpenDateValid || !isTaskDueDateValid)
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (projectDueDateTemp.Date != default(DateTime))
                    {
                        if (taskOpenDateTemp.Date < projectOpenDateTemp.Date || taskDueDateTemp.Date > projectDueDateTemp.Date)
                        {
                            result.AppendLine(ErrorMessage);
                            continue;
                        }
                    }
                    else
                    {
                        if (taskOpenDateTemp.Date < projectOpenDateTemp.Date)
                        {
                            result.AppendLine(ErrorMessage);
                            continue;
                        }
                    }

                    Task validTask = new Task()
                    {
                        Name = task.Name,
                        OpenDate = taskOpenDateTemp,
                        DueDate = taskDueDateTemp,
                        ExecutionType = (ExecutionType)task.ExecutionType,
                        LabelType = (LabelType)task.LabelType,
                    };

                    tasks.Add(validTask);
                }

                Project project = new Project()
                {
                    Name = dto.Name,
                    OpenDate = projectOpenDateTemp,
                    DueDate = projectDueDateTemp,
                    Tasks = tasks
                };

                projects.Add(project);
                result.AppendLine(string.Format(SuccessfullyImportedProject, project.Name, project.Tasks.Count));
            }

            context.Projects.AddRange(projects);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            StringBuilder result = new StringBuilder();

            var dtos = JsonConvert.DeserializeObject<ImportEmployeesDTO[]>(jsonString);

            List<Employee> employees = new List<Employee>();
            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                List<EmployeeTask> employeeTasks = new List<EmployeeTask>();
                foreach (var taskId in dto.Tasks.Distinct())
                {
                    Task task = context.Tasks.Find(taskId);
                    if (task == null)
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }

                    EmployeeTask employeeTask = new EmployeeTask()
                    {
                        Task = task
                    };

                    employeeTasks.Add(employeeTask);
                }

                Employee employee = new Employee()
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    Phone = dto.PhoneNumber,
                    EmployeesTasks = employeeTasks
                };

                employees.Add(employee);
                result.AppendLine(string.Format(SuccessfullyImportedEmployee, employee.Username, employee.EmployeesTasks.Count));
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}