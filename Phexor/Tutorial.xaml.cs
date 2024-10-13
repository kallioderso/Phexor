using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Phexor;

public partial class Tutorial : Window
{
    public Tutorial()
    {
        this.Icon = new BitmapImage(new Uri("pack://application:,,,/Grafiks/Icon.ico"));
        InitializeComponent();
    }

    private void Tutorial_OnClosed(object sender, EventArgs e)
    {
        App.Current.MainWindow.Show();
    }

    private void PfadInput_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            if (Directory.Exists(PfadInput.Text))
            {
                this.Close();
            }
        }

    }

    private void RemoveStandardText(object sender, MouseEventArgs e)
    {
        if (PfadInput.Text == "" || PfadInput.Text == "Füge hier deinen Pfad ein (Drücke dann Enter)")
        {
            PfadInput.Text = String.Empty;
        }
    }
}