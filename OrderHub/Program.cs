using OrderHub.Collections;
using OrderHub.Domain;
using OrderHub.Services;
Console.OutputEncoding = System.Text.Encoding.UTF8;

var service = new OrderService();
service.CreateOrder("Иванов", "Клавиатура", 5490m);
service.CreateOrder("Петрова", "Монитор", 18990m);
service.CreateOrder("Сидоров", "Мышь", 1290m);
//service.ProcessNext();
//service.ProcessNext();
//service.PrintAllOrders();
//service.PrintPendingQueue();
//service.PrintRecentActions();



//service.InitHistory(
//service.CreateOrder("Иванов", "Клавиатура", 5490m),
//service.CreateOrder("Петрова", "Монитор", 18990m));

//service.PlaceOrderAfterCurrent(new OrderHub.Domain.Order("Никита","Рябка",22456m));
//service.PrintHistory();

//Console.WriteLine();
//service.TryFindOrder("Иванов", out var found);
//Console.WriteLine(found);

//Console.WriteLine();
//var bools=service.TryFindOrder("Нет такого", out _);
//Console.WriteLine(bools);

var warehouse = new WarehouseService();
warehouse.Receive("Клавиатура", 10);
warehouse.Receive("Клавиатура", 5);

warehouse.Receive("Монитор", 3);
warehouse.Sell("Монитор", 1);
warehouse.Sell("Монитор", 99);

warehouse.Sell("Проектор", 1);
warehouse.PrintStock();

var products = new List<Product>
{
    new Product("Мышь", 1500m, "Периферия"),
    new Product("Клавиатура", 4500m, "Периферия"),
    new Product("Наушники", 7500m, "Аудио"),
    new Product("Монитор", 18000m, "Мониторы"),
    new Product("Планшет", 32000m, "Планшеты"),
    new Product("Ноутбук", 65000m, "Ноутбуки"),
};

var reports = new ReportService();
const decimal threshold = 15000m;

var list = reports.ExpensiveProducts(products, threshold);
foreach (string line in list)
    Console.WriteLine(line);

Console.WriteLine(reports.CountExpensive(products, threshold));
Console.WriteLine(reports.HasVeryExpensive(products, 50000m));
Console.WriteLine(reports.AllInStock(products));
Console.WriteLine(reports.Cheapest(products));


var summary = reports.SummarizeByCategory(products);
foreach (var row in summary)
    Console.WriteLine($"{row.Category}: {row.Count} шт., средняя { row.AveragePrice:0.00} ₽");


Console.WriteLine();

IEnumerable<Order> stream = OrderStream.Orders();

foreach (Order o in stream.Take(5))
    Console.WriteLine($"  Получен: {o.CustomerName} {o.Price:0.00} ₽");

IEnumerable<Order> expensive = OrderStream.Expensive(10, 10000m);

foreach (Order o in expensive)
    Console.WriteLine($"  Получен: {o.CustomerName} {o.Price:0.00} ₽");

Console.WriteLine();
Console.WriteLine("Почему Orders().ToList() без Take зависнет:");
Console.WriteLine("ToList() вызывает MoveNext() до тех пор, пока он не вернёт false.");
Console.WriteLine("У Orders() внутри while (true) конца нет, MoveNext() всегда возвращает true,");
Console.WriteLine("список растёт бесконечно, пока не кончится память или программу не прервут.");


Console.WriteLine();
var buffer = new CircularBuffer<string>(3);

foreach (string s in new[] { "1", "2", "3", "4", "5" })
    buffer.Add(s);

Console.WriteLine("foreach: " + string.Join(", ", buffer));

Console.WriteLine("Reverse: " + string.Join(", ", buffer.Reverse()));

Console.WriteLine($"Count = {buffer.Count}, IsFull = {buffer.IsFull}");

Console.WriteLine("LINQ: " + string.Join(" ", buffer.Select(x => $"[{x}]")));
Console.ReadLine();