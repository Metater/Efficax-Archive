namespace EfficaxServer.Network.Out; //{}

public sealed class OutboundData
{
    // Eventually encapsulate data that will be sent to places other than a NetPeer
    // Other places: Auth Server, DB, Other Servers, Market Servers

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