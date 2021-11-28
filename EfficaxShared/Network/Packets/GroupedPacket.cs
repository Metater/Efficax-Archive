using BitManipulation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxShared.Network.Packets
{
    public class GroupedPacket : Packet
    {
        private readonly List<Packet> packets = new List<Packet>();

        public GroupedPacket()
        {
            Type = PacketType.GroupedPacket;
        }

        public override Packet ReadIn(BitReader reader)
        {
            int packetsCount = reader.GetByte() + 1;
            for (int i = 0; i < packetsCount; i++)
                packets.Add(GetPacket(reader));
            return this;
        }

        public override Packet WriteOut(BitWriter writer)
        {
            writer.Put(Type);
            if (packets.Count <= 0 || packets.Count > 256)
                throw new Exception("Cannot form group packets with more than 256 packets");
            writer.Put((byte)(packets.Count - 1));
            foreach (Packet packet in packets)
                packet.WriteOut(writer);
            return this;
        }
    }
}
