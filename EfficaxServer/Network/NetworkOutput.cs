namespace EfficaxServer.Network; //{}

public sealed class NetworkOutput
{
    public readonly NetPeer peer;
    public readonly Packet packet;
    public readonly DeliveryMethod deliveryMethod;

    public NetworkOutput(NetPeer peer, Packet packet, DeliveryMethod deliveryMethod)
    {
        this.peer = peer;
        this.packet = packet;
        this.deliveryMethod = deliveryMethod;
    }

    public void Send(BitWriter writer)
    {
        packet.WriteOut(writer);
        peer.Send(writer.Assemble(), deliveryMethod);
        writer.Reset();
    }
}