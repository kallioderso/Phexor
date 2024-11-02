using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Windows;


namespace Phexor.Scripts;
    // C. = Create
    // G. = Generate
    // V. = Variable
    // L. = List
    // M. = Method
    // Xc. = XAML code
    // C#c. = C# code
public class Settingsfile
{
    public static string ForegroundColor = ""; //C. V. for ForegroundColor
    public static string BackgroundColor = ""; //C. V. for BackgroundColor
    public static string SpecialColor = ""; //C. V. for SpecialColor
    public static int Fields;  //C. V. for Field Amount
    private static int settingCount = 0;  //C. V. for ForegroundColor

    public static string AppdataFolder = Path.Combine((Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)), "Phexor"); //C. V. and get Locatin of Appdata Folder for Phexor
    public static string SettingsOrdner = Path.Combine(AppdataFolder, "Settings"); //C. V. and get Locatin of Settings in Appdata Folder for Phexor
    public static string SettingsFiles = Path.Combine(AppdataFolder, SettingsOrdner + @"\Settings.txt"); //C. V. and get Locatin of Settingsfile in Settings in Appdata Folder for Phexor

    public static void GetSettings() //M. to get Saved Settings
    {
        Logging.Log("GetSettings", "SettingsFile"); //C. Log Entry
        try //Prevent Crashes
        {
            string settings = File.ReadAllText(SettingsFiles); //Read everything from the File
            string[] setting = settings.Split('@'); //Split the Readed things into multiple strings

            if (setting.Length >= 1) ForegroundColor = setting[0]; //Get Foreground Setting
            if (setting.Length >= 2) BackgroundColor = setting[1]; //Get Background Setting
            if (setting.Length >= 3) SpecialColor = setting[2]; //Get Special Color Setting
            if (setting.Length >= 4) Fields = Convert.ToInt32(setting[3]); //Get Field Amount Setting
        }
        catch (Exception e) //Used for Logs
        {
            Logging.CatchLog(Convert.ToString(e), "SettingsFile"); //use CatchLog M.
        }
    }
    
    public static void SetSettings(string Foreground, string Background, string Optional, double Fields) //M. to Save all Settings
    {
        Logging.Log("SetSettings", "SettingsFile"); //C. Log Entry
        try //prevent Crashes
        {
            if (!Directory.Exists(AppdataFolder)) //checks if The Appdatafolder Exists
            {
                Directory.CreateDirectory(AppdataFolder); //creates Appdatafolder if not existing
            }

            if (!Directory.Exists(SettingsOrdner)) //checks if Settingsfolder exists
            {
                Directory.CreateDirectory(SettingsOrdner); //creates Settingsfolder if not existing
            }
            
            if (File.Exists(SettingsFiles)) //checks if Settingfile exists
            {
                File.Delete(SettingsFiles); //deleate Settingsfile if not existing
            }
            
            using (StreamWriter writer = new StreamWriter(SettingsFiles, false, Encoding.UTF8)) //C. and Accesses the SettingsFile
            {
                
                string SettingsText = ( Foreground + "@" + Background + "@" + Optional + "@" + Fields); //C. string for write into the Settingsfile
                writer.WriteLine(SettingsText); //Writes all the Settings into the SettingsFile
            }
        }
        catch (Exception e) //used for Logs
        {
            Logging.CatchLog(Convert.ToString(e), "SettingsFile"); //use CatchLog M.
        }
    }
}