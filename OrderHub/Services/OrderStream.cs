using System;
using System.Collections.Generic;
using OrderHub.Domain;

namespace OrderHub.Services;

public class OrderStream
{
    private static readonly string[] Customers = { "Анна", "Борис", "Вера", "Глеб" };
    private static readonly string[] Products = { "Мышь", "Клавиатура", "Монитор" };

    public static IEnumerable<Order> Orders()
    {
        int n = 0;
        while (true)
        {
            n++;
            Console.WriteLine($"Готовлю заказ №{n}");

            yield return new Order(
                Customers[(n - 1) % Customers.Length],  
                Products[(n - 1) % Products.Length],    
                1000m + n * 500m);                  
        }
    }

    public static IEnumerable<Order> Expensive(int limit, decimal threshold)
    {
        for (int i = 1; i <= limit; i++)
        {
            decimal price = i * 2000m;

            if (price < threshold)
                continue; 

            yield return new Order(
                Customers[(i - 1) % Customers.Length],
                Products[(i - 1) % Products.Length],
                price);
        }
    }
}