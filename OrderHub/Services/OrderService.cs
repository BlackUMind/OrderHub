using OrderHub.Domain;
namespace OrderHub.Services;

public class OrderService
{
    private readonly List<Order> allOrders = new();
    private readonly Queue<Order> pendingQueue = new();
    private readonly Stack<string> undoHistory = new();

    public Order CreateOrder(string customerName, string productName, decimal price)
    {
        Order Order1 = new Order(customerName, productName, price);
        allOrders.Add(Order1);
        pendingQueue.Enqueue(Order1);
        undoHistory.Push($"Создан заказ {Order1.Id} для {customerName}");

        return Order1;
    }
    public Order? ProcessNext()
    {
        if (pendingQueue.Count == 0) { return null; }
        var ProcessOrder = pendingQueue.Dequeue();
        undoHistory.Push($"Обработан заказ: {ProcessOrder}");
        Console.WriteLine($"Обработан заказ: {ProcessOrder}");
        return ProcessOrder;

    }
    public bool UndoLastAction()
    {
        if (undoHistory.Count == 0) { return false; }
        var undo = undoHistory.Pop();
        Console.WriteLine($"Отменено действие: {undo}");
        return true;
    }
    public void PrintAllOrders()
    {
        string Orders ="";
        foreach ( var order in allOrders)
        {
            Console.WriteLine(order);
        }
    }
    public void PrintPendingQueue()
    {
        int count = 0;
        foreach ( var order in pendingQueue)
        {
            count++;
        }

        Console.WriteLine($"Всего заказов ждут {count}, первый - {pendingQueue.Peek()}");
    }

    public void PrintRecentActions()
    {
        foreach(var action in undoHistory)
        {
            Console.WriteLine(action.ToString());
        }
    }

};

