namespace Mutsuki.Lib.OpCodes;

[OpControl(0xCC, "Unknown Command")]
public class OpCC : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        return "Unknown Command, Padding? Opcode: CC";
    }
}
