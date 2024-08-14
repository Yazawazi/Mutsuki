using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x76, "Novel Mode Control")]
public class Op76 : IOpControl
{
    public static string ToCommand(BinaryReader binaryReader, StringMessage message)
    {
        var subCommand = binaryReader.ReadByte();

        switch (subCommand)
        {
            case 0x01:
                var data = binaryReader.ReadValue();
                return "Novel Mode Control, Set NVL Mode Flag, Command: 76 01, Arguments: " + data;
            case 0x03:
                return "Novel Mode Control, Unknown: sub_402570 -> sub_402550, Command: 76 03";
            case 0x04:
                return "Novel Mode Control, Unknown: dword_4C4770 = 1, Command: 76 04";
            case 0x05:
                return "Novel Mode Control, Unknown: dword_4C4770 = 0, Command: 76 05";
            default:
                throw new Exception(
                    $"Position: {binaryReader.Now()}, Unknown Command: 76 {subCommand:X2}"
                );
        }
    }
}
