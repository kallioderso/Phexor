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
    }

    private void ColorSettingsButton_OnClick(object sender, RoutedEventArgs e)
    {
        SettingMenu.Content = new SettingMenus.ColorSettings();
    }

    private void SymbolSettingsButton_OnClick(object sender, RoutedEventArgs e)
    {
        SettingMenu.Content = new SettingMenus.SymbolSettings();
    }
    
    private void SizesSettingsButton_OnClick(object sender, RoutedEventArgs e)
    {
        SettingMenu.Content = new SettingMenus.SizeSettings();
    }
    
    private void LoggingSettingsButton_OnClick(object sender, RoutedEventArgs e)
    {
        SettingMenu.Content = new SettingMenus.LoggingSettings();
    }

    private void Settings_OnClosed(object sender, EventArgs e)
    {
        explorerRef.Initialize();
    }
}