using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x13, "Fade In/Fade Out")]
public class Op13 : IOpControl
{
    public static string ToCommand(BinaryReader binaryReader, Header header)
    {
        var subCommand = binaryReader.ReadByte();

        switch (subCommand)
        {
            case 0x04:
                var fadeRed = binaryReader.ReadValue();
                var fadeGreen = binaryReader.ReadValue();
                var fadeBlue = binaryReader.ReadValue();
                var fadeStep = binaryReader.ReadValue();
                return "Fade In/Out, Time, Command: 13 04, Arguments: "
                    + fadeRed
                    + ", "
                    + fadeGreen
                    + ", "
                    + fadeBlue
                    + ", "
                    + fadeStep;
            case 0x10:
                var idx = binaryReader.ReadValue();
                return "Fade In/Out, Fill Screen, Command: 13 10, Arguments: " + idx;
            case 0x11:
                var fillRed = binaryReader.ReadValue();
                var fillGreen = binaryReader.ReadValue();
                var fillBlue = binaryReader.ReadValue();
                return "Fade In/Out, Fill Screen, Command: 13 11, Arguments: "
                    + fillRed
                    + ", "
                    + fillGreen
                    + ", "
                    + fillBlue;
            default:
                throw new Exception(
                    $"Position: {binaryReader.Now()}, Unknown Command: 13 {subCommand:X2}"
                );
        }
    }
}
