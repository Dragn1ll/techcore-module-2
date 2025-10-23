using System.Diagnostics;

var tasks = new[] { Task.Run(() => Task.Delay(1000)), Task.Run(() => Task.Delay(1500)), 
    Task.Run(() => Task.Delay(2000)) };

Console.WriteLine("Начато выполнение tasks...");
var sw = Stopwatch.StartNew();
await Task.WhenAll(tasks);
sw.Stop();
Console.WriteLine($"tasks выполнились за: {sw.ElapsedMilliseconds}ms");

