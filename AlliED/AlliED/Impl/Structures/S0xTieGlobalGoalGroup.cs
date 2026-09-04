using AlliED.Extensions;
using AlliED.Helpers;

namespace AlliED.Impl.Structures;

internal class S0xTieGlobalGoalGroup
{
    public const int Size = 0x0170;

    /* 0x0000 */
    public short Count;
    /* 0x0002 */
    public S0xTieGlobalGoal[] GlobalGoals = ArrayHelpers.CreateArray<S0xTieGlobalGoal>(3);

    public static S0xTieGlobalGoalGroup FromByteArray(byte[] array)
    {
        if (array.Length != Size)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        var group = new S0xTieGlobalGoalGroup();
        group.Count = BitConverter.ToInt16(array, 0x00);
        for (int i = 0; i < 3; i++)
        {
            group.GlobalGoals[i] = S0xTieGlobalGoal.FromByteArray(array.Subarray(0x02 + i * 0x7A, 0x7A));
        }
        return group;
    }

    public byte[] ToByteArray()
    {
        var array = new byte[Size];
        BitConverter.GetBytes(Count).CopyTo(array, 0x00);
        for (int i = 0; i < 3; i++)
        {
            GlobalGoals[i].ToByteArray().CopyTo(array, 0x02 + i * 0x7A);
        }
        return array;
    }
}
