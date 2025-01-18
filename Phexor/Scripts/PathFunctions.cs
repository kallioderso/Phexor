using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Phexor.Scripts;

public class PathFunctions
{
    public static string Undo()
    {
        var newPath = "";
        var pathParts = new List<string>(PathSearcher.Path.Split('\\'));
            
        pathParts.RemoveAll(s => s == "");
        for (int i = 0; i < pathParts.Count -1; i++)
        {
            newPath = newPath + pathParts[i] + @"\";
        }
        PathSearcher.Path = newPath;
        PathSearcher.DirectoryRemoveCount = 0;
        PathSearcher.FileRemoveCount = 0;
        return newPath;
    }
    public static string Redo()
    {
        var newPath = "";

        var pathParts = new List<string>(PathSearcher.Path.Split('\\'));
        var savePathParts = new List<string>(PathSearcher.SavePath.Split('\\'));
            
        pathParts.RemoveAll(s => s == "");
        savePathParts.RemoveAll(s => s == "");
            
        if (pathParts.Count < savePathParts.Count)
        {
            var partCount = pathParts.Count +1;

            foreach (var savePathPart in savePathParts)
            {
                if (partCount > 0)
                {
                    newPath = newPath + savePathPart + @"\";
                }
                partCount--;
            }
        }
        return newPath;
    }
    
    public static void OpenFile(string fileName)
    {
        if (!string.IsNullOrEmpty(fileName))
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = Path.Combine(PathSearcher.Path, fileName),
                UseShellExecute = true
            };
            Process.Start(processStartInfo);
        }
    }
    
    public static void OpenPath(string pathInput, string path)
    {
        PathSearcher.Path = Path.Combine(pathInput, path);
        PathSearcher.DirectoryRemoveCount = 0;
        PathSearcher.FileRemoveCount = 0;
    }
}