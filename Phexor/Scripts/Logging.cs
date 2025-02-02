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
public class Logging
{
    private static readonly string AppdataFolder = Path.Combine((Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)), "Phexor");
    private static readonly string LogDirectory = Path.Combine(AppdataFolder, "Logs");
    private static readonly string DailyLogFileName = string.Concat(LogDirectory, @"\",DateTime.Today.ToString("d").Replace(@"/", ".")) + ".log";

    public Logging(string log, string identifikator, bool catchLog)
    {
        LoggingProcess(log, identifikator, catchLog);
    }
    
    private void LoggingProcess(string log, string identifikator, bool catchLog)
    {
        if (!Directory.Exists(LogDirectory))
        {
            Directory.CreateDirectory(LogDirectory);
        }

        if (catchLog)
        {
            using StreamWriter writer = new StreamWriter(DailyLogFileName, true, Encoding.UTF8);
            string logText = $"{DateTime.Now:HH:mm:ss} [{identifikator}] | Catch | : {log}";
            writer.WriteLine(logText);
        }
        else
        {
            if ((identifikator == "Explorer" && SettingsControl.Log2 == 1) || (identifikator == "Settings" && SettingsControl.Log3 == 1) || (identifikator == "Scripts" && SettingsControl.Log4 == 1))
            {
                using StreamWriter writer = new StreamWriter(DailyLogFileName, true, Encoding.UTF8);
                string logText = $"{DateTime.Now:HH:mm:ss} [{identifikator}] | : {log}";
                writer.WriteLine(logText);
            }
        }
        RemoveOldLogFile();
    }

    private void RemoveOldLogFile()
    {
        if (SettingsControl.Log1 != 0)
        {
            foreach (var logs in Directory.GetFiles(LogDirectory))
            {
                bool logInRanche = false;
                for (int i = 0; i < SettingsControl.Log1; i++)
                {
                    if (logs.Substring(LogDirectory.Length + 1) == DateTime.Today.AddDays(-i).ToString("d").Replace(@"/", ".") + ".log")
                    {
                        logInRanche = true;
                    }
                }
                if (!logInRanche)
                {
                    File.Delete((LogDirectory + @"\" + logs.Substring(LogDirectory.Length + 1)));
                    LoggingProcess("Removed old Logfile", "Logging", false);
                }
            }
        }
    }
}