namespace EfficaxServer.Network; //{}

public class EfficaxServerListener : INetEventListener
{
    private readonly EfficaxServer efficaxServer;

    public EfficaxServerListener(EfficaxServer efficaxServer)
    {
        this.efficaxServer = efficaxServer;
    }

    public void OnConnectionRequest(ConnectionRequest request)
    {
        
    }

    public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
    {

    }

    public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
    {

    }

    public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
    {

    }

    public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {

    }

    public void OnPeerConnected(NetPeer peer)
    {
        // Get auth server later
        byte[] buf = new byte[8];
        efficaxServer.random.NextBytes(buf);
        ulong guid = BitConverter.ToUInt64(buf);

        efficaxServer.peerClientMap.Add(peer, new EfficaxClient(efficaxServer, guid, peer));
    }

    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {

    }
}