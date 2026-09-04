using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0xTieGlobalGoal
{
    public const int Size = 0x007A;

    /* 0x0000 */
    public S0xTieTriggers Triggers = new();
    /* 0x0020 */
    private string _name = string.Empty;
    public string Name { get => _name; set => _name = value.WithMaxLength(16); }
    /* 0x0030 */
    public byte Version;
    /* 0x0031 */
    public bool Op;
    /* 0x0032 */
    public byte TimePassed;
    /* 0x0033 */
    public byte Points;
    /* 0x0034 */
    public byte[] m000034 = new byte[70];

    public static S0xTieGlobalGoal FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var goal = new S0xTieGlobalGoal();
        goal.Triggers = S0xTieTriggers.FromByteArray(array.Subarray(0, 0x20));
        goal.Name = array.ReadFixedLengthString(0x20, 16);
        goal.Version = array[0x30];
        goal.Op = array[0x31] != 0;
        goal.TimePassed = array[0x32];
        goal.Points = array[0x33];
        array.ReadUnknown(0x34, goal.m000034);
        return goal;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        Triggers.ToByteArray().CopyTo(array, 0x00);
        array.WriteFixedLengthString(0x20, Name, 16);
        array[0x30] = Version;
        array[0x31] = Op ? (byte)1 : (byte)0;
        array[0x32] = TimePassed;
        array[0x33] = Points;
        array.WriteUnknown(0x34, m000034);
        return array;
    }
}
