namespace EfficaxServer; //{}

public class EfficaxClient
{
    private readonly EfficaxServer efficaxServer;

    public readonly ulong guid;
    public readonly NetPeer client;

    private readonly ConcurrentStack<ulong> zones = new();

    public EfficaxClient(EfficaxServer efficaxServer, ulong guid, NetPeer client)
    {
        this.efficaxServer = efficaxServer;
        this.guid = guid;
        this.client = client;
    }

    public void Send(Packet packet, DeliveryMethod deliveryMethod)
    {
        efficaxServer.netOutDispatcher.Send(client, packet, deliveryMethod);
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