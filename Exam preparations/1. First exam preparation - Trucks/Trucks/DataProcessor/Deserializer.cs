namespace Trucks.DataProcessor;

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Data;
using Newtonsoft.Json;
using Trucks.Data.Models;
using Trucks.Data.Models.Enums;
using Trucks.DataProcessor.ImportDto;

public class Deserializer
{
    private const string ErrorMessage = "Invalid data!";

    private const string SuccessfullyImportedDespatcher
        = "Successfully imported despatcher - {0} with {1} trucks.";

    private const string SuccessfullyImportedClient
        = "Successfully imported client - {0} with {1} trucks.";

    public static string ImportDespatcher(TrucksContext context, string xmlString)
    {
        XmlRootAttribute root = new XmlRootAttribute("Despatchers");
        XmlSerializer serializer = new XmlSerializer(typeof(ImportDespatchersDTO[]), root);

        StringBuilder result = new StringBuilder();

        using StringReader reader = new StringReader(xmlString);
        var dtos = (ImportDespatchersDTO[])serializer.Deserialize(reader);

        List<Despatcher> despatchers = new List<Despatcher>();
        foreach (var dto in dtos)
        {
            if (!IsValid(dto) || string.IsNullOrEmpty(dto.Position))
            {
                result.AppendLine(ErrorMessage);
                continue;
            }

            var trucksOfCurrDTOs = dto.Trucks
                .ToArray();

            List<Truck> validTrucks = new List<Truck>();
            foreach (var truck in trucksOfCurrDTOs)
            {
                if (int.Parse(truck.CategoryType) == 321312)
                {
                    Console.WriteLine();
                }

                //int categoryTypeLowerBound = Enum.GetValues(typeof(CategoryType)).GetLowerBound(0);
                //int categoryTypeUpperBound = Enum.GetValues(typeof(CategoryType)).GetUpperBound(0);

                //int makeTypeLowerBound = Enum.GetValues(typeof(MakeType)).GetLowerBound(0);
                //int makeTypeUpperBound = Enum.GetValues(typeof(MakeType)).GetUpperBound(0);

                //bool isCategoryTypeValid = int.Parse(truck.CategoryType) >= categoryTypeLowerBound && int.Parse(truck.CategoryType) <= categoryTypeUpperBound;
                //bool isMakeTypeValid = int.Parse(truck.MakeType) >= makeTypeLowerBound && int.Parse(truck.MakeType) <= makeTypeUpperBound;

                if (!IsValid(truck)/* || !isCategoryTypeValid || !isMakeTypeValid*/)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                Truck validTruck = new Truck()
                {
                    RegistrationNumber = truck.RegistrationNumber,
                    VinNumber = truck.VinNumber,
                    TankCapacity = truck.TankCapacity,
                    CargoCapacity = truck.CargoCapacity,
                    CategoryType = (CategoryType)Enum.Parse(typeof(CategoryType), truck.CategoryType),
                    MakeType = (MakeType)Enum.Parse(typeof(MakeType), truck.MakeType)
                };

                validTrucks.Add(validTruck);
            }

            Despatcher despatcher = new Despatcher()
            {
                Name = dto.Name,
                Position = dto.Position,
                Trucks = validTrucks
            };

            despatchers.Add(despatcher);
            result.AppendLine(String.Format(SuccessfullyImportedDespatcher, despatcher.Name, despatcher.Trucks.Count));
        }

        context.AddRange(despatchers);
        context.SaveChanges();

        return result.ToString().Trim();
    }

    public static string ImportClient(TrucksContext context, string jsonString)
    {
        StringBuilder result = new StringBuilder();
        var dtos = JsonConvert.DeserializeObject<ImportClientsDTO[]>(jsonString);

        List<Client> clients = new List<Client>();
        foreach (var dto in dtos)
        {
            if (!IsValid(dto) || dto.Type.ToLower() == "usual")
            {
                result.AppendLine(ErrorMessage);
                continue;
            }

            List<int> truckIds = new List<int>();
            List<ClientTruck> validTrucks = new List<ClientTruck>();
            foreach (var truckId in dto.Trucks)
            {
                Truck truck = context.ClientsTrucks.Find(truckId);
                if (truck == null)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                if (truckIds.Contains(truckId))
                {
                    continue;
                }

                ClientTruck clientTruck = new ClientTruck()
                {
                    TruckId = truck.Id
                };

                truckIds.Add(truck.Id);
                validTrucks.Add(clientTruck);
            }

            Client client = new Client()
            {
                Name = dto.Name,
                Nationality = dto.Nationality,
                Type = dto.Type,
                ClientsTrucks = validTrucks
            };

            clients.Add(client);
            result.AppendLine(string.Format(SuccessfullyImportedClient, dto.Name, validTrucks.Count));
        }

        context.Clients.AddRange(clients);
        context.SaveChanges();

        return result.ToString().Trim();
    }

    private static bool IsValid(object dto)
    {
        var validationContext = new ValidationContext(dto);
        var validationResult = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(dto, validationContext, validationResult, true);

        return isValid;
    }
}