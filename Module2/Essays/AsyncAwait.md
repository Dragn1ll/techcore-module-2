Когда мы пишем await someTask, await "отпускает" поток, позволяя ему вернуться в пул и обслуживать другие операции, 
пока асинхронная задача не завершится. Когда компилятор видит ключевое слово async, он превращает наш простой метод в 
сложную машину состояний. Пример:
```csharp
public async Task<string> GetDataAsync()
{
    Console.WriteLine("Начало метода");
    string data = await DownloadStringAsync("https://example.com");
    Console.WriteLine($"Получены данные: {data}");
    return data.ToUpper();
}
```
После компиляции создаётся примерно такая State Machine:
```csharp
public class StateMachine : IAsyncStateMachine
{
    public int _state;
    public AsyncTaskMethodBuilder<string> _builder;
    private string _result;
    private string _data;
    
    void IAsyncStateMachine.MoveNext()
    {
        try
        {
            switch (_state)
            {
                case 0:
                    Console.WriteLine("Начало метода");
                    var task = DownloadStringAsync("https://example.com");
                    _state = 1;
                    var awaiter = task.GetAwaiter();
                    
                    if (!awaiter.IsCompleted)
                    {
                        _builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
                        return;
                    }
                    break;
                    
                case 1:
                    _data = awaiter.GetResult();
                    Console.WriteLine($"Получены данные: {_data}");
                    _result = _data.ToUpper();
                    _builder.SetResult(_result);
                    break;
            }
        }
        catch (Exception ex)
        {
            _builder.SetException(ex);
        }
    }
}
```
Как работает:
+ Состояние 0: Метод начинает выполняться синхронно до первого await
+ Приостановка: Если задача не завершена, метод "запоминает" состояние и возвращает управление
+ Возобновление: Когда задача завершается, выполнение продолжается с места остановки

SynchronizationContext — это механизм, который определяет, в каком потоке продолжится выполнение после await.