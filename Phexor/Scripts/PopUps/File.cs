using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Phexor.Scripts.PopUps;
// C. = Create
// G. = Generate
// V. = Variable
// L. = List
// M. = Method
// Xc. = XAML code
// C#c. = C# code

public static class File
{
    //-----Public Methods-----\\
    public static void PopUp(Explorer explorer, TextBlock currentTextBlock) => CreatePopUp(explorer, currentTextBlock); //Public method to create the pop-up
    
    //-----Private Methods-----\\
    private static void CreatePopUp(Explorer explorer, TextBlock currentTextBlock) //Private method to create the pop-up
    {
        explorer.PopupStackpanel.Children.Add(new TextBlock { Text = "Settings", Height = 30, Width = 100, TextAlignment = TextAlignment.Center, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")!), FontSize = 14, VerticalAlignment = VerticalAlignment.Center }); //Create a TextBlock for the settings
        explorer.PopupStackpanel.Children.Add(new Button { Content = "Open", Height = 30, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")!), FontSize = 10, Margin = new Thickness(1), VerticalAlignment = VerticalAlignment.Center }); //Create a button to open the file
    }
}