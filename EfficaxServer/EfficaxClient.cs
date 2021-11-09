namespace EfficaxServer; //{}

public class EfficaxClient
{
    public readonly ulong guid;

    private readonly ConcurrentStack<ulong> zones = new();

    public EfficaxClient(ulong guid)
    {
        this.guid = guid;
    }

    public ulong GetCurrentZone()
    {
        if (!zones.TryPeek(out ulong currentZone)) currentZone = 0;
        return currentZone;
    }

    public ulong[] GetOccupiedZones()
    {
        return zones.ToArray();
    }

    public ulong EnterZone(ulong zone)
    {
        ulong previousZone = GetCurrentZone();
        zones.Push(zone);
        return previousZone;
    }

    public ulong ExitZone()
    {
        if (!zones.TryPop(out ulong exitedZone)) exitedZone = 0;
        return exitedZone;
    }
}