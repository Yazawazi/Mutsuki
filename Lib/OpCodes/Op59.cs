using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

// TEXT NEED
[OpControl(0x59, "Text Line")]
public class Op59 : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var subCommand = reader.ReadByte();
        var idx = reader.ReadValue();

        switch (subCommand)
        {
            case 0x01:
                var text = reader.ReadCString();
                return "Text Line, Text Set, Command: 59, Arguments: "
                    + idx
                    + ", "
                    + BitConverter.ToString(text);
            case 0x03:
                var idx2 = reader.ReadValue();
                var idx3 = reader.ReadValue();
                return "Text Line, Text Compare, Command: 59 03, Arguments: "
                    + idx
                    + ", "
                    + idx2
                    + ", "
                    + idx3;
            default:
                throw new Exception(
                    $"Position: {reader.Now()}, Unknown Command: 59 {subCommand:X2}"
                );
        }
    }
}
