using System.Text;
using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x6C, "Area Data")]
public class Op6C : IOpControl
{
    public static string ToCommand(BinaryReader reader, StringMessage message)
    {
        var subCommand = reader.ReadByte();

        switch (subCommand)
        {
            case 0x02:
                var file = Encoding.GetEncoding(932).GetString(reader.ReadCString());
                var ard = Encoding.GetEncoding(932).GetString(reader.ReadCString());
                return "Area Data, Read Area Data, Command: 6C 02, Arguments: " + file + ", " + ard;
            case 0x03:
                return "Area Data, Initialize Area Data, Command: 6C 03";
            case 0x05:
                var idx = reader.ReadValue();
                var idx2 = reader.ReadValue();
                return "Area Data, Clicked Area Data, Command: 6C 05, Arguments: "
                    + idx
                    + ", "
                    + idx2;
            default:
                throw new Exception(
                    $"Position: {reader.Now()}, Unknown Command: 6C {subCommand:X2}"
                );
        }
    }
}
