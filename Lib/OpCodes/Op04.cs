using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x04, "Text, Mess Window Clean??")]
public class Op04 : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var subCommand = reader.ReadByte();

        switch (subCommand)
        {
            case 0x01:
                return "Text, Hide Window, Command: 04 01";
            case 0x04:
                return "Text, Wait Click to Clean Buffer, Command: 04 04";
            case 0x05:
                return "Text, Clear, Command: 04 05";
            default:
                throw new Exception(
                    $"Position: {reader.Now()}, Unknown Command: 04 {subCommand:X2}"
                );
        }
    }
}
