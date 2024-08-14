using System.Reflection;
using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

public interface IOpControl
{
    public static abstract string ToCommand(BinaryReader reader, StringMessage message);
}

public class OpControl(byte opCode, string description) : Attribute
{
    public byte OpCode { get; set; } = opCode;
    public string Description { get; set; } = description;
}

public class OpControlManager
{
    private readonly Dictionary<byte, Type> _opControlTypes = new Dictionary<byte, Type>();

    public OpControlManager()
    {
        var opControlTypes = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.GetCustomAttribute<OpControl>() != null);
        foreach (var type in opControlTypes)
        {
            var opControl = type.GetCustomAttribute<OpControl>();
            if (opControl == null)
            {
                continue;
            }
            _opControlTypes.Add(opControl.OpCode, type);
        }
    }

    public string ToCommand(byte opCode, BinaryReader reader, StringMessage message)
    {
        var inDict = _opControlTypes.TryGetValue(opCode, out var type);
        if (!inDict)
        {
            throw new Exception($"Position: {reader.Now()}, OpCode {opCode:X2} not found.");
        }
        return (string)type!.GetMethod("ToCommand")!.Invoke(null, new object[] { reader, message })!;
    }
}
