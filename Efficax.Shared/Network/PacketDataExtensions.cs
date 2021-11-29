using BitManipulation;
using Efficax.Shared.Network.Packets;

namespace Efficax.Shared.Network
{
    public static class PacketDataExtensions
    {
        public static void Put(this BitWriter bitWriter, PacketType packetType)
        {
            bitWriter.Put((ushort)packetType);
        }
        public static PacketType GetPacketType(this BitReader bitReader)
        {
            return (PacketType)bitReader.GetUShort();
        }
    }
}
