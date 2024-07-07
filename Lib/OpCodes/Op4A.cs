using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x4a, "Bit/Val Get/Set")]
public class Op4A : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var idx = reader.ReadValue();
        var data = reader.ReadValue();

        return "Bit/Val Get/Set, Val Add With Another Val, Command: 4A, Arguments: "
            + idx
            + ", "
            + data;
    }
}
