using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Phexor.Scripts.PopUps;

public class Directory(Explorer explorer, TextBlock currentTextBlock)
{
    private readonly TextBlock _currentTextBlock = currentTextBlock;
    public void PopUp()
    {
        CreatePopUp();
    }

    public string GetDirectory()
    {
        return _currentTextBlock.Text.ToString();
    }
    
    private void CreatePopUp()
    {
        explorer.PopupStackpanel.Children.Add(new TextBlock
        {
            Text = "Settings",
            Height = 30,
            Width = 100,
            TextAlignment = TextAlignment.Center,
            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")!),
            FontSize = 14,
            VerticalAlignment = VerticalAlignment.Center
        });

        explorer.PopupStackpanel.Children.Add(new Button
        {
            Content = "Open",
            Height = 30,
            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333")!),
            FontSize = 10,
            Margin = new Thickness(1),
            VerticalAlignment = VerticalAlignment.Center
        });
    }
}