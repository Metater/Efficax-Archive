namespace EfficaxServer.Network; //{}

public class NetworkOutputHandler
{
    private readonly ConcurrentQueue<NetworkOutput> queuedPackets = new();
    private readonly ManualResetEvent canQueue = new(true);
    private readonly ManualResetEvent queuedPacket = new(false);

    private readonly BitWriter writer = new(128, 64);

    private bool shuttingDown = false;

    public int resets = 0;
    public int sends = 0;

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
                    //networkOutput.Send(writer);
                    int s = 0;
                    for (int i = 0; i < 100000; i++) s += i;
                    sends++;
                    writer.Reset();
                }
                if (sends == 1001) throw new Exception("Done");
            }
            canQueue.Reset();
            if (queuedPackets.IsEmpty && !shuttingDown)
            {
                queuedPacket.Reset();
                resets++;
            }
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