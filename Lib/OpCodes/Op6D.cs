using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x6D, "Mouse Control")]
public class Op6D : IOpControl
{
    public static string ToCommand(BinaryReader binaryReader, Header header)
    {
        var subCommand = binaryReader.ReadByte();

        switch (subCommand)
        {
            case 0x01:
                var idxWait = binaryReader.ReadValue();
                var idx2Wait = binaryReader.ReadValue();
                var idx3Wait = binaryReader.ReadValue();
                return "Mouse Control, Wait Click To Move, Command: 6d 01, Arguments: "
                    + idxWait
                    + ", "
                    + idx2Wait
                    + ", "
                    + idx3Wait;
            case 0x02:
                var idxMove = binaryReader.ReadValue();
                var idx2Move = binaryReader.ReadValue();
                var idx3Move = binaryReader.ReadValue();
                return "Mouse Control, Move, Command: 6d 02, Arguments: "
                    + idxMove
                    + ", "
                    + idx2Move
                    + ", "
                    + idx3Move;
            case 0x03:
                return "Mouse Control, Click Data Flush, Command: 6d 03";
            case 0x20:
                return "Mouse Control, Cursor Off, Command: 6d 20";
            case 0x21:
                return "Mouse Control, Cursor On, Command: 6d 21";
            default:
                throw new Exception(
                    $"Position: {binaryReader.Now()}, Unknown Command: 6d {subCommand:X2}"
                );
        }
    }
}
