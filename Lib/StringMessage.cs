using System.Diagnostics.CodeAnalysis;
using System.Text;
using Newtonsoft.Json;

namespace Mutsuki.Lib;

public class StringMessage(string mappingFile)
{
    
    private readonly List<string> _messages = new();
    private readonly Dictionary<string, string> _mappingTable = 
        JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(mappingFile))!;

    private static int RawToOffset(int jis)
    {
        var low = ShiftJISToJIS(jis);
        low = low - 0x2121;
        var high = low >> 8;
        low = low & 0xFF;
        return (high * 0x5e + low) * 12 * 24;
    }
    
    // Yes, Japanese Industrial Standards
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private static int ShiftJISToJIS(int shiftJIS)
    {
        var codeLow = shiftJIS & 0xFF;
        var codeHigh = (shiftJIS - 0x8100) & 0xff00;

        var tmp = 0x2100;

        switch (codeLow)
        {
            case > 0x7f and >= 0x9f:
            {
                if (codeLow > 0xfc)
                {
                    return 0;
                }

                tmp += 0x100;
                codeLow -= 0x5f;
                break;
            }
            case > 0x7f:
                codeLow -= 1;
                break;
            case 0x7f or < 0x40:
                return 0;
        }

        codeLow -= 0x1f;

        switch (codeHigh)
        {
            case < 0x1f00:
                return ((codeHigh << 1) + tmp) + codeLow;
            case < 0x5f00 or > 0x6e00:
                return 0;
        }

        codeHigh -= 0x5f00;
        tmp += 0x5f00 - 0x2100;

        return ((codeHigh << 1) + tmp) + codeLow;
    }
    
    private string GetMappingValue(int offset)
    {
        var value = _mappingTable.FirstOrDefault(x => x.Key == offset.ToString());
        return value.Value ?? string.Empty;
    }

    public void AddChineseString(byte[] data)
    {
        var stringBuilder = new StringBuilder();
        for (var i = 0; i < data.Length; i += 2)
        {
            var offset = (data[i] & 0xff) << 8 | (data[i + 1] & 0xff);
            var message = GetMappingValue(RawToOffset(offset));
            stringBuilder.Append(message);
        }
        _messages.Add(stringBuilder.ToString());
    }
    
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public void AddShiftJISString(byte[] data)
    {
        var message = Encoding.GetEncoding("Shift_JIS").GetString(data);
        _messages.Add(message);
    }
    
    public override string ToString()
    {
        return _messages.Count > 0 ? string.Join("\n", _messages) : string.Empty;
    }
}