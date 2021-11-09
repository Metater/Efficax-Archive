namespace EfficaxServer.Network.Out; //{}

public class NetOutDispatcher
{
    private ChannelWriter<OutboundPacket> channelWriter;

    private readonly BitWriter bitWriter = new(128, 64);

    public Task Start()
    {
        var channel = Channel.CreateUnbounded<OutboundPacket>(new UnboundedChannelOptions() { SingleReader = true });
        var reader = channel.Reader;
        channelWriter = channel.Writer;
        return Task.Run(async () =>
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

    public void Send(OutboundPacket outboundPacket)
    {
        channelWriter.TryWrite(outboundPacket);
    }

    public void Send(NetPeer peer, Packet packet, DeliveryMethod deliveryMethod)
    {
        Send(new OutboundPacket(peer, packet, deliveryMethod));
    }

    public void Stop()
    {
        channelWriter.Complete();
    }
}