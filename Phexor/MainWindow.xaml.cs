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
        private int Fields;
        private SolidColorBrush _foregroundBrush;
        private SolidColorBrush _backgroundBrush;
        private SolidColorBrush _optionalBrush;
        private List<TextBlock> _verzeichnisListe = new List<TextBlock>();
        private List<TextBlock> _fileListe = new List<TextBlock>();
        private string Pfad = "";
        private string LastPath;
        private int FileCount = 0;
        private int FolderCount = 0;

        public MainWindow()
        {
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/Grafiks/Icon.ico"));
            InitializeComponent();
            var borders = new Border[] { Border1, Border2, Border3, Border4};
            var textBlocks = new TextBlock[] { TextBlock1, TextBlock2};
            CheckForSettings();
            _foregroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settingsfile.ForegroundColor)!);
            _backgroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settingsfile.BackgroundColor)!);
            _optionalBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settingsfile.OptionalColor)!);
            Fields = Settingsfile.Fields;
            FileCount = Fields;
            FolderCount = Fields;
            
            try
            {
                SettingButton.Background = _foregroundBrush;

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
            

            for (int i = 0; i < Fields; i++) // Generating Folder & file Fields
            {
                TextBlock order = new TextBlock();
                order.Height = (Verzeichnise.Height/Settingsfile.Fields);
                order.Width = (Verzeichnise.Width);
                order.Name = $"Ordner{i}";
                order.Foreground = _foregroundBrush;
                order.FontSize = 10;
                order.Text = "";
                order.MouseLeftButtonDown += Folder_Click;
                order.MouseRightButtonDown += Field_Setting;
                order.VerticalAlignment = VerticalAlignment.Top; 
                _verzeichnisListe.Add(order);
                Verzeichnise.Children.Add(order);
                
                TextBlock Datei = new TextBlock();
                Datei.Height = (Dateien.Height/Settingsfile.Fields);
                Datei.Width = (Dateien.Width);
                Datei.Name = $"Datei{i}";
                Datei.Foreground = _foregroundBrush;
                Datei.FontSize = 10;
                Datei.Text = "";
                Datei.MouseLeftButtonDown += OpenFile;
                Datei.MouseRightButtonDown += File_RightClick;
                Datei.VerticalAlignment = VerticalAlignment.Top; 
                _fileListe.Add(Datei);
                Dateien.Children.Add(Datei);
            }
        }
        
        private void CheckForSettings() // Search/Set Settings
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
        
        private void RemoveStandardText(object sender, MouseEventArgs mouseEventArgs) // Remove Standard Text (Input Field)
        {
            if (Pfad == "")
            {
                PfadInput.Text = String.Empty;
            }
        }
        
        private void PfadInput_OnKeyDown(object sender, KeyEventArgs e) //Start Exploring
        {
            if (e.Key == Key.Enter)
            {
                Pfad = PfadInput.Text;
                LoadAllFields(Pfad);
            }
        }
        
        private void DirectoryScrollingWithMouse(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (Directory.Exists(PfadInput.Text))
                {
                    FolderCount++;
                    LoadAllFields(PfadInput.Text);
                }
                else
                {
                }
            }
            else if (e.Delta > 0)
            {
                if (FolderCount != Settingsfile.Fields)
                {
                    FolderCount--;
                    LoadAllFields(PfadInput.Text);
                    Console.WriteLine(FolderCount);
                }
                else
                {
                }
            }
        }

        private void FileScrollingWithMouse(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (Directory.Exists(PfadInput.Text))
                {
                    FileCount++;
                    LoadAllFields(PfadInput.Text);
                }
                else
                {
                }
            }
            else if (e.Delta > 0)
            {
                if (FileCount != Settingsfile.Fields)
                {
                    FileCount--;
                    LoadAllFields(PfadInput.Text);
                    Console.WriteLine(FileCount);
                }
                else
                {
                }
            }
        }
        
        private void Folder_Click(object sender, RoutedEventArgs e) //Click Folder
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
                    PfadInput.Text = Pfad + textBlock.Text;
                    LastPath = textBlock.Text;
                    FolderCount = Fields;
                    FileCount = Fields;
                    LoadAllFields(Pfad + textBlock.Text);
                }
                else
                {
                    return;
                }
            }
        }
        
        private void OpenFile(object sender, MouseButtonEventArgs e) //Open Clicked File
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

        private void Border_MouseEnter(object sender, MouseEventArgs mouseEventArgs) //Field Optional Colored
        {
            var border = sender as Border;
            if (border != null)
            {
                border.Background = _optionalBrush;
                border.Height = 32;
                border.Width = 32;
            }
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e) //Field Normal Colored
        {
            var border = sender as System.Windows.Controls.Border;
            if (border != null)
            {
                border.Background = _foregroundBrush;
                border.Height = 30;
                border.Width = 30;
            }
        }
        
        private void SettingsWindow(object sender, MouseButtonEventArgs e) //Open Settings
        {
            Phexor.SettingsWindow settings = new SettingsWindow();
            settings.Show();
            NotTrueClose = true;
            this.Close();
        }

        private void LoadAllFields(string MeinPfad) //Set Folder/File-Fields Content
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
                        for (int i = 0; i < Fields; i++)
                        {
                            if (directoryCount < DirectorysNotDisplayed + FolderCount - Settingsfile.Fields)
                            {
                                _verzeichnisListe[DirectorysNotDisplayed].Text = null;
                            }

                            DirectorysNotDisplayed++;
                        }

                        DirectorysNotDisplayed = 0;

                        foreach (var directory in directors)
                        {
                            for (int i = 0; i < Fields; i++)
                            {
                                if (directoryCount < DirectorysNotDisplayed + FolderCount - Settingsfile.Fields)
                                {
                                    _verzeichnisListe[DirectorysNotDisplayed].Text = null;
                                }

                                DirectorysNotDisplayed++;
                            }

                            DirectorysNotDisplayed = 0;

                            if (directoryCount == DirectorysDisplayed + FolderCount - Settingsfile.Fields)
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
                    for (int i = 0; i < Fields; i++)
                    {
                        _verzeichnisListe[i].Text = null;
                    }
                }
                
                var files = Directory.GetFiles(directoryPath);
                if (files != null)
                {
                    try
                    {
                        for (int i = 0; i < Fields; i++)
                        {
                            if (fileCount < FilesNotDisplayed + FileCount - Settingsfile.Fields)
                            {
                                _fileListe[FilesNotDisplayed].Text = null;
                            }

                            FilesNotDisplayed++;
                        }

                        FilesNotDisplayed = 0;

                        foreach (var file in files)
                        {
                            for (int i = 0; i < Fields; i++)
                            {
                                if (fileCount < FilesNotDisplayed + FileCount - Settingsfile.Fields)
                                {
                                    _fileListe[FilesNotDisplayed].Text = null;
                                }

                                FilesNotDisplayed++;
                            }

                            FilesNotDisplayed = 0;

                            if (fileCount == FilesDisplayed + FileCount - Settingsfile.Fields)
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
                        for (int i = 0; i < Fields; i++)
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
                    for (int i = 0; i < Fields; i++)
                    {
                        _fileListe[i].Text = null;
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                for (int i = 0; i < Fields; i++)
                {
                    _verzeichnisListe[i].Text = null;
                    _fileListe[i].Text = null;
                }
            }
        }

        private void Field_Setting(object sender, MouseButtonEventArgs e) // Coming Soon
        {

        }

        private void File_RightClick(object sender, MouseButtonEventArgs e) // Coming Soon
        {

        }

        private void OnClose(object sender, EventArgs e) // Stop Running Prozesees
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
