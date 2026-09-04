using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0x004CD5C0_01
{
    public const int Size = 0x0052;

    /* 0x0000 */
    public byte[] m000000 = new byte[19];
    /* 0x0013 */
    public S0x00533EA8 m000013 = new();

    public static S0x004CD5C0_01 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var buffer = new S0x004CD5C0_01();
        buffer.m000000 = array.Subarray(0x00, 19);
        buffer.m000013 = S0x00533EA8.FromByteArray(array.Subarray(0x13, 0x3F));
        return buffer;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        m000000.CopyTo(array, 0x00);
        m000013.ToByteArray().CopyTo(array, 0x13);
        return array;
    }
}
