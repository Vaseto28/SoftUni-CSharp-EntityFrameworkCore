namespace Theatre.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Xml.Serialization;
    using Newtonsoft.Json;
    using Theatre.Data;
    using Theatre.Data.Models;
    using Theatre.Data.Models.Enums;
    using Theatre.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportPlay
            = "Successfully imported {0} with genre {1} and a rating of {2}!";

        private const string SuccessfulImportActor
            = "Successfully imported actor {0} as a {1} character!";

        private const string SuccessfulImportTheatre
            = "Successfully imported theatre {0} with #{1} tickets!";



        public static string ImportPlays(TheatreContext context, string xmlString)
        {
            StringBuilder result = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(ImportPlaysDTO[]), new XmlRootAttribute("Plays"));

            using StringReader reader = new StringReader(xmlString);
            var dtos = (ImportPlaysDTO[])serializer.Deserialize(reader);

            List<Play> plays = new List<Play>();
            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                bool isGenreValid = Enum.TryParse(typeof(Genre), dto.Genre, out object? genre);
                if (!isGenreValid)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                int hours = int.Parse(dto.Duration.Split(':')[0]);
                int minutes = int.Parse(dto.Duration.Split(':')[1]);
                int seconds = int.Parse(dto.Duration.Split(':')[2]);

                if (hours == 0)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                TimeSpan duration = new TimeSpan(hours, minutes, seconds);

                Play play = new Play()
                {
                    Title = dto.Title,
                    Duration = duration,
                    Rating = dto.Rating,
                    Genre = (Genre)Enum.Parse(typeof(Genre), dto.Genre),
                    Description = dto.Description,
                    Screenwriter = dto.Screenwriter
                };

                plays.Add(play);
                result.AppendLine(string.Format(SuccessfulImportPlay, play.Title, play.Genre, play.Rating));
            }

            context.Plays.AddRange(plays);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportCasts(TheatreContext context, string xmlString)
        {
            StringBuilder result = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(ImportCastsDTO[]), new XmlRootAttribute("Casts"));

            using StringReader reader = new StringReader(xmlString);
            var dtos = (ImportCastsDTO[])serializer.Deserialize(reader);

            List<Cast> casts = new List<Cast>();
            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                Cast cast = new Cast()
                {
                    FullName = dto.FullName,
                    IsMainCharacter = dto.IsMainCharacter,
                    PhoneNumber = dto.PhoneNumber,
                    PlayId = dto.PlayId
                };

                casts.Add(cast);
                result.AppendLine(string.Format(SuccessfulImportActor, cast.FullName, cast.IsMainCharacter ? "main" : "lesser"));
            }

            context.Casts.AddRange(casts);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportTtheatersTickets(TheatreContext context, string jsonString)
        {
            StringBuilder result = new StringBuilder();

            var dtos = JsonConvert.DeserializeObject<ImportTheatresDTO[]>(jsonString);

            List<Theatre> theatres = new List<Theatre>();
            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                List<Ticket> tickets = new List<Ticket>();
                foreach (var ticket in dto.Tickets)
                {
                    if (!IsValid(ticket))
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }

                    Ticket validTicket = new Ticket()
                    {
                        Price = ticket.Price,
                        RowNumber = ticket.RowNumber,
                        PlayId = ticket.PlayId
                    };

                    tickets.Add(validTicket);
                }

                Theatre theatre = new Theatre()
                {
                    Name = dto.Name,
                    NumberOfHalls = dto.NumberOfHalls,
                    Director = dto.Director,
                    Tickets = tickets
                };

                theatres.Add(theatre);
                result.AppendLine(string.Format(SuccessfulImportTheatre, theatre.Name, theatre.Tickets.Count));
            }

            context.Theatres.AddRange(theatres);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }


        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}
