namespace Footballers.DataProcessor
{
    using Footballers.Data;
    using Footballers.Data.Models;
    using Footballers.Data.Models.Enums;
    using Footballers.DataProcessor.ImportDto;
    using Microsoft.Extensions.Primitives;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";

        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";

        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            StringBuilder result = new StringBuilder();
            
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCoachesDTO[]), new XmlRootAttribute("Coaches"));

            using StringReader stringReader = new StringReader(xmlString);
            var dtos = (ImportCoachesDTO[])xmlSerializer.Deserialize(stringReader);

            List<Coach> coaches = new List<Coach>();
            foreach (var dto in dtos)
            {
                if (!IsValid(dto)/* || string.IsNullOrEmpty(dto.Name)*/ || string.IsNullOrEmpty(dto.Nationality))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                List<Footballer> footballers = new List<Footballer>();
                foreach (var footballer in dto.Footballers)
                {
                    if (!IsValid(footballer))
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }

                    CultureInfo info = CultureInfo.InvariantCulture;
                    DateTimeStyles styles = new DateTimeStyles();

                    bool isContractStartDateValid = DateTime.TryParseExact(footballer.ContractStartDate, "dd/MM/yyyy", info, styles, out DateTime validStartContractDate);
                    bool isContractEndDateValid = DateTime.TryParseExact(footballer.ContractEndDate, "dd/MM/yyyy", info, styles, out DateTime validEndContractDate);

                    if (!isContractStartDateValid || !isContractEndDateValid || validEndContractDate.Date < validStartContractDate.Date)
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }

                    Footballer validFootballer = new Footballer()
                    {
                        Name = footballer.Name,
                        ContractStartDate = DateTime.ParseExact(footballer.ContractStartDate, "dd/MM/yyyy", info),
                        ContractEndDate = DateTime.ParseExact(footballer.ContractEndDate, "dd/MM/yyyy", info),
                        BestSkillType = (BestSkillType)footballer.BestSkillType,
                        PositionType = (PositionType)footballer.PositionType
                    };

                    footballers.Add(validFootballer);
                }

                Coach coach = new Coach()
                {
                    Name = dto.Name,
                    Nationality = dto.Nationality,
                    Footballers = footballers
                };

                coaches.Add(coach);
                result.AppendLine(string.Format(SuccessfullyImportedCoach, coach.Name, footballers.Count));
            }

            context.Coaches.AddRange(coaches);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {
            StringBuilder result = new StringBuilder();
            var dtos = JsonConvert.DeserializeObject<ImportTeamsDTO[]>(jsonString);

            List<Team> teams = new List<Team>();
            foreach (var dto in dtos)
            {
                if (dto.Name == "Montpellier Herault SC")
                {
                    Console.WriteLine();
                }

                if (!IsValid(dto) || dto.Trophies <= 0)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                List<TeamFootballer> footballers = new List<TeamFootballer>();
                foreach (var footballerId in dto.Footballers.Distinct())
                {
                    Footballer footballer = context.Footballers.Find(footballerId);
                    if (footballer == null)
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }


                    TeamFootballer teamFootballer = new TeamFootballer()
                    {
                        Footballer = footballer
                    };

                    footballers.Add(teamFootballer);
                }

                Team team = new Team()
                {
                    Name = dto.Name,
                    Nationality = dto.Nationality,
                    Trophies = dto.Trophies,
                    TeamsFootballers = footballers
                };

                teams.Add(team);
                result.AppendLine(string.Format(SuccessfullyImportedTeam, team.Name, team.TeamsFootballers.Count));
            }

            context.Teams.AddRange(teams);
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
