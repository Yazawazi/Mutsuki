using System.Text;
using Mutsuki.Extension;

namespace Mutsuki.Lib;

public struct SeenMetadata
{
    public string Name;
    public int Offset;
    public int Size;
}

public class SeenParser
{
    public readonly int FileCount;
    public readonly SeenMetadata[] FilesMetadata;
    public readonly Dictionary<string, byte[]> Files;

    public SeenParser(Stream inputFile)
    {
        var reader = new BinaryReader(inputFile);

        var header = reader.ReadBytes(4);

        var isSeen = Encoding.UTF8.GetString(header) == "PACL";

        if (!isSeen)
        {
            throw new Exception("Input file is not a SEEN file.");
        }

        reader.BaseStream.Seek(0x10, SeekOrigin.Begin);
        FileCount = reader.ReadInt32Le();

        reader.Skip(12);

        FilesMetadata = new SeenMetadata[FileCount];
        Files = new Dictionary<string, byte[]>();

        for (var i = 0; i < FileCount; i++)
        {
            var metadata = new SeenMetadata
            {
                Name = Encoding.UTF8.GetString(reader.ReadBytes(0x10)).TrimEnd('\0'),
                Offset = reader.ReadInt32Le(),
                Size = reader.ReadInt32Le()
            };
            var nowPosition = reader.Now();
            reader.GoTo(metadata.Offset);
            Files.Add(metadata.Name, reader.ReadBytes(metadata.Size));
            reader.GoTo(nowPosition);
            reader.Skip(8);
            FilesMetadata[i] = metadata;
        }
    }
}
