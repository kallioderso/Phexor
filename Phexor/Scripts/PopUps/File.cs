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
        explorer.PopupStackpanel.Children.Add(new TextBlock { Text = "Settings - File", Margin = new Thickness(5), HorizontalAlignment = HorizontalAlignment.Center, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")!) }); //Create a TextBlock for the settings
        explorer.PopupStackpanel.Children.Add(new TextBox { Text = currentTextBlock.Text, Margin = new Thickness(5), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")!) }); //Create a TextBox for the file path
        explorer.PopupStackpanel.Children.Add(new Button { Content = "Rename", Margin = new Thickness(5), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")!) }); //Create a button to rename the file
        explorer.PopupStackpanel.Children.Add(new Button { Content = "Open", Margin = new Thickness(5), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")!) }); //Create a button to open the file
        explorer.PopupStackpanel.Children.Add(new Button { Content = "Delete", Margin = new Thickness(5), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")!) }); //Create a button to delete the file
    }
}