using System;
using System.Collections.Generic;

namespace OrderHub.Domain;

public class Product : IComparable<Product>
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string Category { get; private set; }

    public Product(string name, decimal price, string category)
    {
        Name = name;
        Price = price;
        Category = category;
    }

    public int CompareTo(Product? other)
    {
        if (other is null) return 1;

        return Price.CompareTo(other.Price);
    }

    public override string ToString() => $"{Name} — {Price:0.00} ₽ ({Category})";
}

public class NameComparer : IComparer<Product>
{
    public int Compare(Product? a, Product? b)
    {
        return string.Compare(a?.Name, b?.Name, StringComparison.Ordinal);
    }
}

public class PriceDescComparer : IComparer<Product>
{
    public int Compare(Product? a, Product? b)
    {
        if (ReferenceEquals(a, b)) return 0;
        if (a is null) return 1; 
        if (b is null) return -1;

        return b.Price.CompareTo(a.Price);
    }
}