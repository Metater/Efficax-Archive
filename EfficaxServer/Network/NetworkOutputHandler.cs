namespace EfficaxServer.Network; //{}

public class NetworkOutputHandler
{
    private ChannelWriter<NetworkOutput> channelWriter;

    private readonly BitWriter bitWriter = new(128, 64);

    public void Start()
    {
        var channel = Channel.CreateUnbounded<NetworkOutput>(new UnboundedChannelOptions() { SingleReader = true });
        var reader = channel.Reader;
        channelWriter = channel.Writer;
        Task.Run(async () =>
        {
            while (await reader.WaitToReadAsync())
            {
                while (reader.TryRead(out var networkOutput))
                {
                    networkOutput.Send(bitWriter);
                }
            }
        });
    }

    public void Send(NetworkOutput networkOutput)
    {
        channelWriter.TryWrite(networkOutput);
    }

    public void Send(NetPeer peer, Packet packet, DeliveryMethod deliveryMethod)
    {
        Send(new NetworkOutput(peer, packet, deliveryMethod));
    }

    public void Stop()
    {
        channelWriter.Complete();
    }
}