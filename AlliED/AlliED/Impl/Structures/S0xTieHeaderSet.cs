using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0xTieHeaderSet
{
    public const int Size = 0x0057;

    /* 0x0000 */
    private string _name = string.Empty;
    public string Name { get => _name; set => _name = value.WithMaxLength(64); }

    /* 0xF000040 */
    public byte[] unk000040 = new byte[2];

    /* 0x0042 */
    private string _specialCargoName = string.Empty;
    public string SpecialCargoName { get => _specialCargoName; set => _specialCargoName = value.WithMaxLength(20); }

    /* 0xF000056 */
    public byte[] unk000056 = new byte[1];

    public static S0xTieHeaderSet FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var cargo = new S0xTieHeaderSet();
        cargo.Name = array.ReadFixedLengthString(0x00, 64);
        array.ReadUnknown(0x40, cargo.unk000040);
        cargo.SpecialCargoName = array.ReadFixedLengthString(0x42, 20);
        array.ReadUnknown(0x56, cargo.unk000056);
        return cargo;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        array.WriteFixedLengthString(0x00, Name, 64);
        array.WriteUnknown(0x40, unk000040);
        array.WriteFixedLengthString(0x42, SpecialCargoName, 20);
        array.WriteUnknown(0x56, unk000056);
        return array;
    }
}
