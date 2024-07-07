using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x75, "Audio Control")]
public class Op75 : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var subCommand = reader.ReadByte();
        var data = reader.ReadValue();

        switch (subCommand)
        {
            case 0x11:
                return "Audio Control, Set BGM Volume, Command: 75 11, Arguments: " + data;
            case 0x12:
                return "Audio Control, Set WAV Volume, Command: 75 12, Arguments: " + data;
            case 0x13:
                return "Audio Control, Set KOE Volume, Command: 75 13, Arguments: " + data;
            case 0x14:
                return "Audio Control, Set SE Volume, Command: 75 14, Arguments: " + data;
            default:
                throw new Exception(
                    $"Position: {reader.Now()}, Unknown Command: 75 {subCommand:X2}"
                );
        }
    }
}
