namespace AlliED.Impl.Structures;

internal class S0xTieTrigger
{
    public const int Size = 0x0006;

    /* 0x0000 */
    public TieConditionEnum Condition;
    /* 0x0001 */
    public TieClassEnum VariableType;
    /* 0x0002 */
    public byte Variable;
    /* 0x0003 */
    public TieAmountEnum Amount;
    /* 0x0004 */
    public short Parameter;

    public static S0xTieTrigger FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var trigger = new S0xTieTrigger();
        trigger.Condition = (TieConditionEnum)array[0x00];
        trigger.VariableType = (TieClassEnum)array[0x01];
        trigger.Variable = array[0x02];
        trigger.Amount = (TieAmountEnum)array[0x03];
        trigger.Parameter = BitConverter.ToInt16(array, 0x04);
        return trigger;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        array[0x00] = (byte)Condition;
        array[0x01] = (byte)VariableType;
        array[0x02] = Variable;
        array[0x03] = (byte)Amount;
        BitConverter.GetBytes(Parameter).CopyTo(array, 0x04);
        return array;
    }
}
