using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x20, "Subroutine Return")]
public class Op20 : IOpControl
{
    public static string ToCommand(BinaryReader binaryReader, Header header)
    {
        var subCommand = binaryReader.ReadByte();

        switch (subCommand)
        {
            case 0x01:
                return "Subroutine Return, Return in the Same Seen, Command: 20 01";
            case 0x02:
                return "Subroutine Return, Return to the Other Seen, Command: 20 02";
            case 0x06:
                return "Subroutine Return, Clear Stack, Command: 20 06";
            default:
                throw new Exception(
                    $"Position: {binaryReader.Now()}, Unknown Command: 20 {subCommand:X2}"
                );
        }
    }
}
