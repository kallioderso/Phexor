using System;
using System.Windows;
using System.Windows.Controls;
using Phexor.Scripts;

namespace Phexor.SettingMenus;
// C. = Create
// G. = Generate
// V. = Variable
// L. = List
// M. = Method
// Xc. = XAML code
// C#c. = C# code

public partial class SizeSettings : UserControl
{
    //-----Constructor-----\\
    public SizeSettings() { InitializeComponent(); GetValues(); } //Constructor to initialize the settings

    //-----Private Methods-----\\
    private void GetValues() { SettingsControl.GetSettings(); HightSize.Value = SettingsControl.Size1; TextSize.Value = SettingsControl.Size2; } //Get the values from the settings
    private void SafeSettings(object sender, RoutedEventArgs e) //M. to save the settings
    {
        if (sender == HightSize) { SettingsControl.Size1 = Convert.ToInt32(HightSize.Value); } //Set the size of the directorys and files
        else if (sender == TextSize) { SettingsControl.Size2 = Convert.ToInt32(TextSize.Value); } //Set the size of the text
        SettingsControl.SetSettings(); //Save the Settings into Appdata
    }
}