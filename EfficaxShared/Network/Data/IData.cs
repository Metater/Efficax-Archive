using BitManipulation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxShared.Network.Data
{
    public interface IData<TData>
    {
        TData ReadIn(BitReader reader);
        TData WriteOut(BitWriter writer);
    }
}
