using System;
using System.Linq;

namespace _7;

public static class Functions
{
    public static long MagicSizeFunction(IDirectory dir, ref long sizeThing)
    {
        var dirSize = dir.Files.Sum(x => x.Size);
        
        foreach (var subDir in dir.SubDirectories)
        {
            dirSize += MagicSizeFunction(subDir, ref sizeThing);
        }

        if (dirSize <= 100_000) sizeThing += dirSize;

        return dirSize;
    }
}