using CarDealer.Data;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer;

public class StartUp
{
    public static void Main()
    {
        CarDealerContext ctx = new CarDealerContext();
        string inputJson = File.ReadAllText("../../../Datasets/suppliers.json");

        Console.WriteLine(ImportSuppliers(ctx, inputJson));
    }

    public static string ImportSuppliers(CarDealerContext context, string inputJson)
    {
        var dtos = JsonConvert.DeserializeObject<ImportSuppliersDTO[]>(inputJson);

        List<Supplier> suppliers = new List<Supplier>();
        foreach (var dto in dtos)
        {
            Supplier supplier = new Supplier()
            {
                Name = dto.Name,
                IsImporter = dto.IsImporter
            };

            suppliers.Add(supplier);
        }

        context.AddRange(suppliers);
        context.SaveChanges();

        return $"Successfully imported {suppliers.Count}.";
    }

    public static string ImportParts(CarDealerContext context, string inputJson)
    {
        var dtos = JsonConvert.DeserializeObject<ImportPartsDTO[]>(inputJson);

        List<Part> parts = new List<Part>();
        foreach (var dto in dtos)
        {
            if (context.Suppliers.Find(dto.SupplierId) == null)
            {
                continue;
            }

            Part part = new Part()
            {
                Name = dto.Name,
                Price = dto.Price,
                Quantity = dto.Quantity,
                SupplierId = dto.SupplierId
            };

            parts.Add(part);
        }

        context.AddRange(parts);
        context.SaveChangesAsync();

        return $"Successfully imported {parts.Count}.";
    }

    public static string ImportCars(CarDealerContext context, string inputJson)
    {
        var dtos = JsonConvert.DeserializeObject<ImportCarDTO[]>(inputJson);

        List<Car> cars = new List<Car>();
        foreach (var dto in dtos)
        {
            Car car = new Car()
            {
                Make = dto.Make,
                Model = dto.Model,
                TraveledDistance = dto.TravelledDistance,
                PartsCars = dto.PartsCars
            };

            cars.Add(car);
        }

        context.AddRange(cars);
        context.SaveChangesAsync();

        return $"Successfully imported {cars.Count}."; 
    }

    //Not working
    public static string ImportCustomers(CarDealerContext context, string inputJson)
    {
        var dtos = JsonConvert.DeserializeObject<ImportCustomersDTO[]>(inputJson);

        List<Customer> customers = new List<Customer>();
        foreach (var dto in dtos)
        {
            Customer customer = new Customer()
            {
                Name = dto.Name,
                BirthDate = dto.BirthDate,
                IsYoungDriver = dto.IsYoungDriver
            };

            customers.Add(customer);
        }

        context.AddRange(customers);
        context.SaveChangesAsync();

        return $"Successfully imported {customers.Count}.";
    }

    public static string ImportSales(CarDealerContext context, string inputJson)
    {
        var dtos = JsonConvert.DeserializeObject<ImportSalesDTO[]>(inputJson);

        List<Sale> sales = new List<Sale>();
        foreach (var dto in dtos)
        {
            Sale sale = new Sale()
            {
                CarId = dto.CarId,
                CustomerId = dto.CustomerId,
                Discount = dto.Discount
            };

            sales.Add(sale);
        }

        context.AddRange(sales);
        context.SaveChangesAsync();

        return $"Successfully imported {sales.Count}.";
    }

    public static string GetOrderedCustomers(CarDealerContext context)
    {
        var customers = context.Customers
            .OrderBy(x => x.BirthDate)
            .ThenBy(x => x.IsYoungDriver)
            .Select(x => new ExportCustomersDTO()
            {
                Name = x.Name,
                BirthDate = x.BirthDate.ToString("dd/MM/yyyy"),
                IsYoungDriver = x.IsYoungDriver
            })
            .ToList();

        return JsonConvert.SerializeObject(customers, Formatting.Indented);
    }

    //Not working
    public static string GetCarsFromMakeToyota(CarDealerContext context)
    {
        var toyotas = context.Cars
            .Where(x => x.Make == "Toyota")
            .OrderBy(x => x.Model)
            .ThenByDescending(x => x.TraveledDistance)
            .Select(x => new ExportToyotaCarsDTO()
            {
                Id = x.Id,
                Make = x.Make,
                Model = x.Model,
                TraveledDistance = x.TraveledDistance,
            })
            .ToList();

        return JsonConvert.SerializeObject(toyotas, Formatting.Indented);
    }

    public static string GetLocalSuppliers(CarDealerContext context)
    {
        var suppliers = context.Suppliers
            .Where(x => !x.IsImporter)
            .Select(x => new ExportSuppliersDTO()
            {
                Id = x.Id,
                Name = x.Name,
                PartsCount = x.Parts.Count
            })
            .ToList();

        return JsonConvert.SerializeObject(suppliers, Formatting.Indented);
    }

    public static string GetCarsWithTheirListOfParts(CarDealerContext context)
    {
        var cars = context.Cars
            .Select(x => new ExportCarsPartsDTO()
            {
                Car = new ExportCarsDTO()
                {
                    Make = x.Make,
                    Model = x.Model,
                    TraveledDistance = x.TraveledDistance
                },
                Parts = x.PartsCars
                .Select(x => new ExportPartsDTO()
                {
                    Name = x.Part.Name,
                    Price = $"{Math.Round(x.Part.Price, 2)}"
                })
                .ToArray()
            })
            .ToArray();

        return JsonConvert.SerializeObject(cars, Formatting.Indented);
    }

    public static string GetTotalSalesByCustomer(CarDealerContext context)
    {
        var customers = context.Customers
            .Where(c => c.Sales.Count > 0)
            .Select(c => new ExportCustomersWithBoughtCarsCountDTO()
            {
                FullName = c.Name,
                BoughtCars = c.Sales.Count,
                SpentMoney = Math.Round(c.Sales.Sum(s => s.Car.PartsCars.Sum(p => p.Part.Price) - s.Discount), 2)
            })
            .OrderByDescending(x => x.SpentMoney)
            .ThenByDescending(x => x.BoughtCars)
            .ToList();

        return JsonConvert.SerializeObject(customers);
    }
}