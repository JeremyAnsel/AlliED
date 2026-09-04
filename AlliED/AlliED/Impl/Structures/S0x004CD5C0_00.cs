using AlliED.Extensions;
using AlliED.Helpers;

namespace AlliED.Impl.Structures;

internal class S0x004CD5C0_00
{
    public const int Size = 0x0562;

    /* 0x0000 */
    private string _name = string.Empty;
    public string Name { get => _name; set => _name = value.WithMaxLength(20); }
    /* 0x0014 */
    private string _M000014 = string.Empty;
    public string M000014 { get => _M000014; set => _M000014 = value.WithMaxLength(20); }
    /* 0x0028 */
    private string _Cargo = string.Empty;
    public string Cargo { get => _Cargo; set => _Cargo = value.WithMaxLength(20); }
    /* 0x003C */
    private string _SpecialCargo = string.Empty;
    public string SpecialCargo { get => _SpecialCargo; set => _SpecialCargo = value.WithMaxLength(20); }
    /* 0x0050 */
    public byte[] m000050 = new byte[30];
    /* 0x006E */
    public S0x004CE838 m00006E = new();
    /* 0x0079 */
    public S0x004CE838 m000079 = new();
    /* 0x0084 */
    public bool m000084;
    /* 0x0085 */
    public byte m000085;
    /* 0x0086 */
    public byte m000086;
    /* 0x0087 */
    public byte m000087;
    /* 0x0088 */
    public S0x004CE838 m000088 = new();
    /* 0x0093 */
    public byte[] m000093 = new byte[6];
    /* 0xF000099 */
    public byte[] unk000099 = new byte[1];
    /* 0x009A */
    public byte[] m00009A = new byte[8];
    /* 0x00A2 */
    public S0x004CD5C0_01[] m0000A2 = ArrayHelpers.CreateArray<S0x004CD5C0_01>(4);
    /* 0x01EA */
    public S0x004CE838 m0001EA = new();
    /* 0x01F5 */
    public S0x004CD5C0_03[] m0001F5 = ArrayHelpers.CreateArray<S0x004CD5C0_03>(8);
    /* 0x0465 */
    public byte m000465;
    /* 0x0466 */
    public S0x004CD5C0_02[] m000466 = ArrayHelpers.CreateArray<S0x004CD5C0_02>(4);
    /* 0x0516 */
    public byte[] m000516 = new byte[4];
    /* 0x051A */
    public byte[] m00051A = new byte[14];
    /* 0x0528 */
    public byte[] m000528 = new byte[8];
    /* 0x0530 */
    public byte[] OptionalWarheads = new byte[8];
    /* 0x0538 */
    public byte[] OptionalBeams = new byte[6];
    /* 0x053E */
    public byte[] OptionalCounterMeasures = new byte[4];
    /* 0x0542 */
    public byte OptionalCraftCategory;
    /* 0x0543 */
    public CraftIdEnum[] OptionalCraftsId = new CraftIdEnum[8];
    /* 0xF00054B */
    public byte[] unk000554B = new byte[2];
    /* 0x054D */
    public byte[] OptionalCraftsCount = new byte[6];
    /* 0xF000553 */
    public byte[] unk000553 = new byte[4];
    /* 0x0557 */
    public byte[] OptionalCraftsWaves = new byte[4];
    /* 0xF00055B */
    public byte[] unk00055B = new byte[6];
    /* 0x0561 */
    public byte m000561;

