namespace Footballers.DataProcessor
{
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Footballers.DataProcessor.ExportDto;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {
            var coaches = context.Coaches
                .Where(x => x.Footballers.Any())
                .ToArray()
                .Select(x => new ExportCoachDTO()
                {
                    Name = x.Name,
                    FootballersCount = x.Footballers.Count,
                    Footballers = x.Footballers
                        .ToArray()
                        .Select(x => new ExportFootballersByCoachesDTO()
                        {
                            Name = x.Name,
                            PositionType = x.PositionType.ToString()
                        })
                        .OrderBy(x => x.Name)
                        .ToArray()
                })
                .OrderByDescending(x => x.Footballers.Length)
                .ThenBy(x => x.Name)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportCoachDTO[]), new XmlRootAttribute("Coaches"));

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            StringBuilder sb = new StringBuilder();
            using StringWriter writer = new StringWriter(sb);
            serializer.Serialize(writer, coaches, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string ExportTeamsWithMostFootballers(FootballersContext context, DateTime date)
        {
            var teams = context.Teams
                .Where(x => x.TeamsFootballers.Any(f => f.Footballer.ContractStartDate.Date >= date.Date))
                .ToArray()
                .Select(x => new ExportTeamsDTO()
                {
                    Name = x.Name,
                    Footballers = x.TeamsFootballers
                        .Where(x => x.Footballer.ContractStartDate.Date >= date.Date)
                        .ToArray()
                        .OrderByDescending(x => x.Footballer.ContractEndDate)
                        .ThenBy(x => x.Footballer.Name)
                        .Select(f => new ExportFootballersDTO()
                        {
                            FootballerName = f.Footballer.Name,
                            ContractStartDate = f.Footballer.ContractStartDate.ToString("d", CultureInfo.InvariantCulture),
                            ContractEndDate = f.Footballer.ContractEndDate.ToString("d", CultureInfo.InvariantCulture),
                            BestSkillType = f.Footballer.BestSkillType.ToString(),
                            PositionType = f.Footballer.PositionType.ToString()
                        })
                        .ToArray()
                })
                .OrderByDescending(x => x.Footballers.Length)
                .ThenBy(x => x.Name)
                .Take(5)
                .ToArray();

            return JsonConvert.SerializeObject(teams, Formatting.Indented);
        }
    }
}
