using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using Phexor.Scripts;

namespace Phexor.SettingMenus
{
    public partial class ColorSettings : UserControl
    {
        private Button _currentButton;
        public ColorSettings()
        {
            InitializeComponent();
            this.Loaded += SetColor;
            this.SizeChanged += ColorSettings_Size;
            this.ColorCodeTextBox.TextChanged += ColorCodeTextBox_TextChanged;
        }

        private void ColorCodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { ColorCodeTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorCodeTextBox.Text)!); }
            catch (Exception exception) { Logging.Log(exception.Message, "Script", true); }
        }
        private void ColorSettings_Size(object sender, RoutedEventArgs e)
        {
            try { MainPanel.Height = this.ActualHeight - 42; MainPanel.Width = this.ActualWidth - 42; }
            catch (Exception exception) { Logging.Log(exception.Message, "Script", true); }
            PathInput.Height = MainPanel.Height / 10;
            ButtonField.Height = MainPanel.Height / 10;
            StackPanelFields.Height = MainPanel.Height / 10 * 8;
            Directorys.Height = MainPanel.Height / 10 * 8;
            Directorys.Width = MainPanel.Width / 4;
            Files.Height = MainPanel.Height / 10 * 8;
            Files.Width = MainPanel.Width / 4 * 3;
        }
        private void SetColor(object sender, RoutedEventArgs e)
        {
            ColorSettings_Size(sender, e);
            SettingsControl.GetSettings(); //C. M. to get Saved Settings
            Border1.Background = new SolidColorBrush(Colors.Black);
            Border2.Background = new SolidColorBrush(Colors.Black);
            Border3.Background = new SolidColorBrush(Colors.Black);
            PathInput.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color1)!);
            ButtonField.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color2)!);
            Directorys.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color3)!);
            Files.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color4)!);
        }
        private void PathInput_Click(object sender, RoutedEventArgs e) => ShowColorPicker(sender as Button);
        private void ButtonField_Click(object sender, RoutedEventArgs e) => ShowColorPicker(sender as Button);
        private void Directorys_Click(object sender, RoutedEventArgs e) => ShowColorPicker(sender as Button);
        private void Files_Click(object sender, RoutedEventArgs e) => ShowColorPicker(sender as Button);
        private void ShowColorPicker(Button button) { _currentButton = button; ColorCodeTextBox.Text = ((SolidColorBrush)button.Background).Color.ToString(); ColorPickerPopup.IsOpen = true; }
        private void ApplyColorCode_Click(object sender, RoutedEventArgs e)
        {
            if (_currentButton != null)
            {
                try
                {
                    var color = (Color)ColorConverter.ConvertFromString(ColorCodeTextBox.Text)!;
                    _currentButton.Background = new SolidColorBrush(color);
                    if (_currentButton == PathInput) { SettingsControl.Color1 = ColorCodeTextBox.Text; SettingsControl.SetSettings(); }
                    else if (_currentButton == ButtonField) { SettingsControl.Color2 = ColorCodeTextBox.Text; SettingsControl.SetSettings(); }
                    else if (_currentButton == Directorys) { SettingsControl.Color3 = ColorCodeTextBox.Text; SettingsControl.SetSettings(); }
                    else if (_currentButton == Files) { SettingsControl.Color4 = ColorCodeTextBox.Text; SettingsControl.SetSettings(); }
                }
                catch (FormatException) { MessageBox.Show("Invalid color code. Please enter a valid color code.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
        private void ColorPickerPopup_Closed(object sender, EventArgs e) => _currentButton = null;
        private void ResetColorCode_Click(object sender, RoutedEventArgs e)
        {
            if (_currentButton != null)
            {
                ColorCodeTextBox.Text = ((SolidColorBrush)_currentButton.Background).Color.ToString();
                _currentButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF")!);
                if (_currentButton == PathInput) { SettingsControl.Color1 = "#FFFFFF"; SettingsControl.SetSettings(); }
                else if (_currentButton == ButtonField) { SettingsControl.Color2 = "#FFFFFF"; SettingsControl.SetSettings(); }
                else if (_currentButton == Directorys) { SettingsControl.Color3 = "#FFFFFF"; SettingsControl.SetSettings(); }
                else if (_currentButton == Files) { SettingsControl.Color4 = "#FFFFFF"; SettingsControl.SetSettings(); }
            }
        }
    }
}