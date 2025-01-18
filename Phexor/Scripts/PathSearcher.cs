using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Phexor.Scripts;

public class PathSearcher
{
    private readonly Explorer explorerRef;

    public PathSearcher(Explorer explorer)
    {
        this.explorerRef = explorer;
    }
    
    public static string Path = "";
    public static string SavePath = "";
    public static int DirectoryRemoveCount;
    public static int FileRemoveCount;
    
    public void SearchPath()
    {
        SetPath(Path);
        SearchFiles(FileRemoveCount);
        SearchDirectories(DirectoryRemoveCount);
    }
    
    private void SetPath(string Input) //Path setting M.
    {
        List<string> setPathParts = new List<string>(Input.Split('\\'));
        setPathParts.RemoveAll(s => s == "");
        Path = "";
        foreach (var setPathPart in setPathParts)
        {
            Path = Path + setPathPart + @"\";
        }
    }

    private void SearchFiles(int removeCount)
    {
        if (System.IO.Path.Exists(Path))
        {
            foreach (var file in Directory.GetFiles(Path))
            {
                if (removeCount != 0)
                {
                    removeCount--;
                }
                else
                {
                    var fileTextBlock = new TextBlock
                    {
                        Height = 30,
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")!),
                        FontSize = 10,
                        Text = file.Substring(Path.Length),
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    explorerRef.AddFile(fileTextBlock);
                }
            }
        }
    }
    
    private void SearchDirectories(int removeCount)
    {
        if (System.IO.Path.Exists(Path))
        {
            foreach (var directory in Directory.GetDirectories(Path))
            {
                if (removeCount != 0)
                {
                    removeCount--;
                }
                else
                {
                    var directoryTextBlock = new TextBlock
                    {
                        Height = 30,
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")!),
                        FontSize = 10,
                        Text = directory.Substring(Path.Length),
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                    explorerRef.AddDirectory(directoryTextBlock);
                }
            }
        }
    }
}