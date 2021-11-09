namespace EfficaxServer.Network.Out; //{}

public sealed class OutboundData
{
    public OutboundDataType Type { get; protected set; }

    public abstract Send(BitWriter writer);
}