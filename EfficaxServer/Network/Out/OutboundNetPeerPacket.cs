namespace EfficaxServer.Network.Out; //{}

public sealed class OutboundNetPeerPacket : OutboundData
{
    public readonly NetPeer receiver;
    public readonly Packet packet;
    public readonly DeliveryMethod deliveryMethod;

    public OutboundPacket(NetPeer receiver, Packet packet, DeliveryMethod deliveryMethod)
    {
        Type = OutboundDataType.NetPeerPacket;
        this.receiver = receiver;
        this.packet = packet;
        this.deliveryMethod = deliveryMethod;
    }

    public override void Send(BitWriter writer)
    {
        packet.WriteOut(writer);
        receiver?.Send(writer.Assemble(), deliveryMethod);
        writer.Reset();
    }
}