using System.Diagnostics.CodeAnalysis;
using System.Text;
using Mutsuki.Lib.OpCodes;

namespace Mutsuki.Extension;

public enum ValueFrom
{
    Flag,
    Raw
}

public struct Value
{
    public ValueFrom From;
    public int TrueValue;

    public override string ToString()
    {
        return $"Value({From}, {TrueValue})";
    }
}

internal static class BinaryExtension
{
    public static long Now(this BinaryReader reader)
    {
        return reader.BaseStream.Position;
    }

    public static void Skip(this BinaryReader reader, long count)
    {
        reader.BaseStream.Seek(count, SeekOrigin.Current);
    }

    public static void SkipCurrentZero(this BinaryReader reader)
    {
        if (reader.ReadByte() == 0)
            return;
        reader.ReWind(1);
    }
    
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static void ReWind(this BinaryReader reader, long count)
    {
        reader.BaseStream.Seek(-count, SeekOrigin.Current);
    }

    public static byte PeekByte(this BinaryReader reader)
    {
        var current = reader.ReadByte();
        reader.ReWind(1);
        return current;
    }

    public static void GoTo(this BinaryReader reader, long offset)
    {
        reader.BaseStream.Seek(offset, SeekOrigin.Begin);
    }

    public static short ReadInt16Le(this BinaryReader reader)
    {
        var bytes = reader.ReadBytes(2);
        return BitConverter.ToInt16(bytes, 0);
    }

    public static ushort ReadUInt16Le(this BinaryReader reader)
    {
        var bytes = reader.ReadBytes(2);
        return BitConverter.ToUInt16(bytes, 0);
    }

    public static int ReadInt32Le(this BinaryReader reader)
    {
        var bytes = reader.ReadBytes(4);
        return BitConverter.ToInt32(bytes, 0);
    }

    public static uint ReadUInt32Le(this BinaryReader reader)
    {
        var bytes = reader.ReadBytes(4);
        return BitConverter.ToUInt32(bytes, 0);
    }

    public static long ReadInt64Le(this BinaryReader reader)
    {
        var bytes = reader.ReadBytes(8);
        return BitConverter.ToInt64(bytes, 0);
    }

    public static ulong ReadUInt64Le(this BinaryReader reader)
    {
        var bytes = reader.ReadBytes(8);
        return BitConverter.ToUInt64(bytes, 0);
    }

    public static byte[] ReadCString(this BinaryReader reader)
    {
        var bytes = new List<byte>();
        byte b;
        while ((b = reader.ReadByte()) != 0)
        {
            bytes.Add(b);
        }
        return bytes.ToArray();
    }

    public static string ReadBytesAsHex(this BinaryReader reader, int count)
    {
        var bytes = reader.ReadBytes(count);
        return BitConverter.ToString(bytes).Replace("-", "");
    }

    public static byte[] ReadFormattedText(this BinaryReader reader)
    {
        var type = reader.ReadByte();

        switch (type)
        {
            case 0x28:
                var boxes = Op15.GetConditionBoxes(reader);
                var displayedBoxes = string.Join(", ", boxes.Select(x => x.ToString()));
                return Encoding.ASCII.GetBytes(displayedBoxes);
            case 0xfe
            or 0xff:
                var cString = reader.ReadCString();
                reader.SkipCurrentZero();
                return cString;
            default:
                throw new Exception($"Position: {reader.Now()}, Unknown text type: {type:X2}");
        }
    }

    public static Value ReadValue(this BinaryReader reader)
    {
        var cursorPosition = reader.Now();
        var number = reader.ReadByte();
        reader.ReWind(1);
        var low = (number >> 4) & 7;
        var returnValue = 0;

        for (var i = cursorPosition + low - 1; i > cursorPosition; i--)
        {
            returnValue <<= 8;
            reader.GoTo(i);
            returnValue |= reader.ReadByte();
        }

        returnValue <<= 4;
        returnValue |= number & 15;
        reader.GoTo(cursorPosition + low);

        return (number & 0x80) != 0 ? new Value { From = ValueFrom.Flag, TrueValue = returnValue } : new Value { From = ValueFrom.Raw, TrueValue = returnValue };
    }
}
