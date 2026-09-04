using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0xTieHeaderGlobalCargo
{
    public const int Size = 0x008C;

    /* 0x0000 */
    private string _cargo = string.Empty;
    public string Cargo { get => _cargo; set => _cargo = value.WithMaxLength(64); }

    /* 0xF000040 */
    public byte[] unk000040 = new byte[4];

    /* 0x0044 */
    public byte Count;

    /* 0xF000045 */
    public byte[] unk000045 = new byte[3];

    /* 0x0048 */
    public byte CargoType;

    /* 0xF000049 */
    public byte[] unk000049 = new byte[2];

    /* 0x004B */
    public byte Volatility;

    /* 0xF00004C */
    public byte[] unk00004C = new byte[64];

    public static S0xTieHeaderGlobalCargo FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var cargo = new S0xTieHeaderGlobalCargo();
        cargo.Cargo = array.ReadFixedLengthString(0x00, 64);
        array.ReadUnknown(0x40, cargo.unk000040);
        cargo.Count = array[0x44];
        array.ReadUnknown(0x45, cargo.unk000045);
        cargo.CargoType = array[0x48];
        array.ReadUnknown(0x49, cargo.unk000049);
        cargo.Volatility = array[0x4B];
        array.ReadUnknown(0x4C, cargo.unk00004C);
        return cargo;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        array.WriteFixedLengthString(0x00, Cargo, 64);
        array.WriteUnknown(0x40, unk000040);
        array[0x44] = Count;
        array.WriteUnknown(0x45, unk000045);
        array[0x48] = CargoType;
        array.WriteUnknown(0x49, unk000049);
        array[0x4B] = Volatility;
        array.WriteUnknown(0x4C, unk00004C);
        return array;
    }
}
