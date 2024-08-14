using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x19, "Timer and Wait")]
public class Op19 : IOpControl
{
    public static string ToCommand(BinaryReader binaryReader, StringMessage message)
    {
        var subCommand = binaryReader.ReadByte();

        switch (subCommand)
        {
            case 0x01:
                var waitTime = binaryReader.ReadValue();
                return "Timer, Wait Time us, Command: 19 01, Arguments: " + waitTime;
            case 0x02:
                var waitTime2 = binaryReader.ReadValue();
                var waitCancelIndex = binaryReader.ReadValue();
                return "Timer, Wait Time us Cancelable, Command: 19 02, Arguments: "
                    + waitTime2
                    + ", "
                    + waitCancelIndex;
            case 0x03:
                return "Timer, Reset Timer, Command: 19 03";
            case 0x04:
                var waitTime3 = binaryReader.ReadValue();
                return "Timer, Wait Time us from Base Time, Command: 19 04, Arguments: "
                    + waitTime3;
            case 0x06:
                var idx = binaryReader.ReadValue();
                return "Timer, Set Times from Base Time to Val, Command: 19 06, Arguments: " + idx;
            case 0x10:
                return "Timer, Skip Mode Flag 1, Command 19 10";
            case 0x11:
                return "Timer, Skip Mode Flag 0, Command 19 11";
            case 0x12:
                return "Timer, Wait Flag 1, Command 19 12";
            case 0x13:
                return "Timer, Wait Flag 0, Command 19 13";
            default:
                throw new Exception(
                    $"Position: {binaryReader.Now()}, Unknown Command: 19 {subCommand:X2}"
                );
        }
    }
}
