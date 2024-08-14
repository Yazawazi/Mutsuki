using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x75, "Audio Control")]
public class Op75 : IOpControl
{
    public static string ToCommand(BinaryReader reader, StringMessage message)
    {
        var subCommand = reader.ReadByte();
        var data = reader.ReadValue();

        return subCommand switch
        {
            0x11 => "Audio Control, Set BGM Volume, Command: 75 11, Arguments: " + data,
            0x12 => "Audio Control, Set WAV Volume, Command: 75 12, Arguments: " + data,
            0x13 => "Audio Control, Set KOE Volume, Command: 75 13, Arguments: " + data,
            0x14 => "Audio Control, Set SE Volume, Command: 75 14, Arguments: " + data,
            _ => throw new Exception($"Position: {reader.Now()}, Unknown Command: 75 {subCommand:X2}")
        };
    }
}
