using System;
using System.IO;
using System.Text;


namespace Phexor.Scripts;

public class Settingsfile
{
    public static string ForegroundColor = "";
    public static string BackgroundColor = "";
    public static string OptionalColor = "";
    public static int Fields = 0;
    private static int settingCount = 0;

    public static string AppdataFolder = Path.Combine((Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)), "Phexor");
    public static string SettingsDatei = Path.Combine(AppdataFolder, "Settings.txt");

    public static void GetSettings()
    {
        try
        {
            string settings = File.ReadAllText(SettingsDatei);
            string[] setting = settings.Split('@');

            if (setting.Length >= 1) ForegroundColor = setting[0];
            if (setting.Length >= 2) BackgroundColor = setting[1];
            if (setting.Length >= 3) OptionalColor = setting[2];
            if (setting.Length >= 4) Fields = Convert.ToInt32(setting[3]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public static void SetSettings(string Foreground, string Background, string Optional, double Fields)
    {
        try
        {
            if (!Directory.Exists(AppdataFolder))
            {
                Directory.CreateDirectory(AppdataFolder);
            }

            if (File.Exists(SettingsDatei))
            {
                File.Delete(SettingsDatei);
            }
            
            using (StreamWriter writer = new StreamWriter(SettingsDatei, false, Encoding.UTF8))
            {
                
                string SettingsText = ( Foreground + "@" + Background + "@" + Optional + "@" + Fields);
                writer.WriteLine(SettingsText);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}