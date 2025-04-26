using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection.Emit;
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
    //-----Private Variables-----\\
    private static readonly string AppdataFolder = Path.Combine((Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)), "Phexor"); //Get Appdata folder Path
    private static readonly string LogDirectory = Path.Combine(AppdataFolder, "Logs"); //Get Log directory Path
    private static readonly string DailyLogFileName = string.Concat(LogDirectory, @"\",DateTime.Today.ToString("d").Replace(@"/", ".")) + ".log"; //Get Daily Log file name

    //-----Public Methods-----\\
    public static void Log(string log, string identifikator, bool catchLog) { LoggingProcess(log, identifikator, catchLog); RemoveOldLogFile(); } //M. to log the message and remove old log files

    //-----Private Methods-----\\
    private static void LoggingProcess(string log, string identifikator, bool catchLog) //M. to log the message
    {
        if (!Directory.Exists(LogDirectory)) { Directory.CreateDirectory(LogDirectory); } //C. Log directory if it does not exist
        if (catchLog) //Check if the log is a catch log
        {
            using StreamWriter writer = new StreamWriter(DailyLogFileName, true, Encoding.UTF8); //C. a StreamWriter to write to the log file
            string logText = $"{DateTime.Now:HH:mm:ss} [{identifikator}] | Catch | : {log}"; //C. the log text
            writer.WriteLine(logText); //Write the log text to the file
        }
        else
        {
            if ((identifikator == "Explorer" && SettingsControl.Log2 == 1) || (identifikator == "Settings" && SettingsControl.Log3 == 1) || (identifikator == "Scripts" && SettingsControl.Log4 == 1)) //Check if the logs origin is allowed
            {
                using StreamWriter writer = new StreamWriter(DailyLogFileName, true, Encoding.UTF8); //C. a StreamWriter to write to the log file
                string logText = $"{DateTime.Now:HH:mm:ss} [{identifikator}] | : {log}"; //C. the log text
                writer.WriteLine(logText); //Write the log text to the file
            }
        }
    }
    private static void RemoveOldLogFile() //M. to remove old log files
    {
        if (SettingsControl.Log1 != 0) //Check if the log retention is not zero
        {
            foreach (var logs in Directory.GetFiles(LogDirectory)) //Get all log files in the LogDirectory
            {
                bool logInRange = false; //C. Variable to check if the log is in range
                for (int i = 0; i < SettingsControl.Log1; i++) { if (logs.Substring(LogDirectory.Length + 1) == DateTime.Today.AddDays(-i).ToString("d").Replace(@"/", ".") + ".log") { logInRange = true; } } //Check if the log is in range
                if (!logInRange) //Check if the log is not in range
                {
                    File.Delete((LogDirectory + @"\" + logs.Substring(LogDirectory.Length + 1))); //Delete the log file
                    LoggingProcess("Removed old Logfile", "Logging", false); //Log the removal of the old log file
                }
            }
        }
    }
}