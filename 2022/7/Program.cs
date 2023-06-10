using System;
using System.Collections.Generic;
using System.Linq;
using _7;

const int maxSize = 70_000_000;
const int freeSizeNeeded = 30_000_000;
var input = await System.IO.File.ReadAllLinesAsync("Input.txt");

var magicSizeNumber = 0;
var rootDir = new RootDirectory(new List<File>(), new List<Directory>());
IDirectory currentDir = rootDir;

for (var i = 1; i < input.Length; i++)
{
    var thisLine = input[i];
    var thisLineSplit = thisLine.Split(' ');
    if (thisLine is "$ cd .." && currentDir is Directory d)
    {
        currentDir = d.Parent;
        continue;
    }
    
    if (thisLine.StartsWith("$ cd"))
    {
        currentDir = currentDir.SubDirectories.Single(x => x.Name == thisLineSplit[2]);
        continue;
    }

    if (thisLine is "$ ls") continue;
    
    if (thisLine.StartsWith("dir"))
    {
        currentDir.SubDirectories.Add(new Directory(thisLineSplit[1], new(), new(), currentDir));
        continue;
    }

    currentDir.Files.Add(new File(thisLineSplit[1], int.Parse(thisLineSplit[0])));
}

var rootDirSize = Functions.MagicSizeFunction(rootDir, ref magicSizeNumber);
var deleteTarget = freeSizeNeeded - (maxSize - rootDirSize);
var deleteActual = int.MaxValue;
Functions.FindClosestGreater(rootDir, deleteTarget, ref deleteActual);

Console.WriteLine(magicSizeNumber);

Console.WriteLine(deleteActual);