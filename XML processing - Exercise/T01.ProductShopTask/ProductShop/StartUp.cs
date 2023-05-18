using System.Xml.Serialization;
using AutoMapper.Configuration.Annotations;
using ProductShop.Data;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;
using ProductShop.Utilities;

namespace ProductShop;

public class StartUp
{
    public static void Main()
    {
        ProductShopContext context = new ProductShopContext();
        string inputXml = File.ReadAllText("../../../Datasets/users.xml");

        Console.WriteLine(ImportUsers(context, inputXml));
    }

    public static string ImportUsers(ProductShopContext context, string inputXml)
    {
        XmlHelper helper = new XmlHelper();
        var dtos = helper.Deserialize<ImportUsersDTO[]>("Users", inputXml);

        List<User> users = new List<User>();
        foreach (var dto in dtos)
        {
            if (String.IsNullOrEmpty(dto.FirstName) || string.IsNullOrEmpty(dto.LastName))
            {
                continue;
            }

            User user = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Age = dto.Age
            };

            users.Add(user);
        }

        context.AddRange(users);
        context.SaveChanges();

        return $"Successfully imported {users.Count}";
    }

    // Problem
    public static string ImportProducts(ProductShopContext context, string inputXml)
    {
        XmlHelper helper = new XmlHelper();

        var dtos = helper.Deserialize<ImportProductsDTO[]>("Products", inputXml);

        List<Product> products = new List<Product>();
        foreach (var dto in dtos)
        {
            if (string.IsNullOrEmpty(dto.Name))
            {
                continue;
            }

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
        context.SaveChanges();

        return $"Successfully imported {products.Count}";
    }

    public static string ImportCategories(ProductShopContext context, string inputXml)
    {
        XmlHelper helper = new XmlHelper();

        var dtos = helper.Deserialize<ImportCategoriesDTO[]>("Categories", inputXml);

        List<Category> categories = new List<Category>();
        foreach (var dto in dtos)
        {
            if (string.IsNullOrEmpty(dto.Name))
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

    public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
    {
        XmlHelper helper = new XmlHelper();

        var dtos = helper.Deserialize<ImportCategoryProductDTO[]>("CategoryProducts", inputXml);

        List<CategoryProduct> categoryProducts = new List<CategoryProduct>();
        foreach (var dto in dtos)
        {
            if (dto.CategoryId == 0 || dto.ProductId == 0)
            {
                continue;
            }

            CategoryProduct categoryProduct = new CategoryProduct()
            {
                CategoryId = dto.CategoryId,
                ProductId = dto.ProductId
            };

            categoryProducts.Add(categoryProduct);
        }

        context.CategoryProducts.AddRange(categoryProducts);
        context.SaveChanges();

        return $"Successfully imported {categoryProducts.Count}";
    }

    public static string GetProductsInRange(ProductShopContext context)
    {
        var products = context.Products
            .Where(x => x.Price >= 500 && x.Price <= 1000)
            .OrderBy(x => x.Price)
            .Take(10)
            .Select(x => new ExportProductsInRangeDTO()
            {
                Name = x.Name,
                Price = Math.Round(x.Price, 2),
                BuyerName = x.Buyer.FirstName + " " + x.Buyer.LastName
            })
            .ToArray();

        XmlHelper helper = new XmlHelper();

        return helper.Serialize<ExportProductsInRangeDTO[]>(products, "Products");
    }

    public static string GetSoldProducts(ProductShopContext context)
    {
        var users = context.Users
            .Where(x => x.ProductsSold.Count != 0)
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .Take(5)
            .Select(x => new ExportUsersDTO()
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Products = x.ProductsSold.Select(p => new ExportSoldProductsDTO()
                {
                    Name = p.Name,
                    Price = Math.Round(p.Price, 2)
                })
                .ToList()
            })
            .ToArray();

        XmlHelper helper = new XmlHelper();

        return helper.Serialize<ExportUsersDTO[]>(users, "Users");
    }
}