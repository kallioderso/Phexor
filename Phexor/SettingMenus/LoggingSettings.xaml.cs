using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Phexor.Scripts;

namespace Phexor.SettingMenus;

public partial class LoggingSettings : UserControl
{
    public LoggingSettings()
    {
        InitializeComponent();
        GetValues();
    }
    
    private void SwitchContent(object sender, RoutedEventArgs e)
    {
        var setting = sender as Button;
        if (setting.Content.ToString() == " ")
        {
            setting.Content = "X";
            SaveSettings(setting, e, true);
        }
        else if (setting.Content.ToString() == "X")
        {
            setting.Content = " ";
            SaveSettings(setting, e, false);
        }
    }
    
    private void SaveSettings(object sender, RoutedEventArgs e, bool state)
    {
        if (sender == ExplorerLog)
        {
            if (state)
            {
                SettingsControl.Log2 = "1";
            }
            else
            {
                SettingsControl.Log2 = "0";
            }
        }
        else if (sender == SettingsLog)
        {
            if (state)
            {
                SettingsControl.Log3 = "1";
            }
            else
            {
                SettingsControl.Log3 = "0";
            }
        }
        else if (sender == ScriptsLog)
        {
            if (state)
            {
                SettingsControl.Log4 = "1";
            }
            else
            {
                SettingsControl.Log4 = "0";
            }
        }
        else if (sender == LogCountSlider)
        {
            SettingsControl.Log1 = LogCountSlider.Value.ToString();
        }
        SettingsControl.SetSettings();
    }

    private void LogCountChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        SaveSettings(LogCountSlider, e, true);
    }

    private void GetValues()
    {
        SettingsControl.GetSettings();
        LogCountSlider.Value = Convert.ToDouble(SettingsControl.Log1);
        if (SettingsControl.Log2 == "1")
        {
            ExplorerLog.Content = "X";
        }
        if (SettingsControl.Log3 == "1")
        {
            SettingsLog.Content = "X";
        }
        if (SettingsControl.Log4 == "1")
        {
            ScriptsLog.Content = "X";
        }        
    }
}