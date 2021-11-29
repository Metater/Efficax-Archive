namespace Efficax.Server.Network.Out; //{}

public class NetOutDispatcher
{
    private ChannelWriter<OutboundData> channelWriter;

    private readonly BitWriter bitWriter = new(128, 64);

    public Task Start()
    {
        var channel = Channel.CreateUnbounded<OutboundData>(new UnboundedChannelOptions() { SingleReader = true });
        var reader = channel.Reader;
        channelWriter = channel.Writer;
        return Task.Run(async () =>
        {
            while (await reader.WaitToReadAsync())
            {
                while (reader.TryRead(out var outboundData))
                {
                    outboundData.Send(bitWriter);
                }
            }
        });
    }

    public void Send(OutboundData outboundData)
    {
        channelWriter.TryWrite(outboundData);
    }

    public void Send(NetPeer peer, Packet packet, DeliveryMethod deliveryMethod)
    {
        Send(new OutboundNetPeerPacket(peer, packet, deliveryMethod));
    }

    public void Stop()
    {
        channelWriter.Complete();
    }
}