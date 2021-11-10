using BitManipulation;

namespace EfficaxShared.Network.Packets
{
    public abstract class Packet
    {
        public PacketType Type { get; protected set; }

        public abstract Packet ReadIn(BitReader reader);
        public abstract Packet WriteOut(BitWriter writer);

        public static Packet GetPacket(BitReader reader)
        {
            switch (reader.GetPacketType())
            {
                case PacketType.GroupedPacket:
                    return new GroupedPacket().ReadIn(reader);
                default:
                    return null;
            }
        }
    }
}