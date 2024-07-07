using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x17, "Screen Shake")]
public class Op17 : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var subCommand = reader.ReadByte();

        if (subCommand == 0x01)
        {
            var idx = reader.ReadValue();
            return "Screen Shake, Shake, Command: 17 01, Arguments: " + idx;
        }

        return $"Screen Shake, Unknown Command (continue): 17 {subCommand:X2}";
    }
}
