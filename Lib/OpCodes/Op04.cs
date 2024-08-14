using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x04, "Text, Mess Window Clean??")]
public class Op04 : IOpControl
{
    public static string ToCommand(BinaryReader reader, StringMessage message)
    {
        var subCommand = reader.ReadByte();

        return subCommand switch
        {
            0x01 => "Text, Hide Window, Command: 04 01",
            0x04 => "Text, Wait Click to Clean Buffer, Command: 04 04",
            0x05 => "Text, Clear, Command: 04 05",
            _ => throw new Exception($"Position: {reader.Now()}, Unknown Command: 04 {subCommand:X2}")
        };
    }
}
