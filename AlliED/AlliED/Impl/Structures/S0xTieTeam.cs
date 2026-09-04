using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0xTieTeam
{
    public const int Size = 0x01E7;

    /* 0x0000 */
    public short TeamCreated;

    /* 0x0002 */
    private string _Name = string.Empty;
    public string Name { get => _Name; set => _Name = value.WithMaxLength(16); }

    /* 0xF000012 */
    public byte[] unk000012 = new byte[8];

    /* 0x001A */
    public bool[] TeamAllied = new bool[10];

    /* 0x0024 */
    private string _PrimarySuccessMessage1 = string.Empty;
    public string PrimarySuccessMessage1 { get => _PrimarySuccessMessage1; set => _PrimarySuccessMessage1 = value.WithMaxLength(64); }

    /* 0x0064 */
    private string _PrimarySuccessMessage2 = string.Empty;
    public string PrimarySuccessMessage2 { get => _PrimarySuccessMessage2; set => _PrimarySuccessMessage2 = value.WithMaxLength(64); }

    /* 0x00A4 */
    private string _PrimaryFailureMessage1 = string.Empty;
    public string PrimaryFailureMessage1 { get => _PrimaryFailureMessage1; set => _PrimaryFailureMessage1 = value.WithMaxLength(64); }

    /* 0x00E4 */
    private string _PrimaryFailureMessage2 = string.Empty;
    public string PrimaryFailureMessage2 { get => _PrimaryFailureMessage2; set => _PrimaryFailureMessage2 = value.WithMaxLength(64); }

    /* 0x0124 */
    private string _SecondarySuccessMessage1 = string.Empty;
    public string SecondarySuccessMessage1 { get => _SecondarySuccessMessage1; set => _SecondarySuccessMessage1 = value.WithMaxLength(64); }

    /* 0x0164 */
    private string _SecondarySuccessMessage2 = string.Empty;
    public string SecondarySuccessMessage2 { get => _SecondarySuccessMessage2; set => _SecondarySuccessMessage2 = value.WithMaxLength(64); }

    /* 0xF0001A4 */
    public byte[] unk0001A4 = new byte[67];

    public static S0xTieTeam FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var team = new S0xTieTeam();
        team.TeamCreated = BitConverter.ToInt16(array, 0x000);
        team.Name = array.ReadFixedLengthString(0x002, 16);
        array.ReadUnknown(0x012, team.unk000012);
        for (int i = 0; i < 10; i++)
        {
            team.TeamAllied[i] = array[0x01A + i] != 0;
        }
        team.PrimarySuccessMessage1 = array.ReadFixedLengthString(0x024, 64);
        team.PrimarySuccessMessage2 = array.ReadFixedLengthString(0x064, 64);
        team.PrimaryFailureMessage1 = array.ReadFixedLengthString(0x0A4, 64);
        team.PrimaryFailureMessage2 = array.ReadFixedLengthString(0x0E4, 64);
        team.SecondarySuccessMessage1 = array.ReadFixedLengthString(0x124, 64);
        team.SecondarySuccessMessage2 = array.ReadFixedLengthString(0x164, 64);
        array.ReadUnknown(0x1A4, team.unk0001A4);
        return team;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        BitConverter.GetBytes(TeamCreated).CopyTo(array, 0x000);
        array.WriteFixedLengthString(0x002, Name, 16);
        array.WriteUnknown(0x012, unk000012);
        for (int i = 0; i < 10; i++)
        {
            array[0x01A + i] = TeamAllied[i] ? (byte)1 : (byte)0;
        }
        array.WriteFixedLengthString(0x024, PrimarySuccessMessage1, 64);
        array.WriteFixedLengthString(0x064, PrimarySuccessMessage2, 64);
        array.WriteFixedLengthString(0x0A4, PrimaryFailureMessage1, 64);
        array.WriteFixedLengthString(0x0E4, PrimaryFailureMessage2, 64);
        array.WriteFixedLengthString(0x124, SecondarySuccessMessage1, 64);
        array.WriteFixedLengthString(0x164, SecondarySuccessMessage2, 64);
        array.WriteUnknown(0x1A4, unk0001A4);
        return array;
    }
}
