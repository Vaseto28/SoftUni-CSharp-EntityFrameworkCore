namespace Artillery.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Xml.Serialization;
    using Artillery.Data;
    using Artillery.Data.Models;
    using Artillery.Data.Models.Enums;
    using Artillery.DataProcessor.ImportDto;
    using Microsoft.Extensions.Primitives;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid data.";
        private const string SuccessfulImportCountry =
            "Successfully import {0} with {1} army personnel.";
        private const string SuccessfulImportManufacturer =
            "Successfully import manufacturer {0} founded in {1}.";
        private const string SuccessfulImportShell =
            "Successfully import shell caliber #{0} weight {1} kg.";
        private const string SuccessfulImportGun =
            "Successfully import gun {0} with a total weight of {1} kg. and barrel length of {2} m.";

        public static string ImportCountries(ArtilleryContext context, string xmlString)
        {
            StringBuilder result = new StringBuilder();

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();

            XmlSerializer serializer = new XmlSerializer(typeof(ImportCountriesDTO[]), new XmlRootAttribute("Countries"));

            using StringReader reader = new StringReader(xmlString);
            var dtos = (ImportCountriesDTO[])serializer.Deserialize(reader);

            List<Country> countries = new List<Country>();
            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                Country country = new Country()
                {
                    CountryName = dto.CountryName,
                    ArmySize = dto.ArmySize
                };

                countries.Add(country);
                result.AppendLine(string.Format(SuccessfulImportCountry, country.CountryName, country.ArmySize));
            }

            context.Countries.AddRange(countries);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportManufacturers(ArtilleryContext context, string xmlString)
        {
            StringBuilder result = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(ImportManufacturersDTO[]), new XmlRootAttribute("Manufacturers"));

            using StringReader reader = new StringReader(xmlString);
            var dtos = (ImportManufacturersDTO[])serializer.Deserialize(reader);

            List<Manufacturer> manufacturers = new List<Manufacturer>();
            foreach (var dto in dtos)
            {
                var invalidManufacturer = manufacturers.FirstOrDefault(x => x.ManufacturerName == dto.ManufacturerName);
                if (!IsValid(dto) || invalidManufacturer != null)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                Manufacturer manufacturer = new Manufacturer()
                {
                    ManufacturerName = dto.ManufacturerName,
                    Founded = dto.Founded
                };

                string[] args = manufacturer.Founded.Split(", ");
                string townName = args[args.Length - 2];
                string countryName = args[args.Length - 1];

                manufacturers.Add(manufacturer);
                result.AppendLine(string.Format(SuccessfulImportManufacturer, manufacturer.ManufacturerName, townName + ", " + countryName));
            }

            context.Manufacturers.AddRange(manufacturers);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportShells(ArtilleryContext context, string xmlString)
        {
            StringBuilder result = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(ImportShellsDTO[]), new XmlRootAttribute("Shells"));

            using StringReader reader = new StringReader(xmlString);
            var dtos = (ImportShellsDTO[])serializer.Deserialize(reader);

            List<Shell> shells = new List<Shell>();
            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                Shell shell = new Shell()
                {
                    ShellWeight = dto.ShellWeight,
                    Caliber = dto.Caliber
                };

                shells.Add(shell);
                result.AppendLine(string.Format(SuccessfulImportShell, shell.Caliber, shell.ShellWeight));
            }

            context.Shells.AddRange(shells);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportGuns(ArtilleryContext context, string jsonString)
        {
            StringBuilder result = new StringBuilder();

            var dtos = JsonConvert.DeserializeObject<ImportGunsDTO[]>(jsonString);

            List<Gun> guns = new List<Gun>();
            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                bool isGunTypeValid = Enum.TryParse(typeof(GunType), dto.GunType, false, out object? gunType);
                if (!isGunTypeValid)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                List<CountryGun> countriesGuns = new List<CountryGun>();
                foreach (var countryId in dto.Countries)
                {
                    Country country = context.Countries.Find(countryId.Id);
                    if (country == null)
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }

                    CountryGun countryGun = new CountryGun()
                    {
                        CountryId = country.Id
                    };

                    countriesGuns.Add(countryGun);
                }

                Gun gun = new Gun()
                {
                    ManufacturerId = dto.ManufacturerId,
                    GunWeight = dto.GunWeight,
                    BarrelLength = dto.BarrelLength,
                    NumberBuild = dto.NumberBuild,
                    Range = dto.Range,
                    GunType = (GunType)Enum.Parse(typeof(GunType), dto.GunType),
                    ShellId = dto.ShellId,
                    CountriesGuns = countriesGuns
                };

                guns.Add(gun);
                result.AppendLine(string.Format(SuccessfulImportGun, dto.GunType, gun.GunWeight, gun.BarrelLength));
            }

            context.Guns.AddRange(guns);
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