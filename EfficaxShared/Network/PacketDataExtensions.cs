using BitManipulation;
using EfficaxShared.Network.Data;
using EfficaxShared.Network.Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxShared.Network
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
