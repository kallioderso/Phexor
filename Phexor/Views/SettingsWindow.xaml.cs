using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Phexor.Scripts;
using Phexor.ViewModels;


namespace Phexor;
    // C. = Create
    // G. = Generate
    // V. = Variable
    // L. = List
    // M. = Method
    // Xc. = XAML code
    // C#c. = C# code
    // CW. = ColorWheel
public partial class SettingsWindow : Window
{
    private bool ColorSelecting = false; //C. V. for the CW. Colorpicking
    private static string Foreground; //C. V. for Foreground Color
    private static string Background; //C. V. for Background Color
    private static string Special; //C. V. for Special Color
    private int _fields; //C. V. for Field Amount (MainWindow)
    private int _logCount; //C. V. for Log File Amount 
    private SolidColorBrush ForegroundBrush; //C. ColorBrush V.
    private SolidColorBrush BackgroundBrush; //C. ColorBrush V.
    private SolidColorBrush SpecialBrush; //C. ColorBrush V.
    private int wheelRadius = 45; //C. V. for CW. Radius

    public SettingsWindow(SettingsViewModel settingsViewModel)
    {
        Logging.Log("Initialize", "SettingsWindow"); //C. Log Entry
        
        InitializeComponent();

        DataContext = settingsViewModel;
        
        
        // InitializeComponent(); //Initialize Xc. Objects
        // DrawColorWheels(); //C. the CW. in Xc.
        // var borders = new Border[] { Border1, Border2, Border3, Border4, Border5, Border6, Border7, Border8, Border9, Border10, Border11}; //C. L. with Xc. Objects
        // var TextBlocks = new TextBlock[] { TextBlock1, TextBlock2, TextBlock3, TextBlock4, TextBlock5, TextBlock6, TextBlock7}; //C. L. with Xc. Objects
        // GetSettings(); //start M. to get saved Settings
        // Foreground = ForegroundColor; //Set Foreground  from ForegroundColor V. (Settingsfile)
        // Background = BackgroundColor; //Set Background from BackgroundColor V. (Settingsfile)
        // Special = SpecialColor; //Set Special from SpecialColor V. (Settingsfile) 
        // _fields = Fields; //Set fields Amount from Fields V. (Settingsfile)
        // _logCount = LogCount; //Set _logCount Amoung from LogCount V. (Settingsfile)
        // ForegroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ForegroundColor)!); //Set ColorBrush from ForegroundColor V.
        // BackgroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(BackgroundColor)!); //Set ColorBrush from BackgroundColor V.
        // SpecialBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(SpecialColor)!); //Set ColorBrush from SpecialColor V.
        // TextColorWheel1.Text = ForegroundColor; //Set CW. 1 Text to ForegroundColor V.
        // TextColorWheel2.Text = BackgroundColor; //Set CW. 2 Text to ForegroundColor V.
        // TextColorWheel3.Text = SpecialColor; //Set CW. 3 Text to ForegroundColor V.
        // FelderInput.Value = _fields; //Set Field Slider Value from Fields Amount
        // LogCountInput.Value = _logCount; //Set LogCountInput Slider Value from _logCount Amount
        // SelectedColorDisplay1.Background = ForegroundBrush; //Set the Color for the SelectedColorDisplay 1 to ForegroundBrush 
        // SelectedColorDisplay2.Background = BackgroundBrush; //Set the Color for the SelectedColorDisplay 2 to BackgroundBrush
        // SelectedColorDisplay3.Background = SpecialBrush; //Set the Color for the SelectedColorDisplay 3 to SpecialBrush
        //
        // foreach (var border in borders) //loop for every Border in Xc.
        // {
        //     border.BorderBrush = BackgroundBrush; //change Borders Borderbrush color to BackgroundBrush
        // }
        //
        // foreach (var Textblock in TextBlocks) //loop for every Textblock in Xc.
        // {
        //     Textblock.Foreground = ForegroundBrush; //change Textblocks Font color to ForegroundBrush
        // }
    }

