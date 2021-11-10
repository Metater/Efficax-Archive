namespace EfficaxServer.Network.Out; //{}

public abstract class OutboundData
{
    public OutboundDataType Type { get; protected set; }

    public abstract void Send(BitWriter writer);
}