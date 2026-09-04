namespace AlliED.Impl.Structures;

internal class S0xTieBriefing
{
    public const int Size = 0x5DE4;

    /* 0x0000 */
    public S0xTieBriefingData BriefingData = new();
    /* 0x5DDC */
    public TStrings BriefingTags = new();
    /* 0x5DE0 */
    public TStrings BriefingStrings = new();

    public S0xTieBriefing Clone()
    {
        var obj = new S0xTieBriefing();
        obj.BriefingData = S0xTieBriefingData.FromByteArray(BriefingData.ToByteArray());
        obj.BriefingTags = new TStrings(BriefingTags.Items);
        obj.BriefingStrings = new TStrings(BriefingStrings.Items);
        return obj;
    }
}
