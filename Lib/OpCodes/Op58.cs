using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x58, "Select")]
public class Op58 : IOpControl
{
    public static string ToCommand(BinaryReader reader, StringMessage message)
    {
        var subCommand = reader.ReadByte();

        switch (subCommand)
        {
            case 0x01
            or 0x02:
                var index = reader.ReadValue();
                var flag = reader.ReadByte();
                if (flag == 0x22)
                {
                    reader.SkipCurrentZero();
                    var selectTexts = new List<byte[]>();
                    do
                    {
                        var text = reader.ReadFormattedText();
                        if (text.Any(x => x != 0x00))
                        {
                            selectTexts.Add(text);
                        }

                        if (text[0] != 0x43)
                        {
                            message.AddChineseString(text);
                        }
                    } while (reader.PeekByte() != 0x23);

                    if (reader.PeekByte() == 0x23)
                    {
                        reader.Skip(1);
                    }

                    return "Select, Command: 58 01, Arguments: "
                        + index
                        + ", "
                        + flag
                        + ", "
                        + string.Join(", ", selectTexts.Select(x => BitConverter.ToString(x)));
                }
                else
                {
                    return "Select, Command: 58 01, Arguments: " + index + ", " + flag;
                }
            case 0x04:
                var idx = reader.ReadValue();
                return "Select, Open Load Menu, Command: 58 04, Arguments: " + idx;
            default:
                throw new Exception(
                    $"Position: {reader.Now()}, Unknown Command: 58 {subCommand:X2}"
                );
        }
    }
}
