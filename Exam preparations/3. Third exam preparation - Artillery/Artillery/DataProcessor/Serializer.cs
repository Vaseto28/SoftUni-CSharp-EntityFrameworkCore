
namespace Artillery.DataProcessor
{
    using System.Text;
    using System.Xml.Serialization;
    using Artillery.Data;
    using Artillery.DataProcessor.ExportDto;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportShells(ArtilleryContext context, double shellWeight)
        {
            var shells = context.Shells
                .Where(x => x.ShellWeight > shellWeight)
                .ToArray()
                .Select(x => new ExportShellsDTO()
                {
                    ShellWeight = x.ShellWeight,
                    Caliber = x.Caliber,
                    Guns = x.Guns
                        .Where(x => x.GunType.ToString() == "AntiAircraftGun")
                        .ToArray()
                        .Select(x => new ExportShellsGunsDTO()
                        {
                            GunType = x.GunType.ToString(),
                            GunWeight = x.GunWeight,
                            BarrelLength = x.BarrelLength,
                            Range = x.Range > 3000 ? "Long-range" : "Regular range"
                        })
                        .OrderByDescending(x => x.GunWeight)
                        .ToArray()
                })
                .OrderBy(x => x.ShellWeight)
                .ToArray();

            return JsonConvert.SerializeObject(shells, Formatting.Indented);
        }

        public static string ExportGuns(ArtilleryContext context, string manufacturer)
        {
            StringBuilder result = new StringBuilder();

            var guns = context.Guns
                .Where(x => x.Manufacturer.ManufacturerName == manufacturer)
                .ToArray()
                .Select(x => new ExportGunsDTO()
                {
                    Manufacturer = x.Manufacturer.ManufacturerName,
                    GunType = x.GunType.ToString(),
                    GunWeight = x.GunWeight,
                    BarrelLength = x.BarrelLength,
                    Range = x.Range,
                    Countries = x.CountriesGuns
                        .Where(x => x.Country.ArmySize > 4500000)
                        .ToArray()
                        .Select(x => new ExportGunsCountriesDTO()
                        {
                            Country = x.Country.CountryName,
                            ArmySize = x.Country.ArmySize
                        })
                        .OrderBy(x => x.ArmySize)
                        .ToArray()
                })
                .OrderBy(x => x.BarrelLength)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportGunsDTO[]), new XmlRootAttribute("Guns"));

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using StringWriter writer = new StringWriter(result);
            serializer.Serialize(writer, guns, namespaces);

            return result.ToString().TrimEnd();
        }
    }
}
