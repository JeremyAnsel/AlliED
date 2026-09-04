using AlliED.Extensions;

namespace AlliED.Impl.Structures;

internal class S0xTieRadioMessage
{
    public const int Size = 0x00A2;

    /* 0x0000 */
    public short Id;
    /* 0x0002 */
    private string _Message = string.Empty;
    public string Message { get => _Message; set => _Message = value.WithMaxLength(80); }
    /* 0x0052 */
    public byte[] ForTeam = new byte[10];
    /* 0x005C */
    public S0xTieTriggers Condition = new();
    /* 0x007C */
    public byte[] M00007C = new byte[8];
    /* 0x0084 */
    public int Fg;
    /* 0x0088 */
    public int MessageType;
    /* 0x008C */
    public byte TimePassed;
    /* 0x008D */
    public bool Operator;
    /* 0x008E */
    public byte Side;
    /* 0xF00008F */
    public byte[] unk00008F = new byte[1];
    /* 0x0090 */
    public S0xTieTrigger Trigger1 = new();
    /* 0x0096 */
    public S0xTieTrigger Trigger2 = new();
    /* 0xF00009C */
    public byte[] unk00009C = new byte[2];
    /* 0x009E */
    public byte TriggersOperator;
    /* 0xF00009F */
    public byte[] unk00009F = new byte[3];

    public static S0xTieRadioMessage FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var message = new S0xTieRadioMessage();
        message.Id = BitConverter.ToInt16(array, 0x00);
        message.Message = array.ReadFixedLengthString(0x02, 80);
        message.ForTeam = array.Subarray(0x52, 10);
        message.Condition = S0xTieTriggers.FromByteArray(array.Subarray(0x5C, 0x20));
        message.M00007C = array.Subarray(0x7C, 8);
        message.Fg = BitConverter.ToInt32(array, 0x84);
        message.MessageType = BitConverter.ToInt32(array, 0x88);
        message.TimePassed = array[0x8C];
        message.Operator = array[0x8D] != 0;
        message.Side = array[0x8E];
        array.ReadUnknown(0x8F, message.unk00008F);
        message.Trigger1 = S0xTieTrigger.FromByteArray(array.Subarray(0x90, 0x06));
        message.Trigger2 = S0xTieTrigger.FromByteArray(array.Subarray(0x96, 0x06));
        array.ReadUnknown(0x9C, message.unk00009C);
        message.TriggersOperator = array[0x9E];
        array.ReadUnknown(0x9F, message.unk00009F);
        return message;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        BitConverter.GetBytes(Id).CopyTo(array, 0x00);
        array.WriteFixedLengthString(0x02, Message, 80);
        ForTeam.CopyTo(array, 0x52);
        Condition.ToByteArray().CopyTo(array, 0x5C);
        M00007C.CopyTo(array, 0x7C);
        BitConverter.GetBytes(Fg).CopyTo(array, 0x84);
        BitConverter.GetBytes(MessageType).CopyTo(array, 0x88);
        array[0x8C] = TimePassed;
        array[0x8D] = Operator ? (byte)1 : (byte)0;
        array[0x8E] = Side;
        array.WriteUnknown(0x8F, unk00008F);
        Trigger1.ToByteArray().CopyTo(array, 0x90);
        Trigger2.ToByteArray().CopyTo(array, 0x96);
        array.WriteUnknown(0x9C, unk00009C);
        array[0x9E] = TriggersOperator;
        array.WriteUnknown(0x9F, unk00009F);
        return array;
    }
}
