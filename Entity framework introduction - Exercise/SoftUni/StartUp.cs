using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;
using System.Text;

namespace SoftUni;

public class StartUp
{
    public static void Main(string[] args)
    {
        SoftUniContext dbCtx = new SoftUniContext();

        Console.WriteLine(RemoveTown(dbCtx));
    }

    //Task 3
    public static string GetEmployeesFullInformation(SoftUniContext context)
    {
        var employees = context.Employees
            .OrderBy(e => e.EmployeeId)
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.MiddleName,
                e.JobTitle,
                e.Salary
            })
            .ToArray();

        StringBuilder result = new StringBuilder();

        foreach (var e in employees)
        {
            result.AppendLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}");
        }

        return result.ToString().Trim();
    }

    //Task 4
    public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
    {
        var employees = context.Employees
            .AsNoTracking()
            .Where(x => x.Salary > 50000)
            .OrderBy(x => x.FirstName)
            .Select(x => new
            {
                x.FirstName,
                x.Salary
            })
            .ToArray();

        StringBuilder result = new StringBuilder();

        foreach (var e in employees)
        {
            result.AppendLine($"{e.FirstName} - {e.Salary:f2}");
        }

        return result.ToString().Trim();
    }

    //Task 5
    public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
    {
        var employees = context.Employees
            .AsNoTracking()
            .OrderBy(x => x.Salary)
            .ThenByDescending(x => x.FirstName)
            .Where(x => x.Department.Name == "Research and Development")
            .Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.Salary
            })
            .ToArray();

        StringBuilder sb = new StringBuilder();
        foreach (var e in employees)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} from Research and Development - ${e.Salary:f2}");
        }

        return sb.ToString().Trim();
    }

    //Task 6
    public static string AddNewAddressToEmployee(SoftUniContext context)
    {
        Address address = new Address()
        {
            AddressText = "Vitoshka 15",
            TownId = 4
        };

        context.Addresses.Add(address);
        context.SaveChanges();

        var employee = context.Employees
            .Where(x => x.LastName == "Nakov")
            .ToArray()[0];

        employee.AddressId = address.AddressId;
        employee.Address = address;

        context.SaveChanges();

        var adressTexts = context.Employees
            .AsNoTracking()
            .OrderByDescending(x => x.AddressId)
            .Take(10)
            .Select(x => x.Address!.AddressText)
            .ToArray();

        StringBuilder sb = new StringBuilder();
        foreach (var a in adressTexts)
        {
            sb.AppendLine(a);
        }

        return sb.ToString().Trim();
    }

    //Task 7
    public static string GetEmployeesInPeriod(SoftUniContext context)
    {
        var employees = context.Employees
            .Take(10)
            .Select(x => new
            {
                x.FirstName,
                x.LastName,
                ManagerFirstName = x.Manager!.FirstName,
                ManagerLastName = x.Manager!.LastName,
                Projects = x.EmployeesProjects
                    .Select(x => new
                    {
                        ProjectName = x.Project.Name,
                        StartDate = x.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt"),
                        EndDate = x.Project.EndDate.HasValue ?
                            x.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt") : "not finished"
                    })
            })
            .ToArray();

        StringBuilder sb = new StringBuilder();
        foreach (var e in employees)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} - Manager: {e.ManagerFirstName} {e.ManagerLastName}");

            foreach (var p in e.Projects)
            {
                DateTime startDateComparer = new DateTime(2001, 1, 1);
                DateTime endDateComparer = new DateTime(2003, 12, 12);

                if (DateTime.Parse(p.StartDate) >= startDateComparer && DateTime.Parse(p.StartDate) <= endDateComparer)
                {
                    sb.AppendLine($"--{p.ProjectName} - {p.StartDate} - {p.EndDate}");
                }
            }
        }

        return sb.ToString().Trim();
    }

    //Task 8
    public static string GetAddressesByTown(SoftUniContext context)
    {
        var addresses = context.Addresses
            .OrderByDescending(x => x.Employees.Count)
            .ThenBy(x => x.Town!.Name)
            .ThenBy(x => x.AddressText)
            .Select(x => new
            {
                x.AddressText,
                TownName = x.Town.Name,
                EmployeesCount = x.Employees.Count
            })
            .Take(10)
            .ToArray();

        StringBuilder sb = new StringBuilder();
        foreach (var a in addresses)
        {
            sb.AppendLine($"{a.AddressText}, {a.TownName} - {a.EmployeesCount} employees");
        }

        return sb.ToString().Trim();
    }

    //Task 9
    public static string GetEmployee147(SoftUniContext context)
    {
        var employee = context.Employees
            .Find(147);

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"{employee?.FirstName} {employee?.LastName} - {employee.JobTitle}");
        foreach (var p in employee.EmployeesProjects
            .OrderBy(x => x.Project.Name)
            .Select(x => new
            {
                ProjectName = x.Project.Name
            }).ToArray())
        {
            sb.AppendLine(p.ProjectName);
        }

        return sb.ToString().Trim();
    }

    //Task 10
    public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
    {
        var departments = context.Departments
            .Where(x => x.Employees.Count > 5)
            .OrderBy(x => x.Employees.Count)
            .ThenBy(x => x.Name)
            .Select(x => new
            {
                DepartmentName = x.Name,
                ManagerName = x.Manager.FirstName + " " + x.Manager.LastName,
                Employees = x.Employees
            })
            .ToArray();

        StringBuilder sb = new StringBuilder();
        foreach (var d in departments)
        {
            sb.AppendLine($"{d.DepartmentName} - {d.ManagerName}");

            if (d.Employees != null)
            {
                foreach (var e in d.Employees
                    .OrderBy(x => x.FirstName)
                    .ThenBy(x => x.LastName))
                {
                    sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                }
            }
        }

        return sb.ToString().Trim();
    }

    //Task 11
    public static string GetLatestProjects(SoftUniContext context)
    {
        var projects = context.Projects
            .OrderByDescending(x => x.StartDate)
            .Take(10)
            .OrderBy(x => x.Name)
            .Select(x => new
            {
                x.Name,
                x.Description,
                StartDate = x.StartDate.ToString("M/d/yyyy h:mm:ss tt")
            })
            .ToArray();

        StringBuilder sb = new StringBuilder();
        foreach (var p in projects)
        {
            sb.AppendLine($"{p.Name}\n{p.Description}\n{p.StartDate}");
        }

        return sb.ToString().Trim();
    }

    //Task 12
    public static string IncreaseSalaries(SoftUniContext context)
    {
        var employees = context.Employees
            .Where(x => x.Department.Name == "Engineering" || x.Department.Name == "Tool Design" || x.Department.Name == "Marketing" || x.Department.Name == "Information Services")
            .OrderBy(x => x.FirstName)
            .ThenBy(x => x.LastName)
            .ToArray();

        List<Employee> updatedEmployees = new List<Employee>();
        foreach (var e in employees)
        {
            e.Salary += e.Salary * 0.12m;
            updatedEmployees.Add(e);

            context.SaveChanges();
        }

        StringBuilder sb = new StringBuilder();
        foreach (var e in updatedEmployees)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:f2})");
        }

        return sb.ToString().Trim();
    }

    //Task 13
    public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
    {
        var employees = context.Employees
            .Where(x => x.FirstName.StartsWith("Sa"))
            .OrderBy(x => x.FirstName)
            .ThenBy(x => x.LastName)
            .Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.JobTitle,
                x.Salary
            })
            .ToArray();

        StringBuilder sb = new StringBuilder();
        foreach (var e in employees)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})");
        }

        return sb.ToString().Trim();
    }

    //Task 14
    public static string DeleteProjectById(SoftUniContext context)
    {
        var projectToDelete = context.EmployeesProjects
            .Where(x => x.ProjectId == 2);

        context.EmployeesProjects.RemoveRange(projectToDelete);

        var projectToRemove = context.Projects.Find(2);

        context.Projects.Remove(projectToRemove!);

        context.SaveChanges();

        var projects = context.Projects
            .Select(x => x.Name)
            .Take(10);

        StringBuilder sb = new StringBuilder();
        foreach (var pn in projects)
        {
            sb.AppendLine(pn);
        }

        return sb.ToString().Trim();
    }

    //Task 15
    public static string RemoveTown(SoftUniContext context)
    {
        var employeesAddressesToDelete = context.Employees
            .Where(x => x.Address.Town.Name == "Seattle");

        foreach (var e in employeesAddressesToDelete)
        {
            e.AddressId = null;
        }

        var addressesToDelete = context.Addresses
            .Where(x => x.Town!.Name == "Seattle");

        var countBeforeDeleting = addressesToDelete.ToArray().Length;

        context.Addresses.RemoveRange(addressesToDelete);

        var countAfterDeleting = addressesToDelete.ToArray().Length;

        var townToDelete = context.Towns
            .Where(x => x.Name == "Seattle");

        context.Towns.RemoveRange(townToDelete);

        context.SaveChanges();

        return $"{countBeforeDeleting - countAfterDeleting} addresses in Seattle were deleted";
    }
}