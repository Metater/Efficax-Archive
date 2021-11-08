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
}