namespace EfficaxServer.Network; //{}

public class NetworkOutputHandler
{
    private readonly ConcurrentQueue<NetworkOutput> queuedPackets = new();
    private readonly ManualResetEvent canQueue = new(true);
    private readonly ManualResetEvent queuedPacket = new(false);

    private readonly BitWriter writer = new(128, 64);

    private bool shuttingDown = false;

    public void Start()
    {
        if (shuttingDown) throw new Exception("Cannot restart!");
        while (true)
        {
            queuedPacket.WaitOne();
            if (shuttingDown)
            {
                canQueue.Dispose();
                queuedPacket.Dispose();
                break;
            }
            while (!queuedPackets.IsEmpty)
            {
                if (queuedPackets.TryDequeue(out NetworkOutput networkOutput))
                {
                    networkOutput.Send(writer);
                    writer.Reset();
                }
            }
            canQueue.Reset();
            if (queuedPackets.IsEmpty && !shuttingDown) queuedPacket.Reset();
            canQueue.Set();
        }
    }

    public void Stop()
    {
        shuttingDown = true;
        queuedPacket.Set();
    }

    public void Send(NetworkOutput networkOutput)
    {
        canQueue.WaitOne();
        queuedPackets.Enqueue(networkOutput);
        queuedPacket.Set();
    }

    public void Send(NetPeer peer, Packet packet, DeliveryMethod deliveryMethod)
    {
        Send(new NetworkOutput(peer, packet, deliveryMethod));
    }
}