using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0xTieRegion
{
    public const int Size = 0x0084;

    /* 0x0000 */
    private string _Name = string.Empty;
    public string Name { get => _Name; set => _Name = value.WithMaxLength(64); }

    public static S0xTieRegion FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var region = new S0xTieRegion();
        region.Name = array.ReadFixedLengthString(0x00, 64);
        return region;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        array.WriteFixedLengthString(0x000, Name, 64);
        return array;
    }
}
