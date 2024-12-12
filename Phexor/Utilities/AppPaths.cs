using System;
using System.IO;

namespace Phexor.Utilities;

public static class AppPaths
{
    // Hauptordner für die Anwendung im AppData-Verzeichnis
    public static string AppDataFolder =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Phexor");

    // Pfad zur Settings-Datei
    public static string SettingsFilePath => Path.Combine(SettingsFolder ,"Settings.json");

    public static string SettingsFolder => Path.Combine(AppDataFolder, "Settings");
    
    // Pfad zum Logs-Ordner
    public static string LogsFolder => Path.Combine(AppDataFolder ,"Logs");

    // Sicherstellen, dass Ordner existieren
    public static void EnsureAppDataFolderExists()
    {
        if (!Directory.Exists(AppDataFolder))
        {
            Directory.CreateDirectory(AppDataFolder);
        }

        if (!Directory.Exists(SettingsFolder))
        {
            Directory.CreateDirectory(SettingsFolder);
        }
        
        if (!Directory.Exists(LogsFolder))
        {
            Directory.CreateDirectory(LogsFolder);
        }
    }
}
