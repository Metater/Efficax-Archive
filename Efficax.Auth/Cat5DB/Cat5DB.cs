
public class Cat5DB
{
    private ConcurrentQueue<(Func<Database, DBResult>, Action>))> actions = new();

    public async Task ExecuteAsync(Func<Database, DBResult> action)
    {
        TaskCompletionSource<DBResult> resultTCS = new();
        Action complete = (result) =>
        {
            resultTCS.SetResult(result);
        }
        // https://devblogs.microsoft.com/premier-developer/the-danger-of-taskcompletionsourcet-class/
    }

    public void QueueExecute(DBAction action)
    {

    }
}