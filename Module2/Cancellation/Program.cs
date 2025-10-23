var cts = new CancellationTokenSource();

DoSomething(cts.Token);
await Task.Delay(2000);

cts.Cancel();

return;

async Task DoSomething(CancellationToken token)
{
    while (!token.IsCancellationRequested)
    {
        await Task.Delay(1000, token);
        Console.WriteLine("1");
    }
}