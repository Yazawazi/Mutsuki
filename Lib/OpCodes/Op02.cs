using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x02, "Change Line")]
public class Op02 : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var subCommand = reader.ReadByte();

        switch (subCommand)
        {
            case 0x01:
                return "Change Line, No Indent, Command: 02 01";
            case 0x02:
                return "Change Line, Keep Indent, Command: 02 02";
            case 0x03:
                return "Change Line, Unknown, Command: 02 03";
            default:
                throw new Exception(
                    $"Position: {reader.Now()}, Unknown Command: 02 {subCommand:X2}"
                );
        }
    }
}
