using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Phexor.Scripts;

namespace Phexor;

public partial class Tutorial : Window
{
    private List<TextBlock> _verzeichnisListe = new List<TextBlock>();
    private List<TextBlock> _fileListe = new List<TextBlock>();
    private int _ordnerUndDateienAnzahl = Settingsfile.Fields;
    private int Counter = 0;
    private int TutorialNumber = 0;
    private SolidColorBrush Foreground;
    private SolidColorBrush Background;
    private SolidColorBrush Optional;
    public Tutorial()
    {
        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settingsfile.ForegroundColor)!);
        Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settingsfile.BackgroundColor));
        Optional = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settingsfile.OptionalColor));
        this.Icon = new BitmapImage(new Uri("pack://application:,,,/Grafiks/Icon.ico"));
        InitializeComponent();
        TextBorder.Background = Background;
        TextBorder.BorderBrush = Foreground;
        PfadInput.Foreground = Foreground;
        Border5.BorderBrush = Background;
        Introduction.Text = "Füge zuerst einmahl deinen Pfad ein";
        Introduction.Foreground = Optional;
        for (int i = 0; i < Settingsfile.Fields; i++)
        {
            TextBlock order = new TextBlock();
            order.Name = $"Ordner{i}";
            order.Foreground = Foreground;
            order.FontSize = 10;
            order.Text = "";
            order.Height = (Verzeichnise.Height/_ordnerUndDateienAnzahl);
            order.Width = (Verzeichnise.Width);
            order.MouseLeftButtonDown += Field_Click;
            // order.MouseRightButtonDown += Field_Setting;
            order.VerticalAlignment = VerticalAlignment.Top; 
            _verzeichnisListe.Add(order);
            Verzeichnise.Children.Add(order);
                
            TextBlock Datei = new TextBlock();
            Datei.Name = $"Datei{i}";
            Datei.Foreground = Foreground;
            Datei.FontSize = 10;
            Datei.Text = "";
            Datei.Height = (Dateien.Height/_ordnerUndDateienAnzahl);
            Datei.Width = (Dateien.Width);
            Datei.MouseLeftButtonDown += OpenFile;
            // Datei.MouseRightButtonDown += File_RightClick;
            Datei.VerticalAlignment = VerticalAlignment.Top; 
            _fileListe.Add(Datei);
            Dateien.Children.Add(Datei);
        }
    }

    private void Tutorial_OnClosed(object sender, EventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
    }

    private void PfadInput_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            if (Directory.Exists(PfadInput.Text))
            {
                PfadInput.Foreground = Brushes.Gray;
                Border5.BorderBrush = Brushes.Gray;
                Border8.BorderBrush = Background;
                Introduction.Text = "Klicke nun auf eines Der verzeichnise";
                foreach (var Verzeichnis in _verzeichnisListe)
                {
                    Verzeichnis.Text = "Verzeichnis" + Counter;
                    Counter++;
                }

                Counter = 0;
                TutorialNumber++;
            }
        }
    }

    private void Field_Click(object sender, MouseButtonEventArgs e)
    {
        if (TutorialNumber == 1)
        {
            TutorialNumber++;
            foreach (var Verzeichnis in _verzeichnisListe)
            {
                Verzeichnis.Foreground = Brushes.Gray;
            }
            Border8.BorderBrush = Brushes.Transparent;
            Border9.BorderBrush = Background;
            Introduction.Text = "Klicke nun auf eine der Dateien";
            foreach (var File in _fileListe)
            {
                File.Text = "Datei" + Counter;
                Counter++;
            }
        }
    }

    private void OpenFile(object sender, MouseButtonEventArgs e)
    {
        if (TutorialNumber == 2)
        {
            TutorialNumber++;
            foreach (var File in _fileListe)
            {
                File.Foreground = Brushes.Gray;
            }
            Border9.BorderBrush = Brushes.Gray;
            Introduction.Text = "Klicke nun auf den Pfeil nach unten";
            Border9.BorderBrush = Brushes.Gray;
            PageDownButton.Background = Background;
        }
    }

    private void RemoveStandardText(object sender, MouseEventArgs e)
    {
        if (PfadInput.Text == "" || PfadInput.Text == "Füge hier deinen Pfad ein")
        {
            PfadInput.Text = String.Empty;
        }
    }

    private async void ClickPageDown(object sender, MouseButtonEventArgs e)
    {
        if (TutorialNumber == 3)
        {
            TutorialNumber++;
            PageDownButton.Background = Brushes.Gray;
            Introduction.Text = "Nun sind wir auf der Zweiten Seite";
            PageButton.Background = Background;
            PageCounter.Text = "2";
            foreach (var File in _fileListe)
            {
                File.Text = "Datei" + (Counter + _ordnerUndDateienAnzahl);
                Counter++;
            }
            Counter = 0;
            
            foreach (var Verzeichnis in _verzeichnisListe)
            {
                Verzeichnis.Text = "Verzeichnis" + (Counter + _ordnerUndDateienAnzahl);
                Counter++;
            }
            Counter = 0;
            
            await Task.Delay(5000);
            
            PageButton.Background = Brushes.Gray;
            PageUpButton.Background = Background;
            Introduction.Text = "Klicke nun auf den Pfeil nach Oben";
            foreach (var File in _fileListe)
            {
                File.Text = "Datei" + Counter;
                Counter++;
            }
            Counter = 0;
            
            foreach (var Verzeichnis in _verzeichnisListe)
            {
                Verzeichnis.Text = "Verzeichnis" + Counter;
                Counter++;
            }
            Counter = 0;
        }
    }

    private async void ClickPageUp(object sender, MouseButtonEventArgs e)
    {
        if (TutorialNumber == 4)
        {
            TutorialNumber++;
            PageUpButton.Background = Brushes.Gray;
            PageCounter.Text = "1";
            PageButton.Background = Background;
            Introduction.Text = "Nun sind wir auf der ersten Seite";
            await Task.Delay(5000);
            PageButton.Background = Brushes.Gray;
            Introduction.Text = "Weitere Einstellungen findest du Dort";
            SettingButton.Background = Background;
            await Task.Delay(4000);
            this.Close();
        }
    }
}