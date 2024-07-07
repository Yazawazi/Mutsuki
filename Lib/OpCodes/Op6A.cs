using System.Text;
using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

public struct MultiplePdtEntry
{
    public byte[] Text;
    public Value Data;

    public override string ToString()
    {
        var textDisplay = Encoding.ASCII.GetString(Text).TrimEnd('\0');
        return $"MultiplePDTEntry(Text: {textDisplay}, Data: {Data})";
    }
}

[OpControl(0x6a, "Multiple PDT Handle")]
public class Op6A : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var subCommand = reader.ReadByte();

        switch (subCommand)
        {
            case 0x03:
                var slideShowCount = reader.ReadByte();
                var slideShowPos = reader.ReadValue();
                var slideShowWait = reader.ReadValue();
                var slideShowEntries = new List<MultiplePdtEntry>();
                for (var i = 0; i < slideShowCount; i++)
                {
                    var text = reader.ReadCString();
                    var data = reader.ReadValue();
                    slideShowEntries.Add(new MultiplePdtEntry { Text = text, Data = data });
                }
                return $"Multiple PDT Handle, Slide Show, Command: 6A 03, Arguments: {slideShowCount}, {slideShowPos}, {slideShowWait}, {string.Join(", ", slideShowEntries)}";
            case 0x04:
                var slideShowLoopCount = reader.ReadByte();
                var slideShowLoopPos = reader.ReadValue();
                var slideShowLoopWait = reader.ReadValue();
                var slideShowLoopEntries = new List<MultiplePdtEntry>();
                for (var i = 0; i < slideShowLoopCount; i++)
                {
                    var text = reader.ReadCString();
                    var data = reader.ReadValue();
                    slideShowLoopEntries.Add(new MultiplePdtEntry { Text = text, Data = data });
                }
                return $"Multiple PDT Handle, Slide Show Loop, Command: 6A 04, Arguments: {slideShowLoopCount}, {slideShowLoopPos}, {slideShowLoopWait}, {string.Join(", ", slideShowLoopEntries)}";
            default:
                throw new Exception(
                    $"Position: {reader.Now()}, Unknown Command: 6A {subCommand:X2}"
                );
        }
    }
}
