using System.Text;
using Mutsuki.Extension;
using Mutsuki.Lib.OpCodes;

namespace Mutsuki.Lib;

public struct Flag
{
    public byte Count;
    public int[] Flags;

    public override string ToString()
    {
        return $"Flag(Count: {Count}, Flags: {string.Join(", ", Flags)})";
    }
}

public struct SubMenu
{
    public byte Id;
    public byte FlagCount;
    public Flag[] Flags;

    public override string ToString()
    {
        return $"SubMenu(Id: {Id}, FlagCount: {FlagCount}, Flags: {string.Join(", ", Flags)})";
    }
}

public struct Menu
{
    public byte Id;
    public byte SubMenuCount;
    public SubMenu[] SubMenus;

    public override string ToString()
    {
        return $"Menu(Id: {Id}, SubMenuCount: {SubMenuCount}, SubMenu: {string.Join(", ", SubMenus)})";
    }
}

public struct Header
{
    public string Magic;
    public int LabelCount;
    public int CounterStart;
    public int[] Labels;
    public int MenuCount;
    public Menu[] Menus;
    public byte[][] MenuNames;

    private string GetMenuNamesForPrint()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < MenuCount; i++)
        {
            sb.Append($"Menu {i}: {BitConverter.ToString(MenuNames[i]).Replace("-", "")}\n");
        }
        return sb.ToString();
    }

    public override string ToString()
    {
        return @$"Header: {Magic}
LabelCount: {LabelCount}
CounterStart: {CounterStart}
MenuCount: {MenuCount}
---
Labels: {string.Join(", ", Labels)}
Menus: {string.Join(", ", Menus)}
MenuNames: {GetMenuNamesForPrint()}
";
    }
}

public class ScenarioParser
{
    private readonly BinaryReader _reader;
    private bool _isParsedHeader;
    private readonly Header _header;
    private readonly OpControlManager _opControlManager = new OpControlManager();

    public readonly string FinalContent;

    public ScenarioParser(Stream inputFile)
    {
        _reader = new BinaryReader(inputFile);
        _header = ParseHeader();
        var commands = ParseBody();

        FinalContent = _header.ToString() + "\n" + string.Join("\n", commands);
    }

    private Header ParseHeader()
    {
        _reader.GoTo(0x00);

        if (Encoding.UTF8.GetString(_reader.ReadBytes(5)) != "TPC32")
        {
            throw new Exception("Input file is not a TPC32 file.");
        }

        _reader.Skip(0x13);

        var labelCount = _reader.ReadInt32Le();
        var counterStart = _reader.ReadInt32Le();

        var labels = new int[labelCount];

        for (var i = 0; i < labelCount; i++)
        {
            labels[i] = (_reader.ReadInt32Le());
        }

        _reader.Skip(0x30);
        var menuCount = _reader.ReadInt32Le();

        var menus = new Menu[menuCount];

        for (var i = 0; i < menuCount; i++)
        {
            var menuId = _reader.ReadByte();
            var menuSubMenuCount = _reader.ReadByte();
            _reader.Skip(2);
            var menuSubMenus = new SubMenu[menuSubMenuCount];
            for (var j = 0; j < menuSubMenuCount; j++)
            {
                var subMenuId = _reader.ReadByte();
                var subMenuFlagCount = _reader.ReadByte();
                _reader.Skip(2);
                var subMenuFlags = new Flag[subMenuFlagCount];
                for (var k = 0; k < subMenuFlagCount; k++)
                {
                    var flagCount = _reader.ReadByte();
                    _reader.Skip(1);
                    var flags = new int[flagCount];
                    for (var l = 0; l < flagCount; l++)
                    {
                        flags[l] = _reader.ReadInt32Le();
                    }
                    subMenuFlags[k] = new Flag { Count = flagCount, Flags = flags };
                }
                menuSubMenus[j] = new SubMenu
                {
                    Id = subMenuId,
                    FlagCount = subMenuFlagCount,
                    Flags = subMenuFlags
                };
            }
            menus[i] = new Menu
            {
                Id = menuId,
                SubMenuCount = menuSubMenuCount,
                SubMenus = menuSubMenus
            };
        }

        var strCount = menus.Sum(menu => menu.SubMenuCount + 1);

        var menuStrings = new byte[strCount][];
        for (var i = 0; i < strCount; i++)
        {
            menuStrings[i] = _reader.ReadCString();
        }

        _isParsedHeader = true;

        return new Header
        {
            Magic = "TPC32",
            LabelCount = labelCount,
            CounterStart = counterStart,
            Labels = labels,
            MenuCount = menuCount,
            Menus = menus,
            MenuNames = menuStrings
        };
    }

    private List<string> ParseBody()
    {
        if (!_isParsedHeader)
        {
            throw new Exception("Header must be parsed first.");
        }

        _reader.Skip(0x05);

        var commands = new List<string>();

        while (_reader.Now() < _reader.BaseStream.Length)
        {
            var opCode = _reader.ReadByte();
            var command = _opControlManager.ToCommand(opCode, _reader, _header);
            // Console.WriteLine(command);
            commands.Add(command);
        }

        return commands;
    }
}
