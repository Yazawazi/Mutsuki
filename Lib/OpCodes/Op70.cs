using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x70, "Window Get/Set")]
public class Op70 : IOpControl
{
    public static string ToCommand(BinaryReader binaryReader, StringMessage message)
    {
        var subCommand = binaryReader.ReadByte();

        switch (subCommand)
        {
            case 0x01:
                var data = binaryReader.ReadValue();
                var idx = binaryReader.ReadValue();
                var idx2 = binaryReader.ReadValue();
                var idx3 = binaryReader.ReadValue();
                return "Window Get/Set, Get BG Flag/Color, Command: 70 01, Arguments: "
                    + data
                    + ", "
                    + idx
                    + ", "
                    + idx2
                    + ", "
                    + idx3;
            case 0x02:
                var attr = binaryReader.ReadValue();
                var r = binaryReader.ReadValue();
                var g = binaryReader.ReadValue();
                var b = binaryReader.ReadValue();
                return "Window Get/Set, Set Color, Command: 70 02, Arguments: "
                    + attr
                    + ", "
                    + r
                    + ", "
                    + g
                    + ", "
                    + b;
            default:
                throw new Exception(
                    $"Position: {binaryReader.Now()}, Unknown Command: 70 {subCommand:X2}"
                );
        }
    }
}
