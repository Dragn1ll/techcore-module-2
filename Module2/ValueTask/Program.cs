namespace ValueTask;

public class Program
{
    private static readonly Dictionary<int, string> Cache = new();
    
    static async Task Main()
    {
        Console.WriteLine("Первый вызов:");
        var result1 = await GetDataAsync(1);
        Console.WriteLine($"Результат: {result1}");
        
        Console.WriteLine("Второй вызов:");
        var result2 = await GetDataAsync(1);
        Console.WriteLine($"Результат: {result2}");
    }

    private static async ValueTask<string> GetDataAsync(int id)
    {
        if (Cache.TryGetValue(id, out var data))
        {
            return data;
        }
        
        await Task.Delay(1000);
        
        data = $"Data for ID {id}";
        Cache[id] = data;
        
        return data;
    }
}