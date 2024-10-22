using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Net.Mime;
using System.Security.Principal;
using System.Windows.Media.Imaging;
using Phexor.Scripts;
using static System.Windows.Input.Key;
using Path = System.IO.Path;
using SolidColorBrush = System.Windows.Media.SolidColorBrush;

namespace Phexor
{
    public partial class MainWindow
    {
        private bool NotTrueClose = false;
        private int _ordnerUndDateienAnzahl;
        private SolidColorBrush _foregroundBrush;
        private SolidColorBrush _backgroundBrush;
        private SolidColorBrush _optionalBrush;
        private List<TextBlock> _verzeichnisListe = new List<TextBlock>();
        private List<TextBlock> _fileListe = new List<TextBlock>();
        private int Page = 1;
        private int WhichPage = 0;
        private string Pfad = "";
        private string LastPath;

        public MainWindow()
        {
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/Grafiks/Icon.ico"));
            InitializeComponent();
            var buttonsBorder = new Border[] { PageButton, PageUpButton, PageDownButton, SettingButton};
            var borders = new Border[] { Border1, Border2, Border3, Border4};
            var textBlocks = new TextBlock[] { TextBlock1, TextBlock2};
            CheckForSettings();
            if (!Settingsfile.ScrollRad)
            {
                Dateien2.Visibility = Visibility.Hidden;
                Verzeichnise2.Visibility = Visibility.Hidden;
                Dateien.Visibility = Visibility.Visible;
                Verzeichnise.Visibility = Visibility.Visible;
                _ordnerUndDateienAnzahl = Settingsfile.Fields;
                PageUpButton.Visibility = Visibility.Visible;
                PageDownButton.Visibility = Visibility.Visible;
                PageButton.Visibility = Visibility.Visible;
            }
            else
            {
                Dateien.Visibility = Visibility.Hidden;
                Verzeichnise.Visibility = Visibility.Hidden;
                Dateien2.Visibility = Visibility.Visible;
                Verzeichnise2.Visibility = Visibility.Visible;
                _ordnerUndDateienAnzahl = Settingsfile.Fields * 4;
                PageUpButton.Visibility = Visibility.Hidden;
                PageDownButton.Visibility = Visibility.Hidden;
                PageButton.Visibility = Visibility.Hidden;
            }
            _foregroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settingsfile.ForegroundColor)!);
            _backgroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settingsfile.BackgroundColor)!);
            _optionalBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settingsfile.OptionalColor)!);
            
            try
            {
                foreach (var border2 in buttonsBorder)
                {
                    border2.Background = _foregroundBrush;
                }

                foreach (var border in borders)
                {
                    border.BorderBrush = _backgroundBrush;
                }

                foreach (var textBlock in textBlocks)
                {
                    textBlock.Foreground = _foregroundBrush;
                }

                Border5.Background = _backgroundBrush;
                PfadInput.Foreground = _foregroundBrush;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            

            for (int i = 0; i < _ordnerUndDateienAnzahl; i++)
            {
                TextBlock order = new TextBlock();
                if (Settingsfile.ScrollRad)
                {
                    order.Height = 20;
                }
                else
                {
                    order.Height = (Verzeichnise.Height/_ordnerUndDateienAnzahl);
                    order.Width = (Verzeichnise.Width);
                }
                order.Name = $"Ordner{i}";
                order.Foreground = _foregroundBrush;
                order.FontSize = 10;
                order.Text = "";
                order.MouseLeftButtonDown += Field_Click;
                order.MouseRightButtonDown += Field_Setting;
                order.VerticalAlignment = VerticalAlignment.Top; 
                _verzeichnisListe.Add(order);
                if (!Settingsfile.ScrollRad)
                {
                    Verzeichnise.Children.Add(order);
                }
                else
                {
                    Verzeichnise2.Items.Add(order);
                }
                
                TextBlock Datei = new TextBlock();
                if (Settingsfile.ScrollRad)
                {
                    Datei.Height = 20;
                }
                else
                {
                    Datei.Height = (Dateien.Height/_ordnerUndDateienAnzahl);
                    Datei.Width = (Dateien.Width);
                }
                Datei.Name = $"Datei{i}";
                Datei.Foreground = _foregroundBrush;
                Datei.FontSize = 10;
                Datei.Text = "";
                Datei.MouseLeftButtonDown += OpenFile;
                Datei.MouseRightButtonDown += File_RightClick;
                Datei.VerticalAlignment = VerticalAlignment.Top; 
                _fileListe.Add(Datei);
                if (!Settingsfile.ScrollRad)
                {
                    Dateien.Children.Add(Datei);
                }
                else
                {
                    Dateien2.Items.Add(Datei);
                }
            }
        }
        

        private void Field_Click(object sender, RoutedEventArgs e)
        {
            var textBlock = sender as System.Windows.Controls.TextBlock;
            if (textBlock.Text == "" || textBlock.Text == null)
            {
                return;
            }
            else
            {
                if (textBlock.Text != LastPath)
                {
                    Console.WriteLine(Path.GetDirectoryName(Pfad));
                    Page = 1;
                    WhichPage = 0;
                    PageCounter.Text = Convert.ToString(Page);
                    PfadInput.Text = Pfad + textBlock.Text;
                    LastPath = textBlock.Text;
                    LoadAllFolder(Pfad + textBlock.Text);
                }
                else
                {
                    return;
                }
            }
        }

        private void LastPage(object sender, RoutedEventArgs e)
        {
            if (Page != 1)
            {
                Page--;
                WhichPage -= _ordnerUndDateienAnzahl;
                PageCounter.Text = Convert.ToString(Page);
                Pfad = PfadInput.Text;
                LoadAllFolder(Pfad);
            }
            else
            {
                return;
            }

        }

        private void NextPage(object sender, RoutedEventArgs e)
        {
            Page++;
            WhichPage += _ordnerUndDateienAnzahl;
            PageCounter.Text = Convert.ToString(Page);
            Pfad = PfadInput.Text;
            LoadAllFolder(Pfad);
        }

        private void Border_MouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            var border = sender as Border;
            if (border != null && border != PageButton)
            {
                border.Background = _optionalBrush;
                border.Height = 32;
                border.Width = 32;
            }
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            var border = sender as System.Windows.Controls.Border;
            if (border != null && border != PageButton)
            {
                border.Background = _foregroundBrush;
                border.Height = 30;
                border.Width = 30;
            }
        }

        private void LoadAllFolder(string MeinPfad)
        {
            string directoryPath = MeinPfad + @"\";
            Pfad = directoryPath;
            int directoryPathLength = directoryPath.Length;

            var directoryCount = 0;
            var DirectorysDisplayed = 0;
            var DirectorysNotDisplayed = 0;
            var fileCount = 0;
            var FilesDisplayed = 0;
            var FilesNotDisplayed = 0;
            try
            {
                var directors = Directory.GetDirectories(directoryPath);
                if (directors != null)
                {
                    try
                    {
                        for (int i = 0; i < _ordnerUndDateienAnzahl; i++)
                        {
                            if (directoryCount < DirectorysNotDisplayed + WhichPage)
                            {
                                _verzeichnisListe[DirectorysNotDisplayed].Text = null;
                            }

                            DirectorysNotDisplayed++;
                        }

                        DirectorysNotDisplayed = 0;

                        foreach (var directory in directors)
                        {
                            for (int i = 0; i < _ordnerUndDateienAnzahl; i++)
                            {
                                if (directoryCount < DirectorysNotDisplayed + WhichPage)
                                {
                                    _verzeichnisListe[DirectorysNotDisplayed].Text = null;
                                }

                                DirectorysNotDisplayed++;
                            }

                            DirectorysNotDisplayed = 0;

                            if (directoryCount == DirectorysDisplayed + WhichPage)
                            {
                                _verzeichnisListe[DirectorysDisplayed].Text = directory.Substring(directoryPathLength);
                                DirectorysDisplayed++;
                            }

                            directoryCount++;
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
                else
                {
                    for (int i = 0; i < _ordnerUndDateienAnzahl; i++)
                    {
                        _verzeichnisListe[i].Text = null;
                    }
                }



                var files = Directory.GetFiles(directoryPath);
                if (files != null)
                {
                    try
                    {
                        for (int i = 0; i < _ordnerUndDateienAnzahl; i++)
                        {
                            if (fileCount < FilesNotDisplayed + WhichPage)
                            {
                                _fileListe[FilesNotDisplayed].Text = null;
                            }

                            FilesNotDisplayed++;
                        }

                        FilesNotDisplayed = 0;

                        foreach (var file in files)
                        {
                            for (int i = 0; i < _ordnerUndDateienAnzahl; i++)
                            {
                                if (fileCount < FilesNotDisplayed + WhichPage)
                                {
                                    _fileListe[FilesNotDisplayed].Text = null;
                                }

                                FilesNotDisplayed++;
                            }

                            FilesNotDisplayed = 0;

                            if (fileCount == FilesDisplayed + WhichPage)
                            {
                                _fileListe[FilesDisplayed].Text = file.Substring(directoryPathLength);
                                FilesDisplayed++;
                            }

                            fileCount++;
                        }
                    }
                    catch (Exception e)
                    {
                        FilesNotDisplayed = 0;
                        for (int i = 0; i < _ordnerUndDateienAnzahl; i++)
                        {
                            if (i < FilesNotDisplayed)
                            {
                                _fileListe[FilesNotDisplayed].Text = null;
                            }

                            FilesNotDisplayed++;
                        }

                        FilesNotDisplayed = 0;
                    }
                }
                else
                {
                    for (int i = 0; i < _ordnerUndDateienAnzahl; i++)
                    {
                        _fileListe[i].Text = null;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                for (int i = 0; i < _ordnerUndDateienAnzahl; i++)
                {
                    _fileListe[i].Text = null;
                    _verzeichnisListe[i].Text = null;
                }
            }
        }
        


        private void PfadInput_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Pfad = PfadInput.Text;
                LoadAllFolder(Pfad);
                Page = 1;
                WhichPage = 0;
                PageCounter.Text = Convert.ToString(Page);
            }
        }

        private void Field_Setting(object sender, MouseButtonEventArgs e)
        {

        }

        private void File_RightClick(object sender, MouseButtonEventArgs e)
        {

        }


        private void OpenFile(object sender, MouseButtonEventArgs e)
        {
            var filePath = sender as TextBlock;

            try
            {
                if (filePath != null && (filePath.Text != null && filePath.Text != ""))
                {
                    if (filePath.Text != null) Process.Start(Pfad + @"\" + filePath.Text);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
        
        private void SettingsWindow(object sender, MouseButtonEventArgs e)
        {
            Phexor.SettingsWindow settings = new SettingsWindow();
            settings.Show();
            NotTrueClose = true;
            this.Close();
        }

        private void RemoveStandardText(object sender, MouseEventArgs mouseEventArgs)
        {
            if (Pfad == "")
            {
                PfadInput.Text = String.Empty;
            }
        }

        private void CheckForSettings()
        {
            if (!File.Exists(Settingsfile.SettingsDatei))
            {
                Settingsfile.SetSettings("#FFFFF8DC", "#FFF0FFFF", "#FFE6E6FA", 20, false);
                Settingsfile.GetSettings();
                this.Hide();
                Phexor.Tutorial tutorial = new Phexor.Tutorial();
                tutorial.Show();
            }
            else
            {
                Settingsfile.GetSettings();
            }
        }

        private void OnClose(object sender, EventArgs e)
        {
            
            if (!NotTrueClose)
            {
                var phexorProcesses = Process.GetProcessesByName("Phexor");
                foreach (var process in phexorProcesses)
                {
                    try
                    {
                        process.Kill();
                        process.WaitForExit();
                    }
                    catch (Exception)
                    {
                    }
                } 
            }
            else
            {
                NotTrueClose = false;
            }
        }
    }
}
