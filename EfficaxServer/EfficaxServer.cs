namespace EfficaxServer;

public class EfficaxServer
{
    public readonly EfficaxServerListener listener;
    public readonly NetManager server;

    public readonly PeerClientMap peerClientMap = new();

    public readonly Random random = new();


    public EfficaxServer()
    {
        listener = new(this);
        server = new(listener);
        server.Start(12733);
    }
}

