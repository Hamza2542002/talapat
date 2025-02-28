using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public static class StoreSeedingContext
    {
        public static async Task SeedAsync(StoreContext context)
        {
            await SeedBrands(context);
            await SeedCategories(context);
            await SeedProducts(context);
            await SeedDeleveryMethods(context);

            await SeedDepartments(context);
            await SeedEmployees(context);
        }

        private static async Task SeedDeleveryMethods(StoreContext context)
        {
            if (!await context.DeleveryMethods.AnyAsync())
            {
                var deleveryMethodsData = await File.ReadAllTextAsync("../Talabat.Repository/Data/DataSeed/delivery.json");
                var deleveryMethods = JsonSerializer.Deserialize<ICollection<DeleveryMethod>>(deleveryMethodsData);
                if (deleveryMethods?.Count > 0)
                {
                    foreach (var deleveryMethod in deleveryMethods)
                    {
                        context.Set<DeleveryMethod>().Add(deleveryMethod);
                    }
                }

                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedBrands(StoreContext context)
        {
            if (!await context.Brands.AnyAsync())
            {
                var brandsData = await File.ReadAllTextAsync("../Talabat.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<ICollection<Brand>>(brandsData);
                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        context.Set<Brand>().Add(brand);
                    }
                }

                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedCategories(StoreContext context)
        {
            if (!await context.Categories.AnyAsync())
            {
                var CategoriesData = await File.ReadAllTextAsync("../Talabat.Repository/Data/DataSeed/categories.json");
                var Categories = JsonSerializer.Deserialize<ICollection<Category>>(CategoriesData);
                if (Categories?.Count > 0)
                {
                    foreach (var Category in Categories)
                    {
                        context.Set<Category>().Add(Category);
                    }
                }
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedProducts(StoreContext context)
        {
            if (!await context.Products.AnyAsync())
            {
                var ProductsData = await File.ReadAllTextAsync("../Talabat.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<ICollection<Product>>(ProductsData);
                if (Products?.Count > 0)
                {
                    foreach (var product in Products)
                    {
                        product.BrandId += 3;
                        context.Set<Product>().Add(product);
                    }
                }
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedDepartments(StoreContext context)
        {
            if (!await context.Departments.AnyAsync())
            {
                var DepartmentsData = await File.ReadAllTextAsync("../Talabat.Repository/Data/DataSeed/departments.json");
                var Departments = JsonSerializer.Deserialize<ICollection<Department>>(DepartmentsData);
                if (Departments?.Count > 0)
                {
                    foreach (var Department in Departments)
                    {
                        context.Set<Department>().Add(Department);
                    }
                }
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedEmployees(StoreContext context)
        {
            if (!await context.Employees.AnyAsync())
            {
                var EmployeesData = await File.ReadAllTextAsync("../Talabat.Repository/Data/DataSeed/employees.json");
                var Employees = JsonSerializer.Deserialize<ICollection<Employee>>(EmployeesData);
                if (Employees?.Count > 0)
                {
                    foreach (var Employee in Employees)
                    {
                        context.Set<Employee>().Add(Employee);
                    }
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
