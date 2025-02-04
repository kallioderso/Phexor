using System;
using System.Windows;
using System.Windows.Controls;
using Phexor.Scripts;

namespace Phexor.SettingMenus;

public partial class SizeSettings : UserControl
{
    public SizeSettings()
    {
        InitializeComponent();
        GetValues();
    }

    private void GetValues()
    {
        SettingsControl.GetSettings();
        HightSize.Value = SettingsControl.Size1;
        TextSize.Value = SettingsControl.Size2;
    }

    private void SafeSettings(object sender, RoutedEventArgs e)
    {
        if (sender == HightSize)
        {
            SettingsControl.Size1 = Convert.ToInt32(HightSize.Value);
        }
        else if (sender == TextSize)
        {
            SettingsControl.Size2 = Convert.ToInt32(TextSize.Value);
        }
        SettingsControl.SetSettings();
    }
}