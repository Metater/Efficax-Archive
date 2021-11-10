namespace EfficaxShared.Network.Packets
{
    public enum PacketType : ushort
    {
        // Server bound
        ClientInput,
        // Client bound

        // Shared
        GroupedPacket,
    }
}