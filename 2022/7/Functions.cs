using System;
using System.Linq;

namespace _7;

public static class Functions
{
    public static int MagicSizeFunction(IDirectory dir, ref int sizeThing)
    {
        var dirSize = dir.Files.Sum(x => x.Size);
        
        foreach (var subDir in dir.SubDirectories)
        {
            dirSize += MagicSizeFunction(subDir, ref sizeThing);
        }

        if (dirSize <= 100_000) sizeThing += dirSize;

        return dirSize;
    }
    
    public static int FindClosestGreater(IDirectory dir, int deleteTarget, ref int currentDelete)
    {
        var dirSize = dir.Files.Sum(x => x.Size);
        
        foreach (var subDir in dir.SubDirectories)
        {
            dirSize += FindClosestGreater(subDir, deleteTarget, ref currentDelete);
        }

        if (dirSize >= deleteTarget && dirSize < currentDelete) currentDelete = dirSize;

        return dirSize;
    }
}