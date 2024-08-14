using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x3f, "Bit/Val Get/Set")]
public class Op3F : IOpControl
{
    public static string ToCommand(BinaryReader reader, StringMessage message)
    {
        var idx = reader.ReadValue();
        var data = reader.ReadValue();

        return "Bit/Val Get/Set, Val Div, Command: 3F, Arguments: " + idx + ", " + data;
    }
}
