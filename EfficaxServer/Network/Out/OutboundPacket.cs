namespace EfficaxServer.Network.Out; //{}

public sealed class OutboundPacket
{
    public readonly NetPeer receiver;
    public readonly Packet packet;
    public readonly DeliveryMethod deliveryMethod;

    public OutboundPacket(NetPeer receiver, Packet packet, DeliveryMethod deliveryMethod)
    {
        this.receiver = receiver;
        this.packet = packet;
        this.deliveryMethod = deliveryMethod;
    }

    public void Send(BitWriter writer)
    {
        packet.WriteOut(writer);
        receiver?.Send(writer.Assemble(), deliveryMethod);
        writer.Reset();
    }
}