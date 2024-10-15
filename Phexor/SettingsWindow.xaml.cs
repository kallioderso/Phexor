using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Phexor.Scripts;
using static Phexor.Scripts.Settingsfile;


namespace Phexor;

public partial class SettingsWindow : Window
{
    private bool ColorSelecting = false;
    private static string Foreground;
    private static string Background;
    private static string Optional;
    private int _fields;
    private SolidColorBrush ForegroundBrush;
    private SolidColorBrush BackgroundBrush;
    private SolidColorBrush OptionalBrush;
    private int wheelRadius = 45;
    public SettingsWindow()
    {
        this.Icon = new BitmapImage(new Uri("pack://application:,,,/Grafiks/Icon.ico"));
        InitializeComponent();
        DrawColorWheels();
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
        TextColorWheel1.Text = ForegroundColor;
        TextColorWheel2.Text = BackgroundColor;
        TextColorWheel3.Text = OptionalColor;
        FelderInput.Value = _fields;
        SelectedColorDisplay1.Background = ForegroundBrush;
        SelectedColorDisplay2.Background = BackgroundBrush;
        SelectedColorDisplay3.Background = OptionalBrush;

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
        SetSettings(Foreground, Background, Optional, FelderInput.Value);
        this.Close();
    }
    
     private void DrawColorWheels()
        {
            int size = wheelRadius * 2;
            WriteableBitmap bitmap = new WriteableBitmap(size, size, 96, 96, PixelFormats.Bgra32, null);
            byte[] pixels = new byte[size * size * 4];

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    int cx = x - wheelRadius;
                    int cy = y - wheelRadius;
                    double distance = Math.Sqrt(cx * cx + cy * cy);

                    if (distance <= wheelRadius)
                    {
                        double angle = Math.Atan2(cy, cx) * (180 / Math.PI) + 180;
                        double saturation = distance / wheelRadius;
                        
                        Color color = HsvToRgb(angle, saturation, 1.0);

                        int index = (y * size + x) * 4;
                        pixels[index + 0] = color.B;
                        pixels[index + 1] = color.G;
                        pixels[index + 2] = color.R;
                        pixels[index + 3] = 255;
                    }
                }
            }
            
            bitmap.WritePixels(new Int32Rect(0, 0, size, size), pixels, size * 4, 0);
            
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = bitmap;
            ColorWheel1.Background = brush;
            ColorWheel2.Background = brush;
            ColorWheel3.Background = brush;
        }
     
        private Color HsvToRgb(double hue, double saturation, double value)
        {
            int h = (int)(hue / 60) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);
            byte v = (byte)(value * 255);
            byte p = (byte)(v * (1 - saturation));
            byte q = (byte)(v * (1 - f * saturation));
            byte t = (byte)(v * (1 - (1 - f) * saturation));

            switch (h)
            {
                case 0: return Color.FromRgb(v, t, p);
                case 1: return Color.FromRgb(q, v, p);
                case 2: return Color.FromRgb(p, v, t);
                case 3: return Color.FromRgb(p, q, v);
                case 4: return Color.FromRgb(t, p, v);
                default: return Color.FromRgb(v, p, q);
            }
        }
        
        private void ColorWheel_MouseMove(object sender, MouseEventArgs e)
        {
            var ColorWheel = sender as Canvas;
            if (ColorSelecting != true) return;
            Point position = default;
            if (ColorWheel == ColorWheel1)
            {
                position = e.GetPosition(ColorWheel1);
            }
            else if (ColorWheel == ColorWheel2)
            {
                position = e.GetPosition(ColorWheel2);
            }
            else if (ColorWheel == ColorWheel3)
            {
                position = e.GetPosition(ColorWheel3);
            }
            else return;
            
            int cx = (int)position.X - wheelRadius;
            int cy = (int)position.Y - wheelRadius;
            double distance = Math.Sqrt(cx * cx + cy * cy);

            if (distance <= wheelRadius)
            {
                double angle = Math.Atan2(cy, cx) * (180 / Math.PI) + 180;
                double saturation = distance / wheelRadius;
                
                Color color = HsvToRgb(angle, saturation, 1.0);
                
                if (ColorWheel == ColorWheel1)
                {
                    SelectedColorDisplay1.Background = new SolidColorBrush(color);
                    Foreground = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                    TextColorWheel1.Text = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                }
                else if (ColorWheel == ColorWheel2)
                {
                    SelectedColorDisplay2.Background = new SolidColorBrush(color);
                    Background = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                    TextColorWheel2.Text = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                }
                else if (ColorWheel == ColorWheel3)
                {
                    SelectedColorDisplay3.Background = new SolidColorBrush(color);
                    Optional = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                    TextColorWheel3.Text = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                }
                else return;
            }
        }
        
        private void ColorWheel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!ColorSelecting)
            {
                ColorSelecting = true;
            }
            else
            {
                ColorSelecting = false;
            }
        }

        private void ColorWheel_MouseLeave(object sender, MouseEventArgs e)
        {
            ColorSelecting = false;
        }

        private void ColorText_KeyDown(object sender, KeyEventArgs e)
        {
            var ColorText = sender as TextBox;
            if (e.Key == Key.Enter)
            {
                if (ColorText == TextColorWheel1)
                {
                   SelectedColorDisplay1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorText.Text)!);
                   Foreground = ColorText.Text;
                }
                else if (ColorText == TextColorWheel2)
                {
                    SelectedColorDisplay2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorText.Text)!);
                    Background = ColorText.Text;
                }
                else if (ColorText == TextColorWheel3)
                {
                    SelectedColorDisplay3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorText.Text)!);
                    Optional = ColorText.Text;
                }
            }
        }
}