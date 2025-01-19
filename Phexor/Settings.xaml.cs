using System;
using System.Windows;

namespace Phexor;

public partial class Settings
{
    private readonly Explorer explorerRef;
    public Settings(Explorer explorer)
    {
        explorerRef = explorer;
        InitializeComponent();
        SettingMenu.Content = new SettingMenus.ColorSettings();
        Text2.Text = "Color Settings";
    }

    private void ColorSettingsButton_OnClick(object sender, RoutedEventArgs e)
    {
        SettingMenu.Content = new SettingMenus.ColorSettings();
        Text2.Text = "Color Settings";
    }

    private void SymbolSettingsButton_OnClick(object sender, RoutedEventArgs e)
    {
        SettingMenu.Content = new SettingMenus.SymbolSettings();
        Text2.Text = "Symbol Settings";
    }
    
    private void SizesSettingsButton_OnClick(object sender, RoutedEventArgs e)
    {
        SettingMenu.Content = new SettingMenus.SizeSettings();
        Text2.Text = "Size Settings";
    }
    
    private void LoggingSettingsButton_OnClick(object sender, RoutedEventArgs e)
    {
        SettingMenu.Content = new SettingMenus.LoggingSettings();
        Text2.Text = "Logging Settings";
    }

    private void Settings_OnClosed(object sender, EventArgs e)
    {
        explorerRef.Initialize();
    }
}