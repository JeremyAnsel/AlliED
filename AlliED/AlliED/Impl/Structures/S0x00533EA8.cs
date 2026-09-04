using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0x00533EA8
{
    public const int Size = 0x003F;

    /* 0xF000000 */
    public byte[] unk000000 = new byte[63];

    public static S0x00533EA8 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var buffer = new S0x00533EA8();
        array.ReadUnknown(0x00, buffer.unk000000);
        return buffer;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        array.WriteUnknown(0x00, unk000000);
        return array;
    }
}
