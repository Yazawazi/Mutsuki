using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x61, "Name Value Get/Set")]
public class Op61 : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var subCommand = reader.ReadByte();

        switch (subCommand)
        {
            case 0x10
            or 0x12:
                var idxString = reader.ReadValue();
                var dataString = reader.ReadValue();
                return $"Name Value Get/Set, Get Name String, Command: 61 {subCommand:X2}, Arguments: {idxString}, {dataString}";
            case 0x11:
                var idx = reader.ReadValue();
                var data = reader.ReadValue();
                return $"Name Value Get/Set, Change Name Value, Command: 61 11, Arguments: {idx}, {data}";
            default:
                throw new Exception(
                    $"Position: {reader.Now()}, Unknown Command: 61 {subCommand:X2}"
                );
        }
    }
}
