using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Phexor.Scripts;
// C. = Create
// G. = Generate
// V. = Variable
// L. = List
// M. = Method
// Xc. = XAML code
// C#c. = C# code
public class Logging
{
    public static string AppdataFolder = Path.Combine((Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)), "Phexor"); //C. V. and get Locatin of Appdata Folder for Phexor
    public static string LogDirectory = Path.Combine(AppdataFolder, "Logs"); //C. V. and get Locatin of LogDirectory in Appdata Folder for Phexor
    public static string DailyLogFile = Path.Combine(AppdataFolder, LogDirectory + @"\" + DateTime.Today.ToString("d") + ".log"); //C. V. and get Locatin of DailyLogFile in LogDirectory in Appdata Folder for Phexor
    public static void Log(string Log, string Identifikator) //M. To Add a line to the Logs
    {
        if (!Directory.Exists(LogDirectory)) //check if Log Directory Exists
        {
            Directory.CreateDirectory(LogDirectory); //C. Log Directory
        }
        using (StreamWriter writer = new StreamWriter(DailyLogFile, true, Encoding.UTF8)) //C. or Accesses the DailyLogFile
        {
            string LogText = $"{DateTime.Now:HH:mm:ss} [{Identifikator}] : {Log}"; //C. the LogText
            writer.WriteLine(LogText); //Add new LogText to the LogFile
        }
    }
    
    public static void CatchLog(string Log, string Identifikator) //M. To Add a line to the Logs
    {
        if (!Directory.Exists(LogDirectory)) //check if Log Directory Exists
        {
            Directory.CreateDirectory(LogDirectory); //C. Log Directory
        }
        using (StreamWriter writer = new StreamWriter(DailyLogFile, true, Encoding.UTF8)) //C. or Accesses the DailyLogFile
        {
            string LogText = $"{DateTime.Now:HH:mm:ss} [{Identifikator}] | Catch | : {Log}"; //C. the LogText
            writer.WriteLine(LogText); //Add new LogText to the LogFile
        }
    }
}