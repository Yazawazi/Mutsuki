using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x67, "Buffer Copy / Display")]
public class Op67 : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var subCommand = reader.ReadByte();

        switch (subCommand)
        {
            case 0x00:
                var srcX10 = reader.ReadValue();
                var srcY10 = reader.ReadValue();
                var srcX20 = reader.ReadValue();
                var srcY20 = reader.ReadValue();
                var srcPdt0 = reader.ReadValue();
                var srcFlag = reader.ReadValue();
                return $"Buffer Copy / Display, Copy to PDT, Command: 67 00, Arguments: {srcX10}, {srcY10}, {srcX20}, {srcY20}, {srcPdt0}, {srcFlag}";
            case 0x01:
                var srcX1 = reader.ReadValue();
                var srcY1 = reader.ReadValue();
                var srcX2 = reader.ReadValue();
                var srcY2 = reader.ReadValue();
                var srcPdt = reader.ReadValue();
                var dstX1 = reader.ReadValue();
                var dstY1 = reader.ReadValue();
                var dstPdt = reader.ReadValue();
                var flag = reader.ReadValue();
                return $"Buffer Copy / Display, Copy, Command: 67 01, Arguments: {srcX1}, {srcY1}, {srcX2}, {srcY2}, {srcPdt}, {dstX1}, {dstY1}, {dstPdt}, {flag}";
            case 0x02:
                var maskSrcX1 = reader.ReadValue();
                var maskSrcY1 = reader.ReadValue();
                var maskSrcX2 = reader.ReadValue();
                var maskSrcY2 = reader.ReadValue();
                var maskSrcPdt = reader.ReadValue();
                var maskDstX1 = reader.ReadValue();
                var maskDstY1 = reader.ReadValue();
                var maskDstPdt = reader.ReadValue();
                var maskFlag = reader.ReadValue();
                return $"Buffer Copy / Display, Mask Copy, Command: 67 02, Arguments: {maskSrcX1}, {maskSrcY1}, {maskSrcX2}, {maskSrcY2}, {maskSrcPdt}, {maskDstX1}, {maskDstY1}, {maskDstPdt}, {maskFlag}";
            default:
                throw new Exception(
                    $"Position: {reader.Now()}, Unknown Command: 67 {subCommand:X2}"
                );
        }
    }
}
