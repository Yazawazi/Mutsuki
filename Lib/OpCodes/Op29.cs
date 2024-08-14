namespace Mutsuki.Lib.OpCodes;

[OpControl(0x29, "Null Command")]
public class Op29 : IOpControl
{
    public static string ToCommand(BinaryReader reader, StringMessage message)
    {
        return "Null Command, Continue, Command 29";
    }
}
