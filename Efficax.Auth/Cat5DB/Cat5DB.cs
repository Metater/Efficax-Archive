
public class Cat5DB
{
    private ConcurrentQueue<(Func<Database, DBResult>, Action>))> actions = new();

    public async Task ExecuteAsync(Func<Database, DBResult> action)
    {
        TaskCompletionSource<DBResult> resultTCS = new();
        Action action = (result) =>
        {
            resultTCS.SetResult(result);
        }

    }

    public void QueueExecute(DBAction action)
    {

    }
}