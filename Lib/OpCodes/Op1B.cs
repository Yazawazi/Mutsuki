using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x1B, "Inseen Jump")]
public class Op1B : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var ptr = reader.ReadInt32Le();

        return "Inseen Jump, Jump, Command: 1B, Arguments: " + ptr;
    }
}
