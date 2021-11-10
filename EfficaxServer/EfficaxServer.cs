namespace EfficaxServer; //{}

public class EfficaxServer
{
    public readonly EfficaxServerListener listener;
    public readonly NetManager server;

    public readonly NetOutDispatcher netOutDispatcher = new();

    public readonly PeerClientMap peerClientMap = new();

    public readonly Random random = new();


    public EfficaxServer()
    {
        listener = new(this);
        server = new(listener);
    }

    public void Start(int port)
    {
        server.Start(port);
        netOutDispatcher.Start();
    }

    public void Poll()
    {
        server.PollEvents();
    }

    public void Stop()
    {
        netOutDispatcher.Stop();
        server.Stop();
    }
}

