namespace EfficaxServer.Network; //{}

public class PeerClientMap
{
    private readonly ConcurrentDictionary<NetPeer, EfficaxClient> peerClient = new();
    private readonly ConcurrentDictionary<EfficaxClient, NetPeer> clientPeer = new();

    public void Add(NetPeer peer, EfficaxClient client)
    {
        peerClient.TryAdd(peer, client);
        clientPeer.TryAdd(client, peer);
    }

    public EfficaxClient GetClient(NetPeer peer)
    {
        return peerClient[peer];
    }

    public NetPeer GetPeer(EfficaxClient client)
    {
        return clientPeer[client];
    }

    public void RemoveClient(NetPeer peer)
    {
        clientPeer.TryRemove(GetClient(peer), out _);
        peerClient.TryRemove(peer, out _);
    }

    public void RemovePeer(EfficaxClient client)
    {
        peerClient.TryRemove(GetPeer(client), out _);
        clientPeer.TryRemove(client, out _);
    }
}