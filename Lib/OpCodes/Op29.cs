namespace Mutsuki.Lib.OpCodes;

[OpControl(0x29, "Null Command")]
public class Op29 : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        return "Null Command, Continue, Command 29";
    }
}
