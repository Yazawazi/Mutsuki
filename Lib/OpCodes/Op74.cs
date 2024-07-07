using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x74, "Popup Menu")]
public class Op74 : IOpControl
{
    public static string ToCommand(BinaryReader binaryReader, Header header)
    {
        var subCommand = binaryReader.ReadByte();
        switch (subCommand)
        {
            case 0x02:
                var popupMenuDisable = binaryReader.ReadValue();
                return "Popup Menu, Enable/Disable SW, Command: 74 02, Arguments: "
                    + popupMenuDisable;
            case 0x04:
                var idx = binaryReader.ReadValue();
                var data = binaryReader.ReadValue();
                return "Popup Menu, Enable/Disable Menu, Command 74 04, Arguments: "
                    + idx
                    + ", "
                    + data;
            default:
                throw new Exception(
                    $"Position: {binaryReader.Now()}, Unknown Command: 74 {subCommand:X2}"
                );
        }
    }
}
