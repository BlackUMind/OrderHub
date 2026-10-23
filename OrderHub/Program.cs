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



service.InitHistory(
service.CreateOrder("Иванов", "Клавиатура", 5490m),
service.CreateOrder("Петрова", "Монитор", 18990m));

service.PlaceOrderAfterCurrent(new OrderHub.Domain.Order("Никита","Рябка",22456m));
service.PrintHistory();

Console.WriteLine();
service.TryFindOrder("Иванов", out var found);
Console.WriteLine(found);

Console.WriteLine();
var bools=service.TryFindOrder("Нет такого", out _);
Console.WriteLine(bools);

Console.ReadLine();