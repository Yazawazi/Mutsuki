using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x73, "Get/Set System Config")]
public class Op73 : IOpControl
{
    public static string ToCommand(BinaryReader binaryReader, Header header)
    {
        var subCommand = binaryReader.ReadByte();

        switch (subCommand)
        {
            case 0x02:
                var windowX = binaryReader.ReadValue();
                var windowY = binaryReader.ReadValue();
                return "Set System Config, Set Window Size (aka string number | x, y), Command: 73 02, Arguments: "
                    + windowX
                    + ", "
                    + windowY;
            case 0x06:
                var x = binaryReader.ReadValue();
                var y = binaryReader.ReadValue();
                return "Set System Config, Set Font Size (x, y), Command: 73 06, Arguments: "
                    + x
                    + ", "
                    + y;
            case 0x1d:
                var ctrlData = binaryReader.ReadValue();
                return "Set System Config, Ctrl Skip Enable/Disable, Command: 73 1d, Arguments: "
                    + ctrlData;
            case 0x28:
                var val = binaryReader.ReadValue();
                return "Set System Config, Get Message Speed, Command: 73 28, Arguments: " + val;
            case 0x29:
                var speedData = binaryReader.ReadValue();
                return "Set System Config, Set Message Speed, Command: 73 29, Arguments: "
                    + speedData;
            case 0x30
            or 0x33:
                var idx = binaryReader.ReadValue();
                return "Set System Config, Get #GAME_SPECK_INIT To Val, Command: 73 "
                    + subCommand.ToString("X2")
                    + ", Arguments: "
                    + idx;
            case 0x34:
                var idx2 = binaryReader.ReadValue();
                return "Set System Config, Set Val With #GAME_SPECK_INIT, Command: 73 34, Arguments: "
                    + idx2;
            default:
                throw new Exception(
                    $"Position: {binaryReader.Now()}, Unknown Command: 73 {subCommand:X2}"
                );
        }
    }
}
