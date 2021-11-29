using BitManipulation;

namespace Efficax.Shared.Network.Data
{
    public interface IData<TData>
    {
        TData ReadIn(BitReader reader);
        TData WriteOut(BitWriter writer);
    }
}
