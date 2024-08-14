using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x1C, "Just Jump")]
public class Op1C : IOpControl
{
    public static string ToCommand(BinaryReader reader, StringMessage message)
    {
        var ptr = reader.ReadInt32Le();

        return "Just Jump, Jump, Command: 1C, Arguments: " + ptr;
    }
}
