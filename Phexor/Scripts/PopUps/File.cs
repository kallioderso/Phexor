using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        var _name = new TextBox { Text = currentTextBlock.Text, Margin = new Thickness(5), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")!) };
        var _rename = new Button { Content = "Rename", Margin = new Thickness(5), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")!) };
        var _open = new Button { Content = "Open", Margin = new Thickness(5), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")!)};
        var _delte = new Button { Content = "Delete", Margin = new Thickness(5), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")!) };

        _rename.Click += (_, _) => RenameFunction(explorer, currentTextBlock, _name.Text);
        _open.Click += (_, _) => OpenFunction(explorer, currentTextBlock);
        _delte.Click += (_, _) => DeleteFunction(explorer, currentTextBlock);
        
        explorer.PopupStackpanel.Children.Add(new TextBlock { Text = "Settings - File", Margin = new Thickness(5), HorizontalAlignment = HorizontalAlignment.Center, Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")!) }); //Create a TextBlock for the settings
        explorer.PopupStackpanel.Children.Add(_name); //Create a TextBox for the file path
        explorer.PopupStackpanel.Children.Add(_rename); //Create a button to rename the file
        explorer.PopupStackpanel.Children.Add(_open); //Create a button to open the file
        explorer.PopupStackpanel.Children.Add(_delte); //Create a button to delete the file
    }

    private static void RenameFunction(Explorer explorer, TextBlock currentTextBlock, string newName)
    {
        PathFunctions.RenameFile(explorer.currentPath(), currentTextBlock.Text, newName);
        CloseAndReload(explorer);
    }

    private static void OpenFunction(Explorer explorer, TextBlock currentTextBlock)
    {
        explorer.OpenFile(currentTextBlock);
        CloseAndReload(explorer);
    }

    private static void DeleteFunction(Explorer explorer, TextBlock currentTextBlock)
    {
        PathFunctions.RemoveFile(explorer.currentPath(), currentTextBlock.Text);
        CloseAndReload(explorer);
    }

    private static void CloseAndReload(Explorer explorer)
    {
        explorer.CloseFieldPopup();
        explorer.InputFieldPath();
    }
}