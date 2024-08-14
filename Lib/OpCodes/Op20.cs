using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x20, "Subroutine Return")]
public class Op20 : IOpControl
{
    public static string ToCommand(BinaryReader binaryReader, StringMessage message)
    {
        var subCommand = binaryReader.ReadByte();

        return subCommand switch
        {
            0x01 => "Subroutine Return, Return in the Same Seen, Command: 20 01",
            0x02 => "Subroutine Return, Return to the Other Seen, Command: 20 02",
            0x06 => "Subroutine Return, Clear Stack, Command: 20 06",
            _ => throw new Exception($"Position: {binaryReader.Now()}, Unknown Command: 20 {subCommand:X2}")
        };
    }
}
