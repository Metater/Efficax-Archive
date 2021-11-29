using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxShared.DB
{
    public enum DBPacketHeaderCB : byte
    {
        SessionConfirmation,
    }
    public enum DBPacketHeaderSB : byte
    {
        Query,
    }
}
