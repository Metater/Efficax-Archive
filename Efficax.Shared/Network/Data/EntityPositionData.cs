﻿using BitManipulation;

namespace Efficax.Shared.Network.Data
{
    public struct EntityPositionData : IData<EntityPositionData>
    {
        // byte layer entity is on?
        public float x, y;

        public EntityPositionData(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public EntityPositionData ReadIn(BitReader reader)
        {
            x = (((float)reader.GetUShort()) - (1 << 15)) / 256f;
            y = (((float)reader.GetUShort()) - (1 << 15)) / 256f;
            return this;
        }

        public EntityPositionData WriteOut(BitWriter writer)
        {
            writer.Put((ushort)((x * 256f) + (1 << 15)));
            writer.Put((ushort)((y * 256f) + (1 << 15)));
            return this;
        }
    }
}