    public static S0x004CD5C0_00 FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var buffer = new S0x004CD5C0_00();
        buffer.Name = array.ReadFixedLengthString(0x000, 20);
        buffer.M000014 = array.ReadFixedLengthString(0x014, 20);
        buffer.Cargo = array.ReadFixedLengthString(0x028, 20);
        buffer.SpecialCargo = array.ReadFixedLengthString(0x03C, 20);
        buffer.m000050 = array.Subarray(0x050, 30);
        buffer.m00006E = S0x004CE838.FromByteArray(array.Subarray(0x06E, 0x0B));
        buffer.m000079 = S0x004CE838.FromByteArray(array.Subarray(0x079, 0x0B));
        buffer.m000084 = array[0x084] != 0;
        buffer.m000085 = array[0x085];
        buffer.m000086 = array[0x086];
        buffer.m000087 = array[0x087];
        buffer.m000088 = S0x004CE838.FromByteArray(array.Subarray(0x088, 0x0B));
        buffer.m000093 = array.Subarray(0x093, 6);
        array.ReadUnknown(0x099, buffer.unk000099);
        buffer.m00009A = array.Subarray(0x09A, 8);
        for (int i = 0; i < 4; i++)
        {
            buffer.m0000A2[i] = S0x004CD5C0_01.FromByteArray(array.Subarray(0x0A2 + i * 0x52, 0x52));
        }
        buffer.m0001EA = S0x004CE838.FromByteArray(array.Subarray(0x1EA, 0x0B));
        for (int i = 0; i < 8; i++)
        {
            buffer.m0001F5[i] = S0x004CD5C0_03.FromByteArray(array.Subarray(0x1F5 + i * 0x4E, 0x4E));
        }
        buffer.m000465 = array[0x465];
        for (int i = 0; i < 4; i++)
        {
            buffer.m000466[i] = S0x004CD5C0_02.FromByteArray(array.Subarray(0x466 + i * 0x2C, 0x2C));
        }
        buffer.m000516 = array.Subarray(0x516, 4);
        buffer.m00051A = array.Subarray(0x51A, 14);
        buffer.m000528 = array.Subarray(0x528, 8);
        buffer.OptionalWarheads = array.Subarray(0x530, 8);
        buffer.OptionalBeams = array.Subarray(0x538, 6);
        buffer.OptionalCounterMeasures = array.Subarray(0x53E, 4);
        buffer.OptionalCraftCategory = array[0x542];
        for (int i = 0; i < 8; i++)
        {
            buffer.OptionalCraftsId[i] = (CraftIdEnum)array[0x543 + i];
        }
        array.ReadUnknown(0x54B, buffer.unk000554B);
        buffer.OptionalCraftsCount = array.Subarray(0x54D, 6);
        array.ReadUnknown(0x553, buffer.unk000553);
        buffer.OptionalCraftsWaves = array.Subarray(0x557, 4);
        array.ReadUnknown(0x55B, buffer.unk00055B);
        buffer.m000561 = array[0x561];
        return buffer;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        array.WriteFixedLengthString(0x000, Name, 20);
        array.WriteFixedLengthString(0x014, M000014, 20);
        array.WriteFixedLengthString(0x028, Cargo, 20);
        array.WriteFixedLengthString(0x03C, SpecialCargo, 20);
        m000050.CopyTo(array, 0x050);
        m00006E.ToByteArray().CopyTo(array, 0x06E);
        m000079.ToByteArray().CopyTo(array, 0x079);
        array[0x084] = m000084 ? (byte)1 : (byte)0;
        array[0x085] = m000085;
        array[0x086] = m000086;
        array[0x087] = m000087;
        m000088.ToByteArray().CopyTo(array, 0x088);
        m000093.CopyTo(array, 0x093);
        array.WriteUnknown(0x099, unk000099);
        m00009A.CopyTo(array, 0x09A);
        for (int i = 0; i < 4; i++)
        {
            m0000A2[i].ToByteArray().CopyTo(array, 0x0A2 + i * 0x52);
        }
        m0001EA.ToByteArray().CopyTo(array, 0x1EA);
        for (int i = 0; i < 8; i++)
        {
            m0001F5[i].ToByteArray().CopyTo(array, 0x1F5 + i * 0x4E);
        }
        array[0x465] = m000465;
        for (int i = 0; i < 4; i++)
        {
            m000466[i].ToByteArray().CopyTo(array, 0x466 + i * 0x2C);
        }
        m000516.CopyTo(array, 0x516);
        m00051A.CopyTo(array, 0x51A);
        m000528.CopyTo(array, 0x528);
        OptionalWarheads.CopyTo(array, 0x530);
        OptionalBeams.CopyTo(array, 0x538);
        OptionalCounterMeasures.CopyTo(array, 0x53E);
        array[0x542] = OptionalCraftCategory;
        for (int i = 0; i < 8; i++)
        {
            array[0x543 + i] = (byte)OptionalCraftsId[i];
        }
        array.WriteUnknown(0x54B, unk000554B);
        OptionalCraftsCount.CopyTo(array, 0x54D);
        array.WriteUnknown(0x553, unk000553);
        OptionalCraftsWaves.CopyTo(array, 0x557);
        array.WriteUnknown(0x55B, unk00055B);
        array[0x561] = m000561;
        return array;
    }
}
