using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x3d, "Bit/Val Get/Set")]
public class Op3D : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var idx = reader.ReadValue();
        var data = reader.ReadValue();

        return "Bit/Val Get/Set, Val Sub, Command: 3D, Arguments: " + idx + ", " + data;
    }
}
