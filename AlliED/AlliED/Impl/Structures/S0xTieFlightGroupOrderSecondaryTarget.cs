namespace AlliED.Impl.Structures;

internal class S0xTieFlightGroupOrderSecondaryTarget
{
    public const int Size = 0x0006;

    /* 0x0000 */
    public TieClassEnum ClassA;
    /* 0x0001 */
    public TieClassEnum ClassB;
    /* 0x0002 */
    public byte ParameterA;
    /* 0x0003 */
    public byte ParameterB;
    /* 0x0004 */
    public byte Operator;
    /* 0x0005 */
    public byte m000005;

    public static S0xTieFlightGroupOrderSecondaryTarget FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var target = new S0xTieFlightGroupOrderSecondaryTarget();
        target.ClassA = (TieClassEnum)array[0x00];
        target.ClassB = (TieClassEnum)array[0x01];
        target.ParameterA = array[0x02];
        target.ParameterB = array[0x03];
        target.Operator = array[0x04];
        target.m000005 = array[0x05];
        return target;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        array[0x00] = (byte)ClassA;
        array[0x01] = (byte)ClassB;
        array[0x02] = ParameterA;
        array[0x03] = ParameterB;
        array[0x04] = Operator;
        array[0x05] = m000005;
        return array;
    }
}
