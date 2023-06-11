namespace Theatre.DataProcessor
{

    using System;
    using System.Text;
    using System.Xml.Serialization;
    using Newtonsoft.Json;
    using Theatre.Data;
    using Theatre.DataProcessor.ExportDto;

    public class Serializer
    {
        public static string ExportTheatres(TheatreContext context, int numbersOfHalls)
        {
            var theatres = context.Theatres
                .Where(x => x.NumberOfHalls >= numbersOfHalls && x.Tickets.Count >= 20)
                .ToArray()
                .Select(x => new ExportTheatresDTO()
                {
                    Name = x.Name,
                    Halls = x.NumberOfHalls,
                    TotalIncome = x.Tickets
                        .Where(x => x.RowNumber >= 1 && x.RowNumber <= 5)
                        .ToArray()
                        .Sum(x => x.Price),
                    Tickets = x.Tickets
                        .Where(x => x.RowNumber >= 1 && x.RowNumber <= 5)
                        .ToArray()
                        .Select(x => new ExportTicketsDTO()
                        {
                            Price = decimal.Parse($"{x.Price:f2}"),
                            RowNumber = x.RowNumber
                        })
                        .OrderByDescending(x => x.Price)
                        .ToArray()
                })
                .OrderByDescending(x => x.Halls)
                .ThenBy(x => x.Name)
                .ToArray();

            return JsonConvert.SerializeObject(theatres, Formatting.Indented);
        }

        public static string ExportPlays(TheatreContext context, double raiting)
        {
            var plays = context.Plays
                .Where(x => x.Rating <= raiting)
                .ToArray()
                .Select(x => new ExportPlaysDTO()
                {
                    Title = x.Title,
                    Duration = x.Duration.ToString("c"),
                    Rating = x.Rating,
                    Genre = x.Genre.ToString(),
                    Actors = x.Casts
                        .Where(x => x.IsMainCharacter)
                        .ToArray()
                        .Select(x => new ExportCastDTO()
                        {
                            FullName = x.FullName,
                            MainCharacter = $"Plays main character in '{x.Play.Title}'."
                        })
                        .OrderByDescending(x => x.FullName)
                        .ToArray()
                })
                .OrderBy(x => x.Title)
                .ThenByDescending(x => x.Genre.ToString())
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportPlaysDTO[]), new XmlRootAttribute("Plays"));

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            StringBuilder result = new StringBuilder();
            using StringWriter writer = new StringWriter(result);
            serializer.Serialize(writer, plays, namespaces);

            return result.ToString().TrimEnd();
        }
    }
}
