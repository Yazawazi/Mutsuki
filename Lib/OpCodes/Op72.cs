using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x72, "Message Window, Get/Set")]
public class Op72 : IOpControl
{
    public static string ToCommand(BinaryReader reader, StringMessage message)
    {
        var subCommand = reader.ReadByte();

        switch (subCommand)
        {
            case 0x01:
                var idxCur = reader.ReadValue();
                var idx2Cur = reader.ReadValue();
                return "Message Window, Set Current Message Window Position, Command: 72 01, Arguments: "
                    + idxCur
                    + ", "
                    + idx2Cur;
            case 0x02:
                var idxCurSub = reader.ReadValue();
                var idx2CurSub = reader.ReadValue();
                return "Message Window, Set Sub Current Message Window Position, Command: 72 02, Arguments: "
                    + idxCurSub
                    + ", "
                    + idx2CurSub;
            case 0x11:
                var mesWindowX = reader.ReadValue();
                var mesWindowY = reader.ReadValue();
                return "Message Window, Set Message Window Position, Command: 72 11, Arguments: "
                    + mesWindowX
                    + ", "
                    + mesWindowY;
            case 0x12:
                var mesSubWindowX = reader.ReadValue();
                var mesSubWindowY = reader.ReadValue();
                return "Message Window, Set Sub Message Window Position, Command: 72 11, Arguments: "
                    + mesSubWindowX
                    + ", "
                    + mesSubWindowY;
            default:
                throw new Exception(
                    $"Position: {reader.Now()}, Unknown Command: 72 {subCommand:X2}"
                );
        }
    }
}
