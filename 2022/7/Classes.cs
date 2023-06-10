using System.Collections.Generic;

namespace _7;

public record File(string Name, int Size);
public record Directory(string Name, List<File> Files, List<Directory> SubDirectories, IDirectory Parent) : IDirectory;
public record RootDirectory(List<File> Files, List<Directory> SubDirectories) : IDirectory;

public interface IDirectory
{
    List<File> Files { get; }
    List<Directory> SubDirectories { get; }
}