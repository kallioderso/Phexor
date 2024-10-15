using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Phexor.Scripts;
using static Phexor.Scripts.Settingsfile;
using Xceed.Wpf.Toolkit;


namespace Phexor;

public partial class SettingsWindow : Window
{
    private static string Foreground;
    private static string Background;
    private static string Optional;
    private int _fields;
    private SolidColorBrush ForegroundBrush;
    private SolidColorBrush BackgroundBrush;
    private SolidColorBrush OptionalBrush;
    public SettingsWindow()
    {
        this.Icon = new BitmapImage(new Uri("pack://application:,,,/Grafiks/Icon.ico"));
        InitializeComponent();
        var borders = new Border[] { Border1, Border2, Border3, Border4, Border5, Border6, Border7, Border8};
        var TextBlocks = new TextBlock[] { TextBlock1, TextBlock2, TextBlock3, TextBlock4, TextBlock5};
        GetSettings();
        Foreground = ForegroundColor;
        Background = BackgroundColor;
        Optional = OptionalColor;
        _fields = Fields;
        ForegroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ForegroundColor)!);
        BackgroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(BackgroundColor)!);
        OptionalBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(OptionalColor)!);
        FelderInput.Value = _fields;
        // colorPicker1.SelectedColor = (Color)ColorConverter.ConvertFromString(ForegroundColor);
        // colorPicker2.SelectedColor = (Color)ColorConverter.ConvertFromString(BackgroundColor);
        // colorPicker3.SelectedColor = (Color)ColorConverter.ConvertFromString(OptionalColor);

        foreach (var border in borders)
        {
            border.BorderBrush = BackgroundBrush;
        }

        foreach (var Textblock in TextBlocks)
        {
            Textblock.Foreground = ForegroundBrush;
        }
    }
    
    private void Settings_OnClosing(object? sender, CancelEventArgs e)
    {
        Phexor.MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
    }

    private void Save(object sender, MouseButtonEventArgs mouseButtonEventArgs)
    {
        //SetSettings(colorPicker1.SelectedColor.ToString(), colorPicker2.SelectedColor.ToString(), colorPicker3.SelectedColor.ToString(), FelderInput.Value);
        this.Close();
    }
}