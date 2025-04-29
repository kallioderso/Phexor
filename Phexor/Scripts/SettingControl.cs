using System;
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

public static class SettingsControl
{
    //-----Public Variables for Color-----\\
    public static string Color1 = "FFFFFF"; //C. V. for Color1
    public static string Color2 = "FFFFFF"; //C. V. for Color2
    public static string Color3 = "FFFFFF"; //C. V. for Color3
    public static string Color4 = "FFFFFF"; //C. V. for Color4
    
    //-----Public Variables for Sizes-----\\
    public static int Size1 = 30; //C. V. for Size1
    public static int Size2 = 10; //C. V. for Size2
    
    //-----Public Variables for Logging-----\\
    public static int LogCount = 7; //C. V. for LogCount
    public static double Log1 = 0; //C. V. for Log1
    public static int Log2 = 0; //C. V. for Log2
    public static int Log3 = 0; //C. V. for Log3
    public static int Log4 = 0; //C. V. for Log4

    //-----Private Variables-----\\
    private static string AppdataFolder = Path.Combine((Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)), "Phexor"); //C. V. and get Locatin of Appdata Folder for Phexor
    private static string SettingsOrdner = Path.Combine(AppdataFolder, "Settings"); //C. V. and get Locatin of Settings in Appdata Folder for Phexor
    private static string ColorSettingsFile = Path.Combine(AppdataFolder, SettingsOrdner + @"\ColorSettings.settings"); //C. V. and get Locatin of ColorSettingsfile in Settings in Appdata Folder for Phexor
    private static string SymbolSettingsFile = Path.Combine(AppdataFolder, SettingsOrdner + @"\SymbolSettings.settings"); //C. V. and get Locatin of SymbolSettingsfile in Settings in Appdata Folder for Phexor
    private static string SizeSettingsFile = Path.Combine(AppdataFolder, SettingsOrdner + @"\SizeSettings.settings"); //C. V. and get Locatin of SizeSettingsfile in Settings in Appdata Folder for Phexor
    private static string LoggingSettingsFile = Path.Combine(AppdataFolder, SettingsOrdner + @"\LoggingSettings.settings"); //C. V. and get Locatin of LoggingSettingsfile in Settings in Appdata Folder for Phexor
    
    //-----Puplic Methods-----\\
    public static void GetSettings() => Get(); //C. M. to Call the GetSettings Method
    public static void SetSettings() => Set(); //C. M. to Call the SetSettings Method
    
    //-----Private Methods-----\\
    private static void CheckforDirectoryStructure() {  if (!Directory.Exists(AppdataFolder)) { Directory.CreateDirectory(AppdataFolder); } if (!Directory.Exists(SettingsOrdner)) { Directory.CreateDirectory(SettingsOrdner); } } //C. M. to Check if the Directory Structure is existing
    private static void Get()
    {
        Logging.Log("Get", "Scripts", false); //C. M. to Log the Get Method
        CheckforDirectoryStructure(); //C. M. to Check if the Directory Structure is existing
        GetColorSettings(); GetSymbolSettings(); GetSizeSettings(); GetLoggingSettings(); //C. M. to get all Settings
    }
    private static void Set()
    {
        Logging.Log("SetSettings", "Scripts", false); //C. M. to Log the SetSettings Method
        CheckforDirectoryStructure(); //C. M. to Check if the Directory Structure is existing
        SetColorSettings(); SetSymbolSettings(); SetSizeSettings(); SetLoggingSettings(); //C. M. to set all Settings
    }
    
    //-----Get Methods-----\\
    private static void GetColorSettings()
    {
        Logging.Log("GetColorSettings", "Scripts", false); //C. M. to Log the GetColorSettings Method
        if (File.Exists(ColorSettingsFile))
        {
            string settings = File.ReadAllText(ColorSettingsFile); //Read everything from the File
            string[] setting = settings.Split('@'); //Split the Readed things into multiple strings

            if (setting.Length >= 1) Color1 = setting[0]; //Get Color1 Setting
            if (setting.Length >= 2) Color2 = setting[1]; //Get Color2 Setting
            if (setting.Length >= 3) Color3 = setting[2]; //Get Color3 Setting
            if (setting.Length >= 4) Color4 = setting[3]; //Get Color4 Setting
        }
        else
        {
            using (StreamWriter writer = new StreamWriter(ColorSettingsFile, false, Encoding.UTF8)) //C. and Accesses the ColorSettingsFile
            {
                const string colorSettingsText = ( "#FFFFFF" + "@" + "#FFFFFF" + "@" + "#FFFFFF" + "@" + "#FFFFFF"); //C. string for write into the ColorSettingsfile
                writer.WriteLine(colorSettingsText); //Writes all the Settings into the ColorSettingsFile
            }
            GetColorSettings(); //Call the GetColorSettings Method again to get the default values
        }
    }
    private static void GetSymbolSettings()
    {
        Logging.Log("GetSymbolSettings", "Scripts", false); //C. M. to Log the GetSymbolSettings Method
        if (File.Exists(SymbolSettingsFile))
        {
            string settings = File.ReadAllText(SymbolSettingsFile); //Read everything from the File
            string[] setting = settings.Split('@'); //Split the Readed things into multiple strings
        }
        else
        {
            using (StreamWriter writer = new StreamWriter(SymbolSettingsFile, false, Encoding.UTF8)) //C. and Accesses the SymbolSettingsFile
            {
                const string symbolSettingsText = ( "" + "@" + "" + "@" + "" + "@" + ""); //C. string for write into the SymbolSettingsfile
                writer.WriteLine(symbolSettingsText); //Writes all the Settings into the SymbolSettingsFile
            }
            GetSymbolSettings(); //Call the GetSymbolSettings Method again to get the default values
        }
    }
    private static void GetSizeSettings()
    {
        Logging.Log("GetSizeSettings", "Scripts", false); //C. M. to Log the GetSizeSettings Method
        if (File.Exists(SizeSettingsFile))
        {
            string settings = File.ReadAllText(SizeSettingsFile); //Read everything from the File
            string[] setting = settings.Split('@'); //Split the Readed things into multiple strings
            if (setting.Length >= 1) Size1 = Convert.ToInt32(setting[0]); //Get Size1 Setting
            if (setting.Length >= 2) Size2 = Convert.ToInt32(setting[1]); //Get Size2 Setting
        }
        else
        {
            using (StreamWriter writer = new StreamWriter(SizeSettingsFile, false, Encoding.UTF8)) //C. and Accesses the SizeSettingsFile
            {
                const string sizeSettingsText = ( "30" + "@" + "10"); //C. string for write into the SizeSettingsfile
                writer.WriteLine(sizeSettingsText); //Writes all the Settings into the SizeSettingsFile
            }
            GetSizeSettings(); //Call the GetSizeSettings Method again to get the default values
        }
    }
    private static void GetLoggingSettings()
    {
        Logging.Log("GetLoggingSettings", "Scripts", false); //C. M. to Log the GetLoggingSettings Method
        if (File.Exists(LoggingSettingsFile))
        {
            string settings = File.ReadAllText(LoggingSettingsFile); //Read everything from the File
            string[] setting = settings.Split('@'); //Split the Readed things into multiple strings
            if (setting.Length >= 1) Log1 = Convert.ToDouble(setting[0]); //Get Log1 Setting
            if (setting.Length >= 2) Log2 = Convert.ToInt32(setting[1]); //Get Log2 Setting
            if (setting.Length >= 3) Log3 = Convert.ToInt32(setting[2]); //Get Log3 Setting
            if (setting.Length >= 4) Log4 = Convert.ToInt32(setting[3]); //Get Log4 Setting
        }
        else
        {
            using (StreamWriter writer = new StreamWriter(LoggingSettingsFile, false, Encoding.UTF8)) //C. and Accesses the LoggingSettingsFile
            {
                const string loggingSettingsText = ( "7" + "@" + "0" + "@" + "0" + "@" + "0"); //C. string for write into the LoggingSettingsfile
                writer.WriteLine(loggingSettingsText); //Writes all the Settings into the LoggingSettingsFile
            }
            GetLoggingSettings(); //Call the GetLoggingSettings Method again to get the default values
        }
    }
    
    //-----Set Methods-----\\
    private static void SetColorSettings()
    {
        Logging.Log("SetColorSettings", "Scripts", false); //C. M. to Log the SetColorSettings Method
        if (File.Exists(ColorSettingsFile)) { File.Delete(ColorSettingsFile); } //deleate Settingsfile if not existing
        using (StreamWriter writer = new StreamWriter(ColorSettingsFile, false, Encoding.UTF8)) //C. and Accesses the ColorSettingsFile
        {
            string colorSettingsText = ( Color1 + "@" + Color2 + "@" + Color3 + "@" + Color4); //C. string for write into the ColorSettingsfile
            writer.WriteLine(colorSettingsText); //Writes all the Settings into the ColorSettingsFile
        }
    }
    private static void SetSymbolSettings()
    {
        Logging.Log("SetSymbolSettings", "Scripts", false); //C. M. to Log the SetSymbolSettings Method
        if (File.Exists(SymbolSettingsFile)) { File.Delete(SymbolSettingsFile); } //deleate Settingsfile if not existing
        using (StreamWriter writer = new StreamWriter(SymbolSettingsFile, false, Encoding.UTF8)) //C. and Accesses the SymbolSettingsFile
        {
            const string symbolSettingsText = ( "" + "@" + "" + "@" + "" + "@" + ""); //C. string for write into the SymbolSettingsfile
            writer.WriteLine(symbolSettingsText); //Writes all the Settings into the SymbolSettingsFile
        }
    }
    private static void SetSizeSettings()
    {
        Logging.Log("SetSizeSettings", "Scripts", false); //C. M. to Log the SetSizeSettings Method
        if (File.Exists(SizeSettingsFile)) { File.Delete(SizeSettingsFile); } //deleate Settingsfile if not existing
        using (StreamWriter writer = new StreamWriter(SizeSettingsFile, false, Encoding.UTF8)) //C. and Accesses the SizeSettingsFile
        {
            string sizeSettingsText = ( Size1 + "@" + Size2); //C. string for write into the SizeSettingsfile
            writer.WriteLine(sizeSettingsText); //Writes all the Settings into the SizeSettingsFile
        }
    }
    private static void SetLoggingSettings()
    {
        Logging.Log("SetLoggingSettings", "Scripts", false); //C. M. to Log the SetLoggingSettings Method
        if (File.Exists(LoggingSettingsFile)) { File.Delete(LoggingSettingsFile); } //deleate Settingsfile if not existing
        using (StreamWriter writer = new StreamWriter(LoggingSettingsFile, false, Encoding.UTF8)) //C. and Accesses the LoggingSettingsFile
        {
            string loggingSettingsText = ( Log1 + "@" + Log2 + "@" + Log3 + "@" + Log4);
            writer.WriteLine(loggingSettingsText); //Writes all the Settings into the LoggingSettingsFile
        }
    }
}