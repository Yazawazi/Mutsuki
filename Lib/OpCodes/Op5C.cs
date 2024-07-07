using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x5C, "Multiple Variable Set")]
public class Op5C : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var subCommand = reader.ReadByte();

        var idx = reader.ReadValue();
        var idx2 = reader.ReadValue();
        var data = reader.ReadValue();

        switch (subCommand)
        {
            case 0x01:
                return "Multiple Variable Set, Val, Command: 5C 01, Arguments: "
                    + idx
                    + ", "
                    + idx2
                    + ", "
                    + data;
            case 0x02:
                return "Multiple Variable Set, Bit, Command: 5C 02, Arguments: "
                    + idx
                    + ", "
                    + idx2
                    + ", "
                    + data;
            default:
                throw new Exception(
                    $"Position: {reader.Now()}, Unknown Command: 5C {subCommand:X2}"
                );
        }
    }
}
