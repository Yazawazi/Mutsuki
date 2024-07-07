using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x37, "Bit/Val Get/Set")]
public class Op37 : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var idx = reader.ReadValue();
        var data = reader.ReadValue();

        return "Bit/Val Get/Set, Bit Set, Command: 37, Arguments: " + idx + ", " + data;
    }
}
