using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Phexor.Scripts;
// C. = Create
// G. = Generate
// V. = Variable
// L. = List
// M. = Method
// Xc. = XAML code
// C#c. = C# code

public static class PathSearcher
{
    //-----Public Variables-----\\
    public static string Path = "";
    public static string SavePath = "";
    public static int DirectoryRemoveCount;
    public static int FileRemoveCount;
    
    //-----Public initialize Method-----\\
    public static void SearchPath(Explorer explorer) => Initialize(explorer); //Public method to initialize the path search
    
    //-----Private initialize Method-----\\
    private static void Initialize(Explorer explorer) //M. to Initialize the path search
    {
        explorer.Directorys.Children.Clear(); explorer.Files.Children.Clear(); //Clear the explorer panels
        SetPath(Path); SearchFiles(FileRemoveCount, explorer); SearchDirectories(DirectoryRemoveCount, explorer); //Search for files and directories

    }
    
    //-----Private Methods-----\\
    private static void SetPath(string Input) //Path setting M.
    {
        List<string> setPathParts = new List<string>(Input.Split('\\')); //Split the path into parts and ad it to a L.
        setPathParts.RemoveAll(s => s == ""); //Remove empty parts
        Path = ""; //Initialize the path
        foreach (var setPathPart in setPathParts) { Path = Path + setPathPart + @"\"; } //Rebuild the path
        Logging.Log("Path Modified", "Scripts", false); //Log the path modification
    }
    private static void SearchFiles(int removeCount, Explorer explorer) //Search for files in the path
    {
        if (System.IO.Path.Exists(Path)) //Check if the path exists
        {
            Logging.Log("Search Files", "Scripts", false); //Log the search for files
            foreach (var file in Directory.GetFiles(Path)) //Get all files in the path
            {
                if (removeCount != 0) { removeCount--; } //If the remove count is not zero, decrement it
                else
                {
                    var fileTextBlock = new TextBlock { Height = SettingsControl.Size1, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")!), FontSize = SettingsControl.Size2, Text = file.Substring(Path.Length), VerticalAlignment = VerticalAlignment.Center }; //Create a TextBlock for the file
                    explorer.AddFile(fileTextBlock); //Add the file TextBlock to the explorer
                }
            }
        }
    }
    private static void SearchDirectories(int removeCount, Explorer explorer) //Search for directories in the path
    {
        if (System.IO.Path.Exists(Path)) //Check if the path exists
        {
            Logging.Log("Search Directories", "Scripts", false); //Log the search for directories
            foreach (var directory in Directory.GetDirectories(Path)) //Get all directories in the path
            {
                if (removeCount != 0) { removeCount--; } //If the remove count is not zero, decrement it
                else
                {
                    var directoryTextBlock = new TextBlock { Height = SettingsControl.Size1, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")!), FontSize = SettingsControl.Size2, Text = directory.Substring(Path.Length), VerticalAlignment = VerticalAlignment.Center, }; //Create a TextBlock for the directory
                    explorer.AddDirectory(directoryTextBlock); //Add the directory TextBlock to the explorer
                }
            }
        }
    }
}