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
            using StreamWriter writer = new StreamWriter(DailyLogFileName, true, Encoding.UTF8);
            string logText = $"{DateTime.Now:HH:mm:ss} [{identifikator}] : {log}";
            writer.WriteLine(logText);
        }
        RemoveOldLogFile();
    }

    private void RemoveOldLogFile()
    {
        if (SettingsControl.LogCount != 0)
        {
            foreach (var logs in Directory.GetFiles(LogDirectory))
            {
                bool logInRanche = false;
                for (int i = 0; i < SettingsControl.LogCount; i++)
                {
                    if (logs.Substring(LogDirectory.Length + 1) == DateTime.Today.AddDays(-i).ToString("d").Replace(@"/", ".") + ".log")
                    {
                        logInRanche = true;
                    }
                }
                if (!logInRanche)
                {
                    File.Delete((LogDirectory + @"\" + logs.Substring(LogDirectory.Length + 1)));
                }
            }
        }
    }
}