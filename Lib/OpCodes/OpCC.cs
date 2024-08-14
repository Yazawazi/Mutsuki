using System.Diagnostics.CodeAnalysis;

namespace Mutsuki.Lib.OpCodes;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[OpControl(0xCC, "Unknown Command")]
public class OpCC : IOpControl
{
    public static string ToCommand(BinaryReader reader, StringMessage message)
    {
        return "Unknown Command, Padding? Opcode: CC";
    }
}
