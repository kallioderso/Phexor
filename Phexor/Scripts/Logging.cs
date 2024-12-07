using System;
using System.Data;
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
public static class Logging
{
    public static string AppdataFolder = Path.Combine((Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)), "Phexor"); //C. V. and get Locatin of Appdata Folder for Phexor
    public static string LogDirectory = Path.Combine(AppdataFolder, "Logs"); //C. V. and get Locatin of LogDirectory in Appdata Folder for Phexor
    public static string DailyLogFilePath = Path.Combine(AppdataFolder, LogDirectory); //C. V. and get Locatin of DailyLogFile in LogDirectory in Appdata Folder for Phexor
    private static string DailyLogFileName = string.Concat(DailyLogFilePath, "\\",DateTime.Today.ToString("d").Replace("/",".")) + ".log";
    public static int LogCount; //C. V. for RemoveOldLogFile M.
    public static void Log(string Log, string Identifikator) //M. To Add a line to the Logs
    {
        if (!Directory.Exists(LogDirectory)) //check if Log Directory Exists
        {
            Directory.CreateDirectory(LogDirectory); //C. Log Directory
        }

        using (StreamWriter writer = new StreamWriter(DailyLogFileName, true, Encoding.UTF8)) //C. or Accesses the DailyLogFile
        {
            string LogText = $"{DateTime.Now:HH:mm:ss} [{Identifikator}] : {Log}"; //C. the LogText
            writer.WriteLine(LogText); //Add new LogText to the LogFile
        }
        RemoveOldLogFile(); //call M. to Remove Outdated Log Files
    }
    
    public static void CatchLog(string Log, string Identifikator) //M. To Add a line to the Logs
    {
        if (!Directory.Exists(LogDirectory)) //check if Log Directory Exists
        {
            Directory.CreateDirectory(LogDirectory); //C. Log Directory
        }
        using (StreamWriter writer = new StreamWriter(DailyLogFileName, true, Encoding.UTF8)) //C. or Accesses the DailyLogFile
        {
            string LogText = $"{DateTime.Now:HH:mm:ss} [{Identifikator}] | Catch | : {Log}"; //C. the LogText
            writer.WriteLine(LogText); //Add new LogText to the LogFile
        }
        RemoveOldLogFile(); //call M. to Remove Outdated Log Files
    }

    public static void RemoveOldLogFile() //M. to Remove Outdated Log Files
    {
        if (LogCount != 0) //checks for an empty LogCount
        {
            foreach (var Logs in Directory.GetFiles(LogDirectory)) //Loop for Every Existing Log File
            {
                bool LogInRanche = false; //C. V. to check if file should exist
                for (int i = 0; i < LogCount; i++) //loop to check if File is in allowed time span
                {
                    if (Logs.Substring(LogDirectory.Length + 1) == DateTime.Today.AddDays(-i).ToString("d") + ".log") //if Logs is in Time Span
                    {
                        LogInRanche = true; //set LogInRanche bool to True
                    }
                }
                if (!LogInRanche) //if LogInRanche bool isn´t true
                {
                    File.Delete((LogDirectory + @"\" + Logs.Substring(LogDirectory.Length + 1))); //Delete Momentanly File
                }
            }
        }
    }
}