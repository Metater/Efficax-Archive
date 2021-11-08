namespace EfficaxServer.Network; //{}

public class PeerClientMap
{
    private readonly Dictionary<NetPeer, EfficaxClient> peerClient = new();
    private readonly Dictionary<EfficaxClient, NetPeer> clientPeer = new();

    public void Add(NetPeer peer, EfficaxClient client)
    {
        peerClient.Add(peer, client);
        clientPeer.Add(client, peer);
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
        clientPeer.Remove(GetClient(peer));
        peerClient.Remove(peer);
    }

    public void RemovePeer(EfficaxClient client)
    {
        peerClient.Remove(GetPeer(client));
        clientPeer.Remove(client);
    }
}