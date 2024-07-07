using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x64, "Area Actions")]
public class Op64 : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var subCommand = reader.ReadByte();

        switch (subCommand)
        {
            case 0x20:
                var srcX1 = reader.ReadValue();
                var srcY1 = reader.ReadValue();
                var srcX2 = reader.ReadValue();
                var srcY2 = reader.ReadValue();
                var srcPdt = reader.ReadValue();
                return "Area Actions, Make Mono Image, Command: 64 20, Arguments: "
                    + srcX1
                    + ", "
                    + srcY1
                    + ", "
                    + srcX2
                    + ", "
                    + srcY2
                    + ", "
                    + srcPdt;
            default:
                throw new Exception(
                    $"Position: {reader.Now()}, Unknown Command: 64 {subCommand:X2}"
                );
        }
    }
}
