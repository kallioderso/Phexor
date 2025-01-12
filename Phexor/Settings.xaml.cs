using System.Windows;
using System.Windows.Controls;

namespace Phexor;

public partial class Settings : Window
{
    public Settings()
    {
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
}