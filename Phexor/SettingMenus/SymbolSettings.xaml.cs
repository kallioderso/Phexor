using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;
using Phexor.Scripts;

namespace Phexor.SettingMenus
{
    public partial class SymbolSettings : UserControl
    {
        //-----private Variables-----\\
        private Path _currentPath;
        
        //-----Main Method-----\\
        public SymbolSettings()
        {
            InitializeComponent();
            this.Loaded += SetSymbols;
            SizeChanged += UserControl_SizeChanged;
            this.ColorCodeTextBox.TextChanged += ColorCodeTextBox_TextChanged;
        }
        
        //-----Private Methods-----\\
        private void ColorCodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { ColorCodeTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorCodeTextBox.Text)!); }
            catch (Exception exception) { Logging.Log(exception.Message, "Script", true); }
        }
        private void SetSymbols(object sender, RoutedEventArgs e)
        {
            try
            {
                SettingsControl.GetSettings(); // Einstellungen laden
                if (SettingsIcon != null) SettingsIcon.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Symbol1)!);
                if (UndoIcon != null) UndoIcon.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Symbol2)!);
                if (RedoIcon != null) RedoIcon.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Symbol3)!);
            }
            catch (Exception ex) { Logging.Log(ex.Message, "SetSymbols", true); }
        }
        private void OpenColorPicker(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Content is Viewbox viewbox && viewbox.Child is Path path)
            {
                _currentPath = path;
                if (_currentPath.Fill is SolidColorBrush brush) ColorCodeTextBox.Text = brush.Color.ToString();
                ColorPickerPopup.IsOpen = true;
            }
        }
        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double gridWidth = ButtonGrid.ActualWidth; double gridHeight = ButtonGrid.ActualHeight; double cellSize = Math.Min(gridWidth / 5, gridHeight / 5); // 5x5 Raster
            foreach (UIElement child in ButtonGrid.Children) { if (child is Button button) { button.Width = cellSize - 10; button.Height = cellSize - 10; } }
        }
        private void ApplyColorCode_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPath != null)
            {
                try
                {
                    var color = (Color)ColorConverter.ConvertFromString(ColorCodeTextBox.Text)!;
                    _currentPath.Fill = new SolidColorBrush(color);
                    if (_currentPath == SettingsIcon) SettingsControl.Symbol1 = ColorCodeTextBox.Text;
                    else if (_currentPath == UndoIcon) SettingsControl.Symbol2 = ColorCodeTextBox.Text;
                    else if (_currentPath == RedoIcon) SettingsControl.Symbol3 = ColorCodeTextBox.Text;
                    SettingsControl.SetSettings();
                }
                catch (Exception ex) { Logging.Log(ex.Message, "ApplyColorCode_Click", true); }
            }
        }
        private void ResetColorCode_Click(object sender, RoutedEventArgs e) { if (_currentPath != null) { ColorCodeTextBox.Text = "#000000"; ApplyColorCode_Click(sender, e); } }
        private void ColorPickerPopup_Closed(object sender, EventArgs e)
        {
            if (_currentPath != null)
            {
                try { _currentPath.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorCodeTextBox.Text)!); SettingsControl.SetSettings(); }
                catch (Exception ex) { Logging.Log(ex.Message, "ColorPickerPopup_Closed", true); }
            }
        }
    }
}
