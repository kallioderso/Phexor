using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Phexor.Scripts;

namespace Phexor.SettingMenus;

public partial class LoggingSettings : UserControl
{
    public LoggingSettings() { InitializeComponent(); GetValues(); } //M. to initialize the LoggingSettings
    private void SwitchContent(object sender, RoutedEventArgs e) //M. to switch the state of the Buttons
    {
        var setting = sender as Button; //Get the sender of the event
        if (setting.Content.ToString() == " ") { setting.Content = "X"; SaveSettings(setting, true); } //if the state is off than switch it to on
        else if (setting.Content.ToString() == "X") { setting.Content = " "; SaveSettings(setting, false); } //if the state is on than switch it to off
    }
    
    private void SaveSettings(object sender, bool state) //M. to save the settings
    {
        if (sender == ExplorerLog) { if (state) SettingsControl.Log2 = 1; else SettingsControl.Log2 = 0; } //if Sender is ExplorerLog Button save the state
        else if (sender == SettingsLog) { if (state) SettingsControl.Log3 = 1; else SettingsControl.Log3 = 0; } //if Sender is SettingsLog Button save the state
        else if (sender == ScriptsLog) { if (state) SettingsControl.Log4 = 1; else SettingsControl.Log4 = 0; } //if Sender is ScriptsLog Button save the state
        else if (sender == LogCountSlider) SettingsControl.Log1 = LogCountSlider.Value;  //if Sender is LogCountSlider save the state
        SettingsControl.SetSettings(); //Save the Settings into Appdata
    }
    private void LogCountChanged(object sender, MouseEventArgs mouseEventArgs) => SaveSettings(LogCountSlider, true); //Save the settings when the slider is moved
    private void GetValues() //Get the values from the settings
    {
        SettingsControl.GetSettings(); //Get Saved Settings
        LogCountSlider.Value = SettingsControl.Log1; //Get Value for Log Count
        if (SettingsControl.Log2 == 1) ExplorerLog.Content = "X"; //Get Value for Explorer Log button
        if (SettingsControl.Log3 == 1) SettingsLog.Content = "X"; //Get Value for Settings Log button
        if (SettingsControl.Log4 == 1) ScriptsLog.Content = "X"; //Get Value for Scripts Log button
    }
}