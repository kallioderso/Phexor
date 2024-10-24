using System;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Windows;


namespace Phexor.Scripts;

public class Settingsfile
{
    public static string ForegroundColor = "";
    public static string BackgroundColor = "";
    public static string OptionalColor = "";
    public static int Fields = 0;
    public static bool ScrollRad = false;
    private static int settingCount = 0;

    public static string AppdataFolder = Path.Combine((Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)), "Phexor");
    public static string SettingsOrdner = Path.Combine(AppdataFolder, "Settings");
    public static string SettingsDatei = Path.Combine(AppdataFolder, SettingsOrdner + @"\Settings.txt");

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
            if (setting.Length >= 4) ScrollRad = Convert.ToBoolean(setting[4]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public static void SetSettings(string Foreground, string Background, string Optional, double Fields, bool ScrollMenu)
    {
        try
        {
            if (!Directory.Exists(AppdataFolder))
            {
                Directory.CreateDirectory(AppdataFolder);
            }

            if (!Directory.Exists(SettingsOrdner))
            {
                Directory.CreateDirectory(SettingsOrdner);
            }
            
            if (File.Exists(SettingsDatei))
            {
                File.Delete(SettingsDatei);
            }
            
            using (StreamWriter writer = new StreamWriter(SettingsDatei, false, Encoding.UTF8))
            {
                
                string SettingsText = ( Foreground + "@" + Background + "@" + Optional + "@" + Fields + "@" + ScrollMenu);
                writer.WriteLine(SettingsText);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}