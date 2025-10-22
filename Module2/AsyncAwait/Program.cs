
Console.WriteLine(await DownloadDataAsync());


async Task<string> DownloadDataAsync()
{
    await Task.Delay(2000);
    return "Загрузка окончена!";
}