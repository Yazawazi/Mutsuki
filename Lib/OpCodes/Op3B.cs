using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x3b, "Bit/Val Get/Set")]
public class Op3B : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var idx = reader.ReadValue();
        var data = reader.ReadValue();

        return "Bit/Val Get/Set, Val Set, Command: 3B, Arguments: " + idx + ", " + data;
    }
}
