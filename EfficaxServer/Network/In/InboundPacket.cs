namespace EfficaxServer.Network.In; //{}

public sealed class OutboundPacket
{
    public readonly NetPeer sender;
    public readonly Packet packet;
    public readonly DeliveryMethod deliveryMethod;

    public OutboundPacket(NetPeer sender, Packet packet, DeliveryMethod deliveryMethod)
    {
        this.receiver = receiver;
        this.packet = packet;
        this.deliveryMethod = deliveryMethod;
    }
}