using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x02, "Change Line")]
public class Op02 : IOpControl
{
    public static string ToCommand(BinaryReader reader, StringMessage message)
    {
        var subCommand = reader.ReadByte();

        return subCommand switch
        {
            0x01 => "Change Line, No Indent, Command: 02 01",
            0x02 => "Change Line, Keep Indent, Command: 02 02",
            0x03 => "Change Line, Unknown, Command: 02 03",
            _ => throw new Exception($"Position: {reader.Now()}, Unknown Command: 02 {subCommand:X2}")
        };
    }
}
