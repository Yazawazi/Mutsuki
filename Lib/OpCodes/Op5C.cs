using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x5C, "Multiple Variable Set")]
public class Op5C : IOpControl
{
    public static string ToCommand(BinaryReader reader, StringMessage message)
    {
        var subCommand = reader.ReadByte();

        var idx = reader.ReadValue();
        var idx2 = reader.ReadValue();
        var data = reader.ReadValue();

        return subCommand switch
        {
            0x01 => "Multiple Variable Set, Val, Command: 5C 01, Arguments: " + idx + ", " + idx2 + ", " + data,
            0x02 => "Multiple Variable Set, Bit, Command: 5C 02, Arguments: " + idx + ", " + idx2 + ", " + data,
            _ => throw new Exception($"Position: {reader.Now()}, Unknown Command: 5C {subCommand:X2}")
        };
    }
}
