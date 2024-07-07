using Mutsuki.Extension;

namespace Mutsuki.Lib.OpCodes;

public enum Condition
{
    And,
    Or,
    AddDepth,
    SubDepth,
    BitNotEq,
    BitEq,
    NotEq,
    Eq,
    FlagNotEqConst,
    FlagEqConst,
    FlagAndConst,
    FlagAndConst2,
    FlagXorConst,
    FlagGtConst,
    FlagLtConst,
    FlagGeqConst,
    FlagLeqConst,
    FlagNotEq,
    FlagEq,
    FlagAnd,
    FlagAnd2,
    FlagXor,
    FlagGt,
    FlagLt,
    FlagGeq,
    FlagLeq,
    Unknown
}

public struct ConditionBox
{
    public Condition Condition;
    public List<Value> Values;

    public override string ToString()
    {
        return Values.Count == 0
            ? $"Condition({Condition})"
            : $"Condition({Condition}, Arguments: {string.Join(", ", Values)})";
    }
}

[OpControl(0x15, "Conditional branching (some bad stuff)")]
public class Op15 : IOpControl
{
    public static List<ConditionBox> GetConditionBoxes(BinaryReader binaryReader)
    {
        var finish = false;
        var conditions = new List<ConditionBox>();
        var depth = 0;

        while (!finish)
        {
            var next = binaryReader.ReadByte();

            switch (next)
            {
                case 0x26:
                    conditions.Add(new ConditionBox() { Condition = Condition.And, Values = [] });
                    break;
                case 0x27:
                    conditions.Add(new ConditionBox() { Condition = Condition.Or, Values = [] });
                    break;
                case 0x28:
                    depth += 1;
                    conditions.Add(
                        new ConditionBox() { Condition = Condition.AddDepth, Values = [] }
                    );
                    break;
                case 0x29:
                    depth -= 1;
                    if (depth <= 0)
                    {
                        finish = true;
                    }
                    conditions.Add(
                        new ConditionBox() { Condition = Condition.SubDepth, Values = [] }
                    );
                    break;
                case >= 0x36
                and <= 0x55:
                    var com1 = binaryReader.ReadValue();
                    var com2 = binaryReader.ReadValue();

                    var condition = next switch
                    {
                        0x36 => Condition.BitNotEq,
                        0x37 => Condition.BitEq,
                        0x38 => Condition.NotEq,
                        0x39 => Condition.Eq,
                        0x3a => Condition.FlagNotEqConst,
                        0x3b => Condition.FlagEqConst,
                        0x41 => Condition.FlagAndConst,
                        0x42 => Condition.FlagAndConst2,
                        0x43 => Condition.FlagXorConst,
                        0x44 => Condition.FlagGtConst,
                        0x45 => Condition.FlagLtConst,
                        0x46 => Condition.FlagGeqConst,
                        0x47 => Condition.FlagLeqConst,
                        0x48 => Condition.FlagNotEq,
                        0x49 => Condition.FlagEq,
                        0x4f => Condition.FlagAnd,
                        0x50 => Condition.FlagAnd2,
                        0x51 => Condition.FlagXor,
                        0x52 => Condition.FlagGt,
                        0x53 => Condition.FlagLt,
                        0x54 => Condition.FlagGeq,
                        0x55 => Condition.FlagLeq,
                        _ => Condition.Unknown
                    };

                    if (condition == Condition.Unknown)
                    {
                        throw new Exception(
                            $"Position: {binaryReader.Now()}, Unknown Condition {next:X2}"
                        );
                    }

                    conditions.Add(
                        new ConditionBox() { Condition = condition, Values = [com1, com2] }
                    );
                    break;
                default:
                    throw new Exception(
                        $"Position: {binaryReader.Now()}, Unknown Condition {next:X2}"
                    );
            }
        }
        return conditions;
    }

    public static string ToCommand(BinaryReader binaryReader, Header header)
    {
        var conditions = GetConditionBoxes(binaryReader);
        var ptr = binaryReader.ReadInt32Le();

        return "Condition, Jump, Command: 15, Arguments: "
            + string.Join(", ", conditions.Select(x => x.ToString()))
            + ", Ptr: "
            + ptr;
    }
}
