using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x3e, "Bit/Val Get/Set")]
public class Op3E : IOpControl
{
    public static string ToCommand(BinaryReader reader, StringMessage message)
    {
        var idx = reader.ReadValue();
        var data = reader.ReadValue();

        return "Bit/Val Get/Set, Val Mul, Command: 3E, Arguments: " + idx + ", " + data;
    }
}
