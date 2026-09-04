using AlliED.Extensions;
using AlliED.Helpers;

namespace AlliED.Impl.Structures;

internal class S0x005346AC
{
    public const int Size = 0x0030;

    /* 0x0000 */
    public TFixedString[] M000000 = ArrayHelpers.CreateArray(4, () => new TFixedString(12));

    public static S0x005346AC FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var buffer = new S0x005346AC();
        for (int i = 0; i < 4; i++)
        {
            buffer.M000000[i].Text = array.ReadFixedLengthString(0x00 + i * 0x0C, 12);
        }
        return buffer;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        for (int i = 0; i < 4; i++)
        {
            array.WriteFixedLengthString(0x00 + i * 0x0C, M000000[i].Text, 12);
        }
        return array;
    }
}
