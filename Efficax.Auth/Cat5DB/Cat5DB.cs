
public class Cat5DB
{
    private ConcurrentQueue<(Action<Database>, TaskCompletionSource>)> actionQueue = new();

    private Database database;

    public Cat5DB()
    {
        database = new Database();
    }

    public async Task ExecuteAsync(Action<Database> action)
    {
        TaskCompletionSource tcs = new();
        actionQueue.Enqueue((action, tcs));
        return tcs.Task;
    }

    public void QueueExecute(DBAction action)
    {
        actionQueue.Enqueue((action, null));
    }

    public void Execute()
    {
        int actionCount = actionQueue.Count;
        for (int i = 0; i < actionCount; i++)
        {
            if (actionQueue.TryDequeue(out (Action<Database>, TaskCompletionSource>) action))
            {
                action.Item1(database);
                action.Item2.SetResult();
            }
            else break;
        }
    }
}