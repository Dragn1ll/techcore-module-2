try
{
    await Task.Run(() =>
    {
        Task.Delay(2000).Wait();
        throw new ArgumentException("Ошибка");
    });
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

