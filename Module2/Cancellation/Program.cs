var cts = new CancellationTokenSource();

try
{
    DoSomething(cts.Token);
    await Task.Delay(2000);

    cts.Cancel();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    throw;
}

return;

async Task DoSomething(CancellationToken token)
{
    while (!token.IsCancellationRequested)
    {
        await Task.Delay(1000, token);
        Console.WriteLine("1");
    }
    
    token.ThrowIfCancellationRequested();
}