var progress = new Progress<int>();
progress.ProgressChanged += (sender, percent) => 
{
    Console.WriteLine($"Прогресс: {percent}%");
};

await LongOperationAsync(progress);
return;

async Task LongOperationAsync(IProgress<int> progress)
{
    for (var i = 0; i <= 100; i += 10)
    {
        await Task.Delay(500);
        progress.Report(i);
    }
}