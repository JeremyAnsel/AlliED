using AlliED.Helpers;

namespace AlliED.Impl.Structures;

/// <remarks>unknown size</remarks>
internal class S0xXvTGGStrObject
{
    public const int Size = 0x1504;

    /* 0xF000000 */
    public byte[] unk000000 = new byte[4];
    /* 0x0004 */
    public S0xXvTGGStrings[] GGStrings = ArrayHelpers.CreateArray<S0xXvTGGStrings>(28); // unknown size
}
