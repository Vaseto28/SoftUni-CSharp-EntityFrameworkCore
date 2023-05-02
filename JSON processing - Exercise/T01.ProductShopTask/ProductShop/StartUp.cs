using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;

namespace ProductShop;

public class StartUp
{
    public static void Main()
    {
        ProductShopContext context = new ProductShopContext();
        string inputJson = File.ReadAllText(@"../../../Datasets/users.json");

        Console.WriteLine(ImportUsers(context, inputJson));
    }

    public static string ImportUsers(ProductShopContext context, string inputJson)
    {
        ImportUserDTO[] dtos = JsonConvert.DeserializeObject<ImportUserDTO[]>(inputJson);

        ICollection<User> users = new HashSet<User>();
        foreach (var dto in dtos)
        {
            User user = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Age = dto.Age
            };

            users.Add(user);
        }

        context.Users.AddRange(users);
        context.SaveChanges();

        return $"Successfully imported {users.Count}";
    }

    //Problem!!!
    public static string ImportProducts(ProductShopContext context, string inputJson)
    {
        var dtos = JsonConvert.DeserializeObject<ImportProductsDTO[]>(inputJson);

        List<Product> products = new List<Product>();
        foreach (var dto in dtos)
        {
            Product product = new Product()
            {
                Name = dto.Name,
                Price = dto.Price,
                SellerId = dto.SellerId,
                BuyerId = dto.BuyerId
            };

            products.Add(product);
        }

        context.Products.AddRange(products);
        context.SaveChangesAsync();

        return $"Successfully imported {products.Count}";
    }

    public static string ImportCategories(ProductShopContext context, string inputJson)
    {
        var dtos = JsonConvert.DeserializeObject<ImportCategoriesDTO[]>(inputJson);

        List<Category> categories = new List<Category>();
        foreach (var dto in dtos)
        {
            if (dto.Name == null)
            {
                continue;
            }

            Category category = new Category()
            {
                Name = dto.Name
            };

            categories.Add(category);
        }

        context.Categories.AddRange(categories);
        context.SaveChanges();

        return $"Successfully imported {categories.Count}";
    }

    public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
    {
        var dtos = JsonConvert.DeserializeObject<ImportCategoriesProductsDTO[]>(inputJson);

        List<CategoryProduct> categoryProducts = new List<CategoryProduct>();
        foreach (var dto in dtos)
        {
            CategoryProduct categoryProduct = new CategoryProduct()
            {
                CategoryId = dto.CategoryId,
                ProductId = dto.ProductId
            };

            categoryProducts.Add(categoryProduct);
        }

        context.CategoriesProducts.AddRange(categoryProducts);
        context.SaveChanges();

        return $"Successfully imported {categoryProducts.Count}";
    }

    //Problem!!!
    public static string GetProductsInRange(ProductShopContext context)
    {
        var products = context.Products
            .Where(x => x.Price >= 500 && x.Price <= 1000)
            .OrderBy(x => x.Price)
            .Select(x => new ExportProductsDTO()
            {
                Name = x.Name,
                Price = x.Price,
                SellerName = $"{x.Seller.FirstName} {x.Seller.LastName}"
            })
            .AsNoTracking()
            .ToArray();

        string json = JsonConvert.SerializeObject(products, Formatting.Indented);

        return json;
    }

    //Problem
    public static string GetSoldProducts(ProductShopContext context)
    {
        var users = context.Users
            .Where(x => x.ProductsSold.Count != 0)
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .Select(x => new ExportUsersWithAtLeastOneSoldProductDTO()
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Products = x.ProductsSold
                    .Where(x => x.Buyer != null)
                    .Select(x => new ExportProductInformationDTO()
                    {
                        Name = x.Name,
                        Price = x.Price,
                        BuyerFirstName = x.Buyer.FirstName,
                        BuyerLastName = x.Buyer.LastName
                    })
                    .ToArray()
            })
            .ToArray();

        return JsonConvert.SerializeObject(users, Formatting.Indented);
    }

    public static string GetCategoriesByProductsCount(ProductShopContext context)
    {
        var categories = context.Categories
            .OrderByDescending(x => x.CategoriesProducts.Count)
            .Select(x => new ExportCategoriesDTO()
            {
                Category = x.Name,
                ProductsCount = x.CategoriesProducts.Count,
                AveragePrice = decimal.Parse($"{x.CategoriesProducts.Average(x => x.Product.Price):f2}"),
                TotalRevenue = decimal.Parse($"{x.CategoriesProducts.Sum(x => x.Product.Price):f2}")
            })
            .ToArray();

        return JsonConvert.SerializeObject(categories, Formatting.Indented);
    }
}