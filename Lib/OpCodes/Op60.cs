using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x60, "System Control / Save Load")]
public class Op60 : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var subCommand = reader.ReadByte();

        switch (subCommand)
        {
            case 0x02:
                var fileIdx = reader.ReadValue();
                return "System Control / Save Load, Load Save Data, Command: 60 02, Arguments: "
                    + fileIdx;
            case 0x04:
                var windowTitle = reader.ReadFormattedText();
                return "System Control / Save Load, Set Window Title, Command: 60 04, Arguments: "
                    + BitConverter.ToString(windowTitle);
            case 0x20:
                return "System Control / Save Load, Game End, Command: 60 20";
            case 0x31:
                var data = reader.ReadValue();
                var idx = reader.ReadValue();
                return "System Control / Save Load, Check Save Data, Command: 60 31, Arguments: "
                    + data
                    + ", "
                    + idx;
            default:
                throw new Exception(
                    $"Position: {reader.Now()}, Unknown Command: 60 {subCommand:X2}"
                );
        }
    }
}
