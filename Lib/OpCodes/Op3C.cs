using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x3c, "Bit/Val Get/Set")]
public class Op3C : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var idx = reader.ReadValue();
        var data = reader.ReadValue();

        return "Bit/Val Get/Set, Val Add, Command: 3C, Arguments: " + idx + ", " + data;
    }
}
