await foreach (var num in GetNumbersAsync())
{
    Console.WriteLine(num);
}

return;

async IAsyncEnumerable<int> GetNumbersAsync()
{
    for (var i = 0; i < 10; i++)
    {
        await Task.Delay(1000);
        yield return i;
    }
}