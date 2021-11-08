namespace EfficaxServer.Network; //{}

public class NetworkOutputHandler
{
    private ConcurrentQueue<NetworkOutput> queuedPackets = new ConcurrentQueue<NetworkOutput>();
    private ManualResetEvent queuedPacket = new ManualResetEvent(false);

    public NetworkOutputHandler()
    {

    }

    public void Start()
    {
        while (true)
        {
            queuedPackets.WaitOne();
            while (!queuedPackets.IsEmpty)
            {
                // do double check before resetting event
            }
        }
    }

    public void Send(NetworkOutput networkOutput)
    {
        queuedPackets.Enqueue(networkOutput);
    }

    public void Send(NetPeer peer, Packet packet, DeliveryMethod deliveryMethod)
    {
        Send(new NetworkOutput(peer, packet, deliveryMethod));
    }
}