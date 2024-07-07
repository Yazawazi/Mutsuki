using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x16, "Jump/Call Other SEEN")]
public class Op16 : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var subCommand = reader.ReadByte();
        var ptr = reader.ReadValue();

        return subCommand != 1
            ? $"Jump/Call Other SEEN, Call, Command: 16 {subCommand:X2}, Arguments: {ptr}"
            : $"Jump/Call Other SEEN, Jump, Command: 16 {subCommand:X2}, Arguments: {ptr}";
    }
}
