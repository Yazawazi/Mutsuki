using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x68, "Graphic Effect")]
public class Op68 : IOpControl
{
    public static string ToCommand(BinaryReader reader, StringMessage message)
    {
        var subCommand = reader.ReadByte();

        switch (subCommand)
        {
            case 0x01:
                var dstPdt = reader.ReadValue();
                var r = reader.ReadValue();
                var g = reader.ReadValue();
                var b = reader.ReadValue();
                return "Graphic Effect, Fill Screen With Color, Command: 68 01, Arguments: "
                    + dstPdt
                    + ", "
                    + r
                    + ", "
                    + g
                    + ", "
                    + b;
            case 0x10:
                var flashRed = reader.ReadValue();
                var flashGreen = reader.ReadValue();
                var flashBlue = reader.ReadValue();
                var flashTime = reader.ReadValue();
                var flashCount = reader.ReadValue();
                return "Graphic Effect, Flash Screen, Command: 68 10, Arguments: "
                    + flashRed
                    + ", "
                    + flashGreen
                    + ", "
                    + flashBlue
                    + ", "
                    + flashTime
                    + ", "
                    + flashCount;
            default:
                throw new Exception(
                    $"Position: {reader.Now()}, Unknown Command: 68 {subCommand:X2}"
                );
        }
    }
}
