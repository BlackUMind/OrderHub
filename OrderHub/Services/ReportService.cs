using System.Linq;
using System.Collections.Generic;
using OrderHub.Domain;

namespace OrderHub.Services;

public class CategorySummary
{
    public string Category { get; set; } = "";
    public int Count { get; set; }
    public decimal AveragePrice { get; set; }
}
public class ReportService
{
    public List<string> ExpensiveProducts(IEnumerable<Product> products, decimal threshold)
    {
        return products
            .Where(p => p.Price > threshold)          
            .OrderBy(p => p.Price)                    
            .Select(p => $"{p.Name} — {p.Price:0.00} ₽")
            .ToList();                                
    }

    public int CountExpensive(IEnumerable<Product> products, decimal threshold)
    {
        return products.Count(p => p.Price > threshold);
    }

    public bool HasVeryExpensive(IEnumerable<Product> products, decimal threshold)
    {
        return products.Any(p => p.Price > threshold);
    }

    public bool AllInStock(IEnumerable<Product> products)
    {
        return products.All(p => p.Price > 0);
    }

    public Product? Cheapest(IEnumerable<Product> products)
    {
        return products.OrderBy(p => p.Price).FirstOrDefault();
    }

    public List<CategorySummary> SummarizeByCategory(IEnumerable<Product> products)
    {
        return products
            .GroupBy(p => p.Category)                      
            .Select(g => new CategorySummary
            {
                Category = g.Key,
                Count = g.Count(),                          
                AveragePrice = g.Average(p => p.Price)
            })
            .OrderByDescending(s => s.Count)               
            .ThenBy(s => s.Category)                        
            .ToList();
    }

    public CategorySummary? MostExpensiveCategory(IEnumerable<Product> products)
    { 
        return SummarizeByCategory(products)
            .OrderByDescending(s => s.AveragePrice)
            .FirstOrDefault();
    }

    public List<string> CategoriesWithAtLeast(IEnumerable<Product> products, int minCount)
    {
        return SummarizeByCategory(products)
            .Where(s => s.Count >= minCount)
            .Select(s => s.Category)                  
            .ToList();
    }

    public List<string> TopCustomers(
    IEnumerable<Customer> customers,
    IEnumerable<Order> orders,
    int topCount)
    {
        return customers
            .Join(orders,
                  c => c.Name,
                  o => o.CustomerName,
                  (c, o) => new { c.Name, o.Price })
            .GroupBy(x => x.Name)
            .Select(g => new { Name = g.Key, Total = g.Sum(x => x.Price) })
            .OrderByDescending(x => x.Total)
            .Take(topCount)
            .Select(x => $"{x.Name} — {x.Total:0.00} ₽")
            .ToList();
    }

    public decimal RevenueForYear(IEnumerable<Order> orders, int year)
    {
        return orders
            .Where(o => o.CreatedAt.Year == year)
            .Sum(o => o.Price);
    }

    public string? CustomerWithMostOrders(IEnumerable<Order> orders)
    {
        return orders
            .GroupBy(o => o.CustomerName)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)                
            .Select(g => g.Key)
            .FirstOrDefault();                
    }

    public List<Order> OrdersAboveAverage(IEnumerable<Order> orders)
    {
        var all = orders.ToList();
        if (all.Count == 0) return new List<Order>();  

        decimal average = all.Average(o => o.Price);  

        return all.Where(o => o.Price > average).ToList();
    }
}