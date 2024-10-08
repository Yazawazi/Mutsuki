using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x00, "Error and End")]
public class Op00 : IOpControl
{
    public static string ToCommand(BinaryReader reader, StringMessage message)
    {
        if (reader.Now() == reader.BaseStream.Length)
        {
            return "EOF, 00";
        }
        
        throw new Exception($"Error, May Cause Alignment Issue, Position: " + reader.Now() + ", OpCode: 00");
    }
}
