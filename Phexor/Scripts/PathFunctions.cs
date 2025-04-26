using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Phexor.Scripts;
// C. = Create
// G. = Generate
// V. = Variable
// L. = List
// M. = Method
// Xc. = XAML code
// C#c. = C# code

public static class PathFunctions
{
    //-----Public Methods-----\\
    public static string Undo() => ProcessUndo(); //M. to undo the last action
    public static string Redo() => ProcessRedo(); //M. to redo the last action
    public static void OpenFile(string fileName) => ProcessFile(fileName); //M. to open a file
    public static void OpenPath(string pathInput, string path) => ProcessPath(pathInput, path); //M. to open a path
    
    //-----Private Methods-----\\
    private static string ProcessRedo() //M. to redo the last action
    {
        var newPath = ""; //C. Variable to store the new path
        var pathParts = new List<string>(PathSearcher.Path.Split('\\')); pathParts.RemoveAll(s => s == ""); //Split the path into parts and ad it to a L. and remove empty parts
        var savePathParts = new List<string>(PathSearcher.SavePath.Split('\\')); savePathParts.RemoveAll(s => s == ""); //Split the save path into parts and ad it to a L. and remove empty parts
        if (pathParts.Count < savePathParts.Count) //Check if the path parts count is less than the save path parts count
        {
            var partCount = pathParts.Count +1; //C. Variable to store the part count
            foreach (var savePathPart in savePathParts) { if (partCount > 0) { newPath = newPath + savePathPart + @"\"; } partCount--; } //Rebuild the path
        }
        return newPath; //Return the new path
        Logging.Log("Redo Successful", "Scripts", false); //Log the redo action
    }
    private static string ProcessUndo() //M. to undo the last action
    {
        var newPath = ""; //C. Variable to store the new path
        var pathParts = new List<string>(PathSearcher.Path.Split('\\')); pathParts.RemoveAll(s => s == ""); //Split the path into parts and ad it to a L. and remove empty parts
        for (int i = 0; i < pathParts.Count -1; i++) { newPath = newPath + pathParts[i] + @"\"; } //Rebuild the path
        PathSearcher.Path = newPath; PathSearcher.DirectoryRemoveCount = 0; PathSearcher.FileRemoveCount = 0; //Reset the remove counts and set new path
        return newPath; //Return the new path
        Logging.Log("Undo Successful", "Scripts", false); //Log the undo action
    }
    private static void ProcessFile(string fileName) //M. to open a file
    {
        if (!string.IsNullOrEmpty(fileName)) //Check if the file name is not empty
        {
            var processStartInfo = new ProcessStartInfo { FileName = Path.Combine(PathSearcher.Path, fileName), UseShellExecute = true }; Process.Start(processStartInfo); //Start the process
            Logging.Log("OpenFile Successful", "Scripts", false); //Log the open file action
        }
    }
    private static void ProcessPath(string pathInput, string path) //M. to open a path
    {
        PathSearcher.Path = Path.Combine(pathInput, path); PathSearcher.DirectoryRemoveCount = 0; PathSearcher.FileRemoveCount = 0; //Reset the remove counts and set new path
        Logging.Log("Open Directory Successful", "Scripts", false); //Log the open directory action
    }
}