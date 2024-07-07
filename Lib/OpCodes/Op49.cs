using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x49, "Bit/Val Get/Set")]
public class Op49 : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var idx = reader.ReadValue();
        var data = reader.ReadValue();

        return "Bit/Val Get/Set, Val Set With Another Val, Command: 49, Arguments: "
            + idx
            + ", "
            + data;
    }
}