    // private void Settings_OnClosing(object? sender, CancelEventArgs e) //M. for Closingevent
    // {
    //     Logging.Log("Close Started", "SettingsWindow"); //C. Log Entry
    //     MainWindow mainWindow = new MainWindow(); //C. new Mainwindow window
    //     mainWindow.Show(); // Show the new Mainwindow window
    // }
    //
    // private void Save(object sender, MouseButtonEventArgs mouseButtonEventArgs) //M. for Saving the Settings and Close this window
    // {
    //     Logging.Log("Saving Settings", "SettingsWindow"); //C. Log Entry
    //     SetSettings(Foreground, Background, Special, FelderInput.Value, LogCountInput.Value); //Start Settings Save M.
    //     this.Close(); //Start Closing Prozess
    // }
    //
    // private void DrawColorWheels() //Create the CW. Xc. Objects !!!(Parts of this Code got Created with the help of ChatGPT)!!!
    // {
    //     Logging.Log("Create ColorWheels", "SettingsWindow"); //C. Log Entry
    //     int size = wheelRadius * 2; //C. V. for the diameter
    //     WriteableBitmap bitmap = new WriteableBitmap(size, size, 96, 96, PixelFormats.Bgra32, null); //C. bitmap for the CW.
    //     byte[] pixels = new byte[size * size * 4]; //C. V. pixels for the CW.
    //
    //     for (int y = 0; y < size; y++) //Loops for the y < diameter 
    //     {
    //         for (int x = 0; x < size; x++) //Loops for the y > diameter 
    //         {
    //             int cx = x - wheelRadius; //C. V. for CX
    //             int cy = y - wheelRadius; //C. V. for CY
    //             double distance = Math.Sqrt(cx * cx + cy * cy); //Some Maths that got Generated by Chatgpt
    //
    //             if (distance <= wheelRadius) //check if CW. distance is lower than the WheelRadius
    //             {
    //                 double angle = Math.Atan2(cy, cx) * (180 / Math.PI) + 180; //Some Maths that got Generated by Chatgpt
    //                 double saturation = distance / wheelRadius; //Some Maths that got Generated by Chatgpt
    //
    //                 Color color = HsvToRgb(angle, saturation, 1.0); //Some Maths that got Generated by Chatgpt
    //
    //                 int index = (y * size + x) * 4; //Some Maths that got Generated by Chatgpt
    //                 pixels[index + 0] = color.B; //Some Maths that got Generated by Chatgpt
    //                 pixels[index + 1] = color.G; //Some Maths that got Generated by Chatgpt
    //                 pixels[index + 2] = color.R; //Some Maths that got Generated by Chatgpt
    //                 pixels[index + 3] = 255; //Some Maths that got Generated by Chatgpt
    //             }
    //         }
    //     }
    //
    //     bitmap.WritePixels(new Int32Rect(0, 0, size, size), pixels, size * 4, 0); //set Bitmap for the CW.
    //
    //     ImageBrush brush = new ImageBrush(); //C. V. brush
    //     brush.ImageSource = bitmap; //set Imagesource of Brush to Bitmap
    //     ColorWheel1.Background = brush; //Brush ColorWheel 1
    //     ColorWheel2.Background = brush; //Brush ColorWheel 2
    //     ColorWheel3.Background = brush; //Brush ColoWheel 3
    // }
    //
    // private Color HsvToRgb(double hue, double saturation, double value) //M. to set the Colors of the CW. Objects !!!(this Code got Created by ChatGPT)!!!
    // {
    //     int h = (int)(hue / 60) % 6;  //Some Maths that got Generated by Chatgpt
    //     double f = hue / 60 - Math.Floor(hue / 60); //Some Maths that got Generated by Chatgpt
    //     byte v = (byte)(value * 255); //Some Maths that got Generated by Chatgpt
    //     byte p = (byte)(v * (1 - saturation)); //Some Maths that got Generated by Chatgpt
    //     byte q = (byte)(v * (1 - f * saturation)); //Some Maths that got Generated by Chatgpt
    //     byte t = (byte)(v * (1 - (1 - f) * saturation)); //Some Maths that got Generated by Chatgpt
    //
    //     switch (h)
    //     {
    //         case 0: return Color.FromRgb(v, t, p); //Some Maths that got Generated by Chatgpt
    //         case 1: return Color.FromRgb(q, v, p); //Some Maths that got Generated by Chatgpt
    //         case 2: return Color.FromRgb(p, v, t); //Some Maths that got Generated by Chatgpt
    //         case 3: return Color.FromRgb(p, q, v); //Some Maths that got Generated by Chatgpt
    //         case 4: return Color.FromRgb(t, p, v); //Some Maths that got Generated by Chatgpt
    //         default: return Color.FromRgb(v, p, q); //Some Maths that got Generated by Chatgpt
    //     }
    // }
    //
    // private void ColorWheel_MouseMove(object sender, MouseEventArgs e) //M. to react of the MouseMoveEvents inside the CW. !!!(Parts of this Code got Created with the help of ChatGPT)!!!
    // {
    //     var ColorWheel = sender as Canvas; //C. V. for the Canvas of the CW.
    //     if (ColorSelecting != true) return; //check for Colorselecting is false
    //     Point position = default; //C. V. Point and set it to default
    //     if (ColorWheel == ColorWheel1) //if CW. is CW. 1
    //     {
    //         position = e.GetPosition(ColorWheel1); //Localisize Mouse Position on CW.
    //     }
    //     else if (ColorWheel == ColorWheel2) //if CW. is CW. 2
    //     {
    //         position = e.GetPosition(ColorWheel2); //Localisize Mouse Position on CW.
    //     }
    //     else if (ColorWheel == ColorWheel3) //if CW. is CW. 3
    //     {
    //         position = e.GetPosition(ColorWheel3); //Localisize Mouse Position on CW.
    //     }
    //     else return;
    //
    //     int cx = (int)position.X - wheelRadius; //Some Maths that got Generated by Chatgpt
    //     int cy = (int)position.Y - wheelRadius; //Some Maths that got Generated by Chatgpt
    //     double distance = Math.Sqrt(cx * cx + cy * cy); //Some Maths that got Generated by Chatgpt
    //
    //     if (distance <= wheelRadius) //Some Maths that got Generated by Chatgpt
    //     {
    //         double angle = Math.Atan2(cy, cx) * (180 / Math.PI) + 180; //Some Maths that got Generated by Chatgpt
    //         double saturation = distance / wheelRadius; //Some Maths that got Generated by Chatgpt
    //
    //         Color color = HsvToRgb(angle, saturation, 1.0); //Some Maths that got Generated by Chatgpt
    //
    //         if (ColorWheel == ColorWheel1) //if CW. is CW. 1
    //         {
    //             SelectedColorDisplay1.Background = new SolidColorBrush(color); //Some Maths that got Generated by Chatgpt
    //             Foreground = $"#{color.R:X2}{color.G:X2}{color.B:X2}";  //Some Maths that got Generated by Chatgpt
    //             TextColorWheel1.Text = $"#{color.R:X2}{color.G:X2}{color.B:X2}"; //Some Maths that got Generated by Chatgpt
    //         }
    //         else if (ColorWheel == ColorWheel2) //if CW. is CW. 2
    //         {
    //             SelectedColorDisplay2.Background = new SolidColorBrush(color); //Some Maths that got Generated by Chatgpt
    //             Background = $"#{color.R:X2}{color.G:X2}{color.B:X2}"; //Some Maths that got Generated by Chatgpt
    //             TextColorWheel2.Text = $"#{color.R:X2}{color.G:X2}{color.B:X2}"; //Some Maths that got Generated by Chatgpt
    //         }
    //         else if (ColorWheel == ColorWheel3) //if CW. is CW. 3
    //         {
    //             SelectedColorDisplay3.Background = new SolidColorBrush(color); //Some Maths that got Generated by Chatgpt
    //             Special = $"#{color.R:X2}{color.G:X2}{color.B:X2}"; //Some Maths that got Generated by Chatgpt
    //             TextColorWheel3.Text = $"#{color.R:X2}{color.G:X2}{color.B:X2}"; //Some Maths that got Generated by Chatgpt
    //         }
    //         else return;
    //     }
    // }
    //
    // private void ColorWheel_MouseDown(object sender, MouseButtonEventArgs e) //M. to React on Click on CW.
    // {
    //     if (!ColorSelecting) //checks if Colorselecting is False
    //     {
    //         ColorSelecting = true; //Sets Colorselecting to True
    //         Logging.Log("Start ColorWheel Color Selecting", "SettingsWindow"); //C. Log Entry
    //     }
    //     else //checks if Colorselecting is True
    //     {
    //         ColorSelecting = false; //Sets Colorselecting to False
    //         Logging.Log("End ColorWheel Color Selecting", "SettingsWindow"); //C. Log Entry
    //     }
    // }
    //
    // private void ColorWheel_MouseLeave(object sender, MouseEventArgs e) //M. to react on Mouse Leave CW.
    // {
    //     ColorSelecting = false; //set ColorSelecting bool to false
    // }
    //
    // private void ColorText_KeyDown(object sender, KeyEventArgs e) //M. to Enter the Written Color code
    // {
    //     try //prevent Crashes
    //     {
    //         var ColorText = sender as TextBox; //Set an Referenz to the Xc. Textbox
    //         if (e.Key == Key.Enter) //check if the Entered Key is Enter
    //         {
    //             Logging.Log("Enter Color per TextBox", "SettingsWindow"); //C. Log Entry
    //             if (ColorText == TextColorWheel1) //checks if Referenzed Texbox is from CW. 1
    //             {
    //                 SelectedColorDisplay1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorText.Text)!); //Changes the Background Brush for the SelectedColorDisplay 1
    //                 Foreground = ColorText.Text; //changes the Foreground
    //             }
    //             else if (ColorText == TextColorWheel2) //checks if Referenzed Texbox is from CW. 2
    //             {
    //                 SelectedColorDisplay2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorText.Text)!); //Changes the Background Brush for the SelectedColorDisplay 2
    //                 Background = ColorText.Text; //changes the Background
    //             }
    //             else if (ColorText == TextColorWheel3) //checks if Referenzed Texbox is from CW. 3
    //             {
    //                 SelectedColorDisplay3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorText.Text)!); //Changes the Background Brush for the SelectedColorDisplay 3
    //                 Special = ColorText.Text; //changes the Special Color
    //             }
    //         }
    //     }
    //     catch (Exception exception) //Used for Logs
    //     {
    //         Logging.CatchLog(Convert.ToString(e), "SettingsWindow"); //use CatchLog M.
    //     }
    // }
    //
    // private void SetOriginalDesign(object sender, MouseButtonEventArgs e) //M. to reset saved Design
    // {
    //     Logging.Log("Set Colors To Original", "SettingsWindow"); //C. Log Entry
    //     SelectedColorDisplay1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFF8DC")!); //Set Color of SelectedColorDisplay 1
    //     Foreground = "#FFFFF8DC"; //Change Foreground
    //     TextColorWheel1.Text = "#FFFFF8DC"; //Change CW. 1 Text
    //     
    //     SelectedColorDisplay2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF0FFFF")!); //Set Color of SelectedColorDisplay 2
    //     Background = "#FFF0FFFF"; //Change Background
    //     TextColorWheel2.Text = "#FFF0FFFF"; //Change CW. 2 Text
    //     
    //     SelectedColorDisplay3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE6E6FA")!); //Set Color of SelectedColorDisplay 3
    //     Special = "#FFE6E6FA"; //Change Special Color
    //     TextColorWheel3.Text = "#FFE6E6FA"; //Change CW. 3 Text
    // }
}