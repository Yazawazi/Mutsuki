using System.Text;
using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

[OpControl(0x0e, "Sound Play")]
public class Op0E : IOpControl
{
    public static string ToCommand(BinaryReader reader, Header header)
    {
        var subCommand = reader.ReadByte();

        switch (subCommand)
        {
            case 0x01:
                var loopAudio = Encoding
                    .GetEncoding(932)
                    .GetString(reader.ReadCString())
                    .TrimEnd('\0');
                return "Sound Play, Loop Audio, Command: 0E 01, Arguments: " + loopAudio;
            case 0x02:
                var waitAudio = Encoding
                    .GetEncoding(932)
                    .GetString(reader.ReadCString())
                    .TrimEnd('\0');
                return "Sound Play, Wait Audio, Command: 0E 02, Arguments: " + waitAudio;
            case 0x03:
                var playAudio = Encoding
                    .GetEncoding(932)
                    .GetString(reader.ReadCString())
                    .TrimEnd('\0');
                return "Sound Play, Play Audio, Command: 0E 03, Arguments: " + playAudio;
            case 0x05:
                var cdTrack = Encoding
                    .GetEncoding(932)
                    .GetString(reader.ReadCString())
                    .TrimEnd('\0');
                var data5 = reader.ReadValue();
                return "Sound Play, Fade In Loop, Command: 0E 05, Arguments: "
                    + cdTrack
                    + ", "
                    + data5;
            case 0x07:
                var cdTrack7 = Encoding
                    .GetEncoding(932)
                    .GetString(reader.ReadCString())
                    .TrimEnd('\0');
                var data7 = reader.ReadValue();
                return "Sound Play, Fade In Once, Command: 0E 07, Arguments: "
                    + cdTrack7
                    + ", "
                    + data7;
            case 0x10:
                var fadeOut = reader.ReadValue();
                return "Sound Play, Fade Out, Command: 0E 10, Arguments: " + fadeOut;
            case 0x11:
                return "Sound Play, Stop CD, Command: 0E 11";
            case 0x16:
                return "Sound Play, Unknown Command (Sleep? sub_424670), Command: 0E 16";
            case 0x20:
                var data = reader.ReadValue();
                return "Sound Play, Voice Wait, Command: 0E 20, Arguments: " + data;
            case 0x21:
                var data2 = reader.ReadValue();
                return "Sound Play, Play Voice Index, Command: 0E 21, Arguments: " + data2;
            case 0x30:
                var wavFile = Encoding
                    .GetEncoding(932)
                    .GetString(reader.ReadCString())
                    .TrimEnd('\0');
                return "Sound Play, Play WAV File, Command: 0E 30, Arguments: " + wavFile;
            case 0x31:
                var waveFile31 = Encoding
                    .GetEncoding(932)
                    .GetString(reader.ReadCString())
                    .TrimEnd('\0');
                var waveFile31Idx = reader.ReadValue();
                return "Sound Play, Play WAV File with Idx, Command: 0E 31, Arguments: "
                    + waveFile31
                    + ", "
                    + waveFile31Idx;
            case 0x32:
                var wavFileLoop = Encoding
                    .GetEncoding(932)
                    .GetString(reader.ReadCString())
                    .TrimEnd('\0');
                return "Sound Play, Play WAV File Loop, Command: 0E 32, Arguments: " + wavFileLoop;
            case 0x33:
                var wavFileLoop33 = Encoding
                    .GetEncoding(932)
                    .GetString(reader.ReadCString())
                    .TrimEnd('\0');
                var wavFileLoop33Idx = reader.ReadValue();
                return "Sound Play, Play WAV File Loop with Idx, Command: 0E 33, Arguments: "
                    + wavFileLoop33
                    + ", "
                    + wavFileLoop33Idx;
            case 0x34:
                var waveFileWait = Encoding
                    .GetEncoding(932)
                    .GetString(reader.ReadCString())
                    .TrimEnd('\0');
                return "Sound Play, Play WAV File Wait, Command: 0E 34, Arguments: " + waveFileWait;
            case 0x36:
                return "Sound Play, Stop WAV, Command: 0E 36";
            case 0x37:
                var wavStopData = reader.ReadValue();
                return "Sound Play, Stop WAV with Data, Command: 0E 37, Arguments: " + wavStopData;
            case 0x50
            or 0x51:
                var movieFile = Encoding
                    .GetEncoding(932)
                    .GetString(reader.ReadCString())
                    .TrimEnd('\0');
                var x1 = reader.ReadValue();
                var y1 = reader.ReadValue();
                var x2 = reader.ReadValue();
                var y2 = reader.ReadValue();
                return $"Sound Play, Play Movie, Command: 0E {subCommand:X2}, Arguments: {movieFile}, {x1}, {y1}, {x2}, {y2}";
            case 0x60:
                return "Sound Play, Unknown Command (Timer? sub_420CD0), Command: 0E 60";
            default:
                throw new Exception(
                    $"Position: {reader.Now()}, Unknown Command: 0E {subCommand:X2}"
                );
        }
    }
}
