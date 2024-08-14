using System.Text;
using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

public enum CompositeMethod
{
    Corner,
    Copy,
    Move1,
    Move2
}

public struct CompositeBox
{
    public CompositeMethod Method;
    public byte[] File;
    public List<Value> Values;

    public override string ToString()
    {
        var fileName = Encoding.ASCII.GetString(File).TrimEnd('\0');
        return Values.Count == 0
            ? $"Composite({Method}, {fileName})"
            : $"Composite({Method}, {fileName}, Arguments: {string.Join(", ", Values)})";
    }
}

[OpControl(0x0B, "Image Load")]
public class Op0B : IOpControl
{
    public static string ToCommand(BinaryReader binaryReader, StringMessage message)
    {
        var subCommand = binaryReader.ReadByte();

        switch (subCommand)
        {
            case 0x01
            or 0x05:
                var text = Encoding
                    .GetEncoding(932)
                    .GetString(binaryReader.ReadCString())
                    .TrimEnd('\0');
                var idx = binaryReader.ReadValue();
                return $"Image Load, Load and Display, Command: 0B {subCommand:X2}, Arguments: {text}, {idx}";
            case 0x02
            or 0x04
            or 0x06:
                var text02 = Encoding
                    .GetEncoding(932)
                    .GetString(binaryReader.ReadCString())
                    .TrimEnd('\0');
                var sx1 = binaryReader.ReadValue();
                var sy1 = binaryReader.ReadValue();
                var sx2 = binaryReader.ReadValue();
                var sy2 = binaryReader.ReadValue();
                var dx = binaryReader.ReadValue();
                var dy = binaryReader.ReadValue();
                var stepTime = binaryReader.ReadValue();
                var cmd = binaryReader.ReadValue();
                var mask = binaryReader.ReadValue();
                var effectArg1 = binaryReader.ReadValue();
                var effectArg2 = binaryReader.ReadValue();
                var effectArg3 = binaryReader.ReadValue();
                var effectStep = binaryReader.ReadValue();
                var effectArg5 = binaryReader.ReadValue();
                var effectArg6 = binaryReader.ReadValue();
                return $"Image Load, Load and Display with Effect, Command: 0B {subCommand:X2}, Arguments: {text02}, {sx1}, {sy1}, {sx2}, {sy2}, {dx}, {dy}, {stepTime}, {cmd}, {mask}, {effectArg1}, {effectArg2}, {effectArg3}, {effectStep}, {effectArg5}, {effectArg6}";
            case 0x08:
                return "Image Load, Unknown Command, Command: 0B 08";
            case 0x09:
                var text2 = Encoding
                    .GetEncoding(932)
                    .GetString(binaryReader.ReadCString())
                    .TrimEnd('\0');
                var idx2 = binaryReader.ReadValue();
                return "Image Load, Load To Buffer, Command: 0B 09, Arguments: "
                    + text2
                    + ", "
                    + idx2;
            case 0x10
            or 0x54:
                var text10 = Encoding
                    .GetEncoding(932)
                    .GetString(binaryReader.ReadCString())
                    .TrimEnd('\0');
                var idx10 = binaryReader.ReadValue();
                return $"Image Load, Load To Buffer 2, Command: 0B {subCommand:X2}, Arguments: {text10}, {idx10}";
            case 0x11:
                var cachedText = Encoding
                    .GetEncoding(932)
                    .GetString(binaryReader.ReadCString())
                    .TrimEnd('\0');
                return "Image Load, Load From Cache, Command: 0B 11, Arguments: " + cachedText;
            case 0x13:
                return "Image Load, Unknown Command, Command: 0B 13";
            case 0x22:
                var count = binaryReader.ReadByte();
                var baseFile = Encoding
                    .GetEncoding(932)
                    .GetString(binaryReader.ReadCString())
                    .TrimEnd('\0');
                var idx3 = binaryReader.ReadValue();
                var compositeBoxes = new List<CompositeBox>();
                for (var i = 0; i < count; i++)
                {
                    var index = binaryReader.ReadByte();
                    var file = binaryReader.ReadCString();

                    switch (index)
                    {
                        case 0x01:
                            compositeBoxes.Add(
                                new CompositeBox()
                                {
                                    File = file,
                                    Method = CompositeMethod.Corner,
                                    Values = []
                                }
                            );
                            break;
                        case 0x02:
                            var val = binaryReader.ReadValue();
                            compositeBoxes.Add(
                                new CompositeBox()
                                {
                                    File = file,
                                    Method = CompositeMethod.Copy,
                                    Values = [val]
                                }
                            );
                            break;
                        case 0x03:
                            var srcX1 = binaryReader.ReadValue();
                            var srcY1 = binaryReader.ReadValue();
                            var srcX2 = binaryReader.ReadValue();
                            var srcY2 = binaryReader.ReadValue();
                            var dstX1 = binaryReader.ReadValue();
                            var dstY1 = binaryReader.ReadValue();
                            compositeBoxes.Add(
                                new CompositeBox()
                                {
                                    File = file,
                                    Method = CompositeMethod.Move1,
                                    Values = [srcX1, srcY1, srcX2, srcY2, dstX1, dstY1]
                                }
                            );
                            break;
                        case 0x04:
                            var srcX1A = binaryReader.ReadValue();
                            var srcY1A = binaryReader.ReadValue();
                            var srcX2A = binaryReader.ReadValue();
                            var srcY2A = binaryReader.ReadValue();
                            var dstX1A = binaryReader.ReadValue();
                            var dstY1A = binaryReader.ReadValue();
                            var arg = binaryReader.ReadValue();
                            compositeBoxes.Add(
                                new CompositeBox()
                                {
                                    File = file,
                                    Method = CompositeMethod.Move2,
                                    Values = [srcX1A, srcY1A, srcX2A, srcY2A, dstX1A, dstY1A, arg]
                                }
                            );
                            break;
                        default:
                            throw new Exception(
                                $"Position: {binaryReader.Now()}, Unknown Composite: {index:X2}"
                            );
                    }
                }
                return "Image Load, Composite Group, Command: 0B 22, Arguments: "
                    + count
                    + ", "
                    + baseFile
                    + ", "
                    + idx3
                    + ", "
                    + string.Join(", ", compositeBoxes);
            case 0x30:
                return "Image Load, Macro Buffer Clear, Command: 0B 30";
            case 0x31:
                var marcoIdx = binaryReader.ReadValue();
                return "Image Load, Delete Macro, Command: 0B 31, Arguments: " + marcoIdx;
            default:
                throw new Exception(
                    $"Position: {binaryReader.Now()}, Unknown Command: 0B {subCommand:X2}"
                );
        }
    }
}
