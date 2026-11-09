using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderHub.Services;

public class WarehouseService
{
    private readonly Dictionary<string, int> stock = new Dictionary<string, int>();

    public void Receive(string productName, int quantity)
    {
        if (stock.TryGetValue(productName, out int existing))
        {
            stock[productName] = existing + quantity;
        }
        else
        {
            stock[productName] = quantity;
        }

        Console.WriteLine($"Принято: {productName} x{quantity}. Всего на складе: {stock[productName]}");
    }

    public bool Sell(string productName, int quantity)
    {
        if (!stock.TryGetValue(productName, out int existing))
        {
            Console.WriteLine($"Товар «{productName}» отсутствует на складе.");
            return false;
        }

        if (existing < quantity)
        {
            Console.WriteLine($"Недостаточно товара «{productName}»: запрошено {quantity}, в наличии {existing}.");
            return false;
        }

        int remaining = existing - quantity;

        if (remaining == 0)
        {
            stock.Remove(productName);
            Console.WriteLine($"Товар «{productName}» продан полностью и снят с учёта.");
        }
        else
        {
            stock[productName] = remaining;
            Console.WriteLine($"Продано: {productName} x{quantity}. Остаток: {remaining}");
        }

        return true;
    }

    public bool TryGetStock(string productName, out int quantity)
    {
        if (stock.TryGetValue(productName, out quantity))
        {
            return true;
        }

        quantity = 0;
        return false;
    }

    public void PrintStock()
    {
        Console.WriteLine("Остатки на складе:");

        foreach (KeyValuePair<string, int> item in stock.OrderBy(p => p.Key))
        {
            Console.WriteLine($"  {item.Key}: {item.Value}");
        }
    }
}