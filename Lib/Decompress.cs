using System.Text;

namespace Mutsuki.Lib;

public class Decompress
{
    public static byte[] UnPack(byte[] src)
    {
        byte[] dst;
        byte flag = 0;

        if (Encoding.ASCII.GetString(src, 0, 4) == "PACK")
        {
            var srcWithEof = new byte[src.Length + 1];
            src.CopyTo(srcWithEof, 0);
            // Raw translation of the original C++ code
            // Need refactoring
            var size = src[8] | (src[9] << 8) | (src[10] << 16) | (src[11] << 24);
            var rawLen = src[12] | (src[13] << 8) | (src[14] << 16) | (src[15] << 24);

            dst = new byte[size];
            var bit = 0;
            var srcCount = rawLen - 16;

            var srcIndex = 16;
            var dstIndex = 0;

            while ((dstIndex < size) && (srcCount > 0))
            {
                if (bit == 0)
                {
                    bit = 8;
                    flag = srcWithEof[srcIndex++];
                    srcCount--;
                }

                if ((flag & 0x80) != 0)
                {
                    dst[dstIndex++] = srcWithEof[srcIndex++];
                    srcCount--;
                }
                else
                {
                    int num = srcWithEof[srcIndex++];
                    num += (srcWithEof[srcIndex++] << 8);
                    srcCount -= 2;
                    var count = (num & 15) + 2;
                    num >>= 4;
                    var repeat = dst[(num - 1)..];
                    var repeatIndex = 0;

                    for (var i = 0; (i < count) && (dstIndex < size); i++)
                    {
                        dst[dstIndex++] = repeat[repeatIndex++];
                        repeatIndex++;
                    }
                }

                bit--;
                flag <<= 1;
            }
        }
        else
        {
            throw new Exception("Input file is not a PACK file.");
        }

        return dst;
    }
}
