using System.Diagnostics.CodeAnalysis;
using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[OpControl(0xff, "Zenkaku Text")]
public class OpFF : IOpControl
{
    public static string ToCommand(BinaryReader reader, StringMessage message)
    {
        var index = reader.ReadInt32Le();
        var text = reader.ReadCString();
        
        message.AddChineseString(text);

        return "Zenkaku Text, Display, Command: FF, Arguments: "
            + index
            + ", "
            + BitConverter.ToString(text);
    }
}
