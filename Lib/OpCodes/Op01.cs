namespace Mutsuki.Lib.OpCodes;

[OpControl(0x01, "Wait Click")]
public class Op01 : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        return "Wait Click, Wait For Click, Command: 01";
    }
}
