using BitManipulation;

namespace EfficaxShared.Network.Packets
{
    public abstract class Packet
    {
        public PacketType Type { get; protected set; }

        public abstract void WriteOut(BitWriter writer);
    }
}