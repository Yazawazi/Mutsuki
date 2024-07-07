using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

// TEXT NEED
[OpControl(0xff, "Zenkaku Text")]
public class OpFF : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var index = reader.ReadInt32Le();
        var text = reader.ReadCString();

        return "Zenkaku Text, Display, Command: FF, Arguments: "
            + index
            + ", "
            + BitConverter.ToString(text);
    }
}
