namespace Trucks.DataProcessor;

using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Trucks.Data.Models.Enums;
using Trucks.DataProcessor.ExportDto;

public class Serializer
{
    public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
    {
        var despatchers = context.Despatchers
            .Where(x => x.Trucks.Count != 0)
            .ToArray()
            .Select(x => new ExportDespatchersDTO()
            {
                DespatcherName = x.Name,
                Trucks = x.Trucks
                    .ToArray()
                    .Select(x => new ExportDespatcherTrucksDTO()
                    {
                        RegistrationNumber = x.RegistrationNumber,
                        Make = x.MakeType.ToString()
                    })
                    .OrderBy(x => x.RegistrationNumber)
                    .ToArray(),
                TrucksCount = x.Trucks.Count
            })
            .OrderByDescending(x => x.Trucks.Length)
            .ThenBy(x => x.DespatcherName)
            .ToArray();

        XmlRootAttribute root = new XmlRootAttribute("Despatchers");
        XmlSerializer serializer = new XmlSerializer(typeof(ExportDespatchersDTO[]), root);

        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
        namespaces.Add(string.Empty, string.Empty);

        StringBuilder result = new StringBuilder();
        using StringWriter writer = new StringWriter(result);
        serializer.Serialize(writer, despatchers, namespaces);

        return result.ToString().Trim();
    }

    public static string ExportClientsWithMostTrucks(TrucksContext context, int capacity)
    {
        var clients = context.Clients
            .Where(x => x.ClientsTrucks.Any(x => x.Truck.TankCapacity >= capacity))
            .ToArray()
            .Select(x => new ExportClientsWithTheirTrucksDTO()
            {
                Name = x.Name,
                Trucks = x.ClientsTrucks
                    .Where(x => x.Truck.TankCapacity >= capacity)
                    .ToArray()
                    .OrderBy(x => x.Truck.MakeType.ToString())
                    .ThenByDescending(x => x.Truck.CargoCapacity)
                    .Select(x => new ExportTrucksDTO()
                    {
                        TruckRegistrationNumber = x.Truck.RegistrationNumber,
                        VinNumber = x.Truck.VinNumber,
                        TankCapacity = x.Truck.TankCapacity,
                        CargoCapacity = x.Truck.CargoCapacity,
                        CategoryType = x.Truck.CategoryType.ToString(),
                        MakeType = x.Truck.MakeType.ToString()
                    })
                    .ToArray()
            })
            .OrderByDescending(x => x.Trucks.Length)
            .ThenBy(x => x.Name)
            .Take(10)
            .ToArray();

        return JsonConvert.SerializeObject(clients, Newtonsoft.Json.Formatting.Indented);
    }
}
