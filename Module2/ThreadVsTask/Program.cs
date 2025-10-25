using System.Diagnostics;

var worksCount = 1000;

Console.WriteLine($"Запуск {worksCount} потоков через Thread...");
var sw1 = Stopwatch.StartNew();
RunWithThreads(worksCount);
sw1.Stop();
Console.WriteLine($"Время выполнения: {sw1.ElapsedMilliseconds}ms");

Thread.Sleep(2000);

Console.WriteLine($"Запуск {worksCount} потоков через Task...");
var sw2 = Stopwatch.StartNew();
RunWithTasks(worksCount);
sw2.Stop();
Console.WriteLine($"Время выполнения: {sw2.ElapsedMilliseconds}ms");

Console.WriteLine($"Разница: {sw1.ElapsedMilliseconds - sw2.ElapsedMilliseconds}ms");

return;

void RunWithThreads(int count)
{
    var threads = new Thread[count];
        
    for (var i = 0; i < count; i++)
    {
        threads[i] = new Thread(() => Thread.Sleep(10));
        threads[i].Start();
    }

    foreach (var thread in threads)
    {
        thread.Join();
    }
}

void RunWithTasks(int count)
{
    var tasks = new Task[count];

    for (var i = 0; i < count; i++)
    {
        tasks[i] = Task.Run(() => Thread.Sleep(10));
    }
    
    Task.WaitAll(tasks);
}