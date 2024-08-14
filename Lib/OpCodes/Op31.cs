using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x31, "Text Rank")]
public class Op31 : IOpControl
{
    public static string ToCommand(BinaryReader reader, StringMessage message)
    {
        var subCommand = reader.ReadByte();

        switch (subCommand)
        {
            case 0x01:
                var data = reader.ReadValue();
                return "Text Rank, Set Value, Command: 31 01, Arguments: " + data;
            case 0x02:
                return "Text Rank, Clear, Command: 31 02";
            default:
                throw new Exception(
                    $"Position: {reader.Now()}, Unknown Command: 31 {subCommand:X2}"
                );
        }
    }
}
