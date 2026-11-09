using System;
using System.Collections.Generic;
using OrderHub.Domain;

namespace OrderHub.Services;

public class CatalogService
{
    private readonly List<Product> products = new();

    public void AddProduct(string name, decimal price, string category)
    {
        products.Add(new Product(name, price, category));
    }

    public void PrintSortedByNaturalOrder()
    {
        var sorted = new List<Product>(products);
        sorted.Sort(); 

        Console.WriteLine("Естественный порядок (по цене):");
        foreach (Product p in sorted)
        {
            Console.WriteLine($"  {p}");
        }
    }

    public void PrintSortedByName()
    {
        var sorted = new List<Product>(products);
        sorted.Sort(new NameComparer());

        Console.WriteLine("По названию:");
        foreach (Product p in sorted)
        {
            Console.WriteLine($"  {p}");
        }
    }

    public void PrintSortedByNameLength()
    {
        var sorted = new List<Product>(products);
        sorted.Sort((a, b) => a.Name.Length.CompareTo(b.Name.Length));

        Console.WriteLine("По длине названия:");
        foreach (Product p in sorted)
        {
            Console.WriteLine($"  {p}");
        }

        Console.WriteLine("(Тип Product не менялся: сортировка задана лямбдой на месте вызова.)");
    }

    public void PrintAll()
    {
        Console.WriteLine("Каталог (в порядке добавления):");
        foreach (Product p in products)
        {
            Console.WriteLine($"  {p}");
        }
    }
}