using System;
using System.Windows;
using Phexor.Scripts;

namespace Phexor;

public partial class Settings
{
    private readonly Explorer _explorerRef;
    public Settings(Explorer explorer)
    {
        _explorerRef = explorer;
        InitializeComponent();
        SettingMenu.Content = new SettingMenus.ColorSettings();
        Text2.Text = "Color Settings";
        var log = new Logging("Initialized", "Settings", false);
    }

    private void ColorSettingsButton_OnClick(object sender, RoutedEventArgs e)
    {
        var log = new Logging("OpenColorSettings", "Settings", false);
        SettingMenu.Content = new SettingMenus.ColorSettings();
        Text2.Text = "Color Settings";
    }

    private void SymbolSettingsButton_OnClick(object sender, RoutedEventArgs e)
    {
        var log = new Logging("OpenSymbolSettings", "Settings", false);
        SettingMenu.Content = new SettingMenus.SymbolSettings();
        Text2.Text = "Symbol Settings";
    }
    
    private void SizesSettingsButton_OnClick(object sender, RoutedEventArgs e)
    {
        var log = new Logging("OpenSizeSettings", "Settings", false);
        SettingMenu.Content = new SettingMenus.SizeSettings();
        Text2.Text = "Size Settings";
    }
    
    private void LoggingSettingsButton_OnClick(object sender, RoutedEventArgs e)
    {
        var log = new Logging("OpenLoggingSettings", "Settings", false);
        SettingMenu.Content = new SettingMenus.LoggingSettings();
        Text2.Text = "Logging Settings";
    }

    private void Settings_OnClosed(object sender, EventArgs e)
    {
        var log = new Logging("ReturnToExplorer", "Settings", false);
        _explorerRef.Initialize();
    }
}