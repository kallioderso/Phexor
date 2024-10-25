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
    // C. = Create
    // G. = Generate
    // V. = Variable
    // L. = List
    // M. = Method
    // Xc. = XAML code
    // C#c. = C# code
    public partial class MainWindow
    {
        private bool NotTrueClose = false; //C. V. for Close M.
        private int Fields; //C. V. for LoadAllFields M.
        private SolidColorBrush _foregroundBrush; //C. V. for Xc. & C#c.
        private SolidColorBrush _backgroundBrush; //C. V. for Xc. & C#c.
        private SolidColorBrush _optionalBrush; //C. V. for Xc. & C#c.
        private List<TextBlock> _verzeichnisListe = new List<TextBlock>(); //C. L. for LoadAllFields M.
        private List<TextBlock> _fileListe = new List<TextBlock>(); //C. L. for LoadAllFields M.
        private string Path = ""; //C. L. for LoadAllFields M.
        private string LastPath; //C. L. for LoadAllFields M.
        private int FileCount = 0; //C. L. for LoadAllFields M.
        private int FolderCount = 0; //C. L. for LoadAllFields M.

        public MainWindow()
        {
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/Grafiks/Icon.ico")); //Set Icon for Xc.
            InitializeComponent(); //Initialize Xc.
            var borders = new Border[] { Border1, Border2, Border3, Border4}; //C. L. with Xc. Objects
            var textBlocks = new TextBlock[] { TextBlock1, TextBlock2}; //C. L. with Xc. Objects
            CheckForSettings(); //Start M.
            _foregroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settingsfile.ForegroundColor)!); //set V. for Xc. & C#c.
            _backgroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settingsfile.BackgroundColor)!); //set V. for Xc. & C#c.
            _optionalBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settingsfile.OptionalColor)!); //set V. for Xc. & C#c.
            Fields = Settingsfile.Fields; //set V. for C#c.
            FileCount = Fields; //set V. for LoadAllFields
            FolderCount = Fields; //set V. for LoadAllFields
            
            try //try to prevent Crashes
            {
                SettingsButton.Background = _foregroundBrush; //set Xc. Colors

                foreach (var border in borders)
                {
                    border.BorderBrush = _backgroundBrush; //set Xc. Colors
                }

                foreach (var textBlock in textBlocks)
                {
                    textBlock.Foreground = _foregroundBrush; //set Xc. Colors
                }

                Border5.Background = _backgroundBrush; //set Xc. Colors
                PathInput.Foreground = _foregroundBrush; //set Xc. Colors
            }
            catch (Exception e) //laterly for Logs used
            {
            }
            

            for (int i = 0; i < Fields; i++) // Generating Folder & file Fields
            {
                TextBlock order = new TextBlock(); //G. L. Xc. Object
                order.Height = (Directorys.Height/Settingsfile.Fields); //set Xc. Size
                order.Width = (Directorys.Width); //set Xc. Size
                order.Name = $"Directory{i}"; //set identification Number
                order.Foreground = _foregroundBrush; //set Xc. Color
                order.FontSize = 10; //set Xc. Font Settings
                order.Text = ""; //set Xc. Font Settings
                order.MouseEnter += Border_MouseEnter; //set Xc. Event M.
                order.MouseLeave += Border_MouseLeave; //set Xc. Event M.
                order.MouseLeftButtonDown += Folder_Click; //set Xc. Event M.
                order.MouseRightButtonDown += Field_Setting; //set Xc. Event M.
                order.VerticalAlignment = VerticalAlignment.Top; //set Xc. Font Settings
                _verzeichnisListe.Add(order); //add to L. 
                Directorys.Children.Add(order); //add to Xc.
                
                TextBlock Datei = new TextBlock(); //G. L. Xc. Object
                Datei.Height = (Files.Height/Settingsfile.Fields); //set Xc. Size
                Datei.Width = (Files.Width); //set Xc. Size
                Datei.Name = $"File{i}"; //set identification Number
                Datei.Foreground = _foregroundBrush; //set Xc. Color
                Datei.FontSize = 10; //set Xc. Font Settings
                Datei.Text = ""; //set Xc. Font Settings
                Datei.MouseEnter += Border_MouseEnter; //set Xc. Event M.
                Datei.MouseLeave += Border_MouseLeave; //set Xc. Event M.
                Datei.MouseLeftButtonDown += OpenFile; //set Xc. Event M.
                Datei.MouseRightButtonDown += File_RightClick; //set Xc. Event M.
                Datei.VerticalAlignment = VerticalAlignment.Top; //set Xc. Font Settings
                _fileListe.Add(Datei); //add to L. 
                Files.Children.Add(Datei); //add to Xc.
            }
        }
        
        private void CheckForSettings() // search/Set Settings
        {
            if (!File.Exists(Settingsfile.SettingsDatei)) //check for File existens
            {
                Settingsfile.SetSettings("#FFFFF8DC", "#FFF0FFFF", "#FFE6E6FA", 20, false); //set settings in txt
                Settingsfile.GetSettings(); //get Settings from txt
                this.Hide(); //vanish this Window
            }
            else
            {
                Settingsfile.GetSettings(); //get Settings from txt
            }
        }
        
        private void RemoveStandardText(object sender, MouseEventArgs mouseEventArgs) // Remove Standard Text (Input Field)
        {
            if (Path == "") //check for an Empty Path
            {
                PathInput.Text = String.Empty; //clear the Path input textbox
            }
        }
        
        private void PfadInput_OnKeyDown(object sender, KeyEventArgs e) //Start Exploring
        {
            if (e.Key == Key.Enter) //check which Button got pressed
            {
                Path = PathInput.Text; //set Path to Path from textbox
                LoadAllFields(Path); //start the LoadAllFields M.
            }
        }
        
        private void DirectoryScrollingWithMouse(object sender, MouseWheelEventArgs e) //M. to react to scrolling
        {
            if (e.Delta < 0) //check in which direction got scrolled
            {
                if (Directory.Exists(PathInput.Text)) //check if Directory Exists
                {
                    FolderCount++; //Increase  the Foldercount for LoadAllFields
                    LoadAllFields(PathInput.Text); //start LoadAllFields M.
                }
                else
                {
                }
            }
            else if (e.Delta > 0) //check if the other direction was it
            {
                if (FolderCount != Settingsfile.Fields) //check if FolderCount goes to low
                {
                    FolderCount--; //decrease the Foldercount for LoadAllFields
                    LoadAllFields(PathInput.Text); //start LoadAllFields M.
                }
                else
                {
                }
            }
        }

        private void FileScrollingWithMouse(object sender, MouseWheelEventArgs e) //M. to react to scrolling
        {
            if (e.Delta < 0) //check in which direction got scrolled
            {
                if (Directory.Exists(PathInput.Text)) //check if Directory Exists
                {
                    FileCount++; //increase Filecount for LoadAllFields M.
                    LoadAllFields(PathInput.Text); //start LoadAllFields M.
                }
                else
                {
                }
            }
            else if (e.Delta > 0) //check if the other direction was it
            {
                if (FileCount != Settingsfile.Fields) //check if FolderCount goes to low
                {
                    FileCount--; //decrease the Filecount for the LoadAllFields M.
                    LoadAllFields(PathInput.Text); //start LoadAllFields M.
                }
                else
                {
                }
            }
        }
        
        private void Folder_Click(object sender, RoutedEventArgs e) //M. to react on Folderclick
        {
            var textBlock = sender as System.Windows.Controls.TextBlock; //C. an V. for the TextBlock
            if (textBlock.Text == "" || textBlock.Text == null) //check if textblocks text is empty or null
            {
                return;
            }
            else
            {
                if (textBlock.Text != LastPath) //check for un Doubble Path Bug
                {
                    PathInput.Text = Path + textBlock.Text; //set new text for the path input textbox
                    LastPath = textBlock.Text; //set new lastpath
                    FolderCount = Fields; //reset the Foldercount
                    FileCount = Fields; //reset the Filecount
                    LoadAllFields(Path + textBlock.Text); //start LoadAllFields M.
                }
                else
                {
                    for (int i = 0; i < Fields; i++) //Removing all the Files and Folders
                    {
                        _verzeichnisListe[i].Text = null; //removing the Folders
                        _fileListe[i].Text = null; //removing the files
                    }
                }
            }
        }
        
        private void OpenFile(object sender, MouseButtonEventArgs e) //Open File on Click event
        {
            var filePath = sender as TextBlock; //C. V. for the TextBlock

            try //try to prevent Crashes (if file can´t be opened)
            {
                if (filePath != null && (filePath.Text != null && filePath.Text != "")) //check if filepath is not null and Text also not null and not empty
                {
                    if (filePath.Text != null) Process.Start(Path + @"\" + filePath.Text); //Start the Selected File 
                }
            }
            catch (Exception exception) //laterly for Logs used
            {
            }
        }

        private void Border_MouseEnter(object sender, MouseEventArgs mouseEventArgs) //M. to make an event if an Refered Object got an MousEnter event
        {
            if (sender is Border) //check the Type of Sender
            {
                var border = sender as Border; //C. V. for sender (Border)
                if (border != null)
                {
                    border.Background = _optionalBrush; //change the color of the Xc. V.
                    border.Height = 32; //change the size of the Xc. V.
                    border.Width = 32; //change the size of the Xc. V.
                }
            }
            else
            {
                var TextBlock = sender as TextBlock; //C. V. for sender (Textblock)
                if (TextBlock.Text != null && TextBlock.Text != "") //check if Textblocks text is not empty
                {
                    TextBlock.Foreground = _optionalBrush; //change the Font color of the Xc. V.
                }
            }
            
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e) //M. to make an event if an Refered Object got an MousLeave event
        {
            if (sender is Border) //check the Type of Sender
            {
                var border = sender as Border; //C. V. for sender (Border)
                if (border != null)
                {
                    border.Background = _foregroundBrush; //change the color of the Xc. V.
                    border.Height = 30; //change the size of the Xc. V.
                    border.Width = 30; //change the size of the Xc. V.
                }
            }
            else
            {
                var TextBlock = sender as TextBlock; //C. V. for sender (Textblock)
                if (TextBlock.Text != null && TextBlock.Text != "") //check if Textblocks text is not empty
                {
                    TextBlock.Foreground = _foregroundBrush; //change the Font color of the Xc. V.
                }
            }
        }
        
        private void SettingsWindow(object sender, MouseButtonEventArgs e) //Open Settings Window
        {
            Phexor.SettingsWindow settings = new SettingsWindow(); //C. new Settings window
            settings.Show(); //show the Created Settings window
            NotTrueClose = true; //prevent an close of the program
            this.Close(); // close this window
        }

        private void LoadAllFields(string myPath) //Set Folder/File-Fields Content
        {
            string directoryPath = myPath + @"\"; //set the directorypath to the M. input
            Path = directoryPath; //set the path to the directorypath
            int directoryPathLength = directoryPath.Length; //C. V. for the length of the Path

            var directoryCount = 0; //C. V. for the directorys
            var DirectorysDisplayed = 0; //C. V. for the directorys
            var DirectorysNotDisplayed = 0; //C. V. for the directorys
            
            var fileCount = 0; //C. V. for the files
            var FilesDisplayed = 0; //C. V. for the files
            var FilesNotDisplayed = 0; //C. V. for the files
            
            try //prevent crashes
            {
                var directors = Directory.GetDirectories(directoryPath); //Adding Every Directory to the directory L.
                if (directors != null) //check if directorys are empty
                {
                    try //prevent crashes
                    {
                        foreach (var directory in directors) //loop for each directory in the path
                        {
                            for (int i = 0; i < Fields; i++) //loop for the Amount of Fields
                            {
                                if (directoryCount < DirectorysNotDisplayed + FolderCount - Settingsfile.Fields) //prove the position for this text removing 
                                {
                                    _verzeichnisListe[DirectorysNotDisplayed].Text = null; //Removing the text from the Textblock
                                }

                                DirectorysNotDisplayed++; //increase DirectorysNotDisplayed count
                            }

                            DirectorysNotDisplayed = 0; //reset DirectorysNotDisplayed count

                            if (directoryCount == DirectorysDisplayed + FolderCount - Settingsfile.Fields) //check for the position of the Textblock
                            {
                                _verzeichnisListe[DirectorysDisplayed].Text = directory.Substring(directoryPathLength); //setting the Textblocks Text to the directory name (just name, without Path)
                                DirectorysDisplayed++; //increase DirectorysDisplayed count
                            }

                            directoryCount++; //increase directorycount
                        }
                    }
                    catch (Exception e) //laterly used for logs
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
                
                var files = Directory.GetFiles(directoryPath); //get every File from the Path
                if (files != null) //check if Files L. is empty
                {
                    try //prevent Crashes
                    {
                        foreach (var file in files) // Loop for every file 
                        {
                            for (int i = 0; i < Fields; i++) //loop for the maximum amount of settet Fields
                            {
                                if (fileCount < FilesNotDisplayed + FileCount - Settingsfile.Fields) //prove the position for this text removing 
                                {
                                    _fileListe[FilesNotDisplayed].Text = null; //Set Text of Textblock to null
                                }

                                FilesNotDisplayed++; //Increase FilesNotDisplayed count
                            }

                            FilesNotDisplayed = 0; //reset FilesNotDisplayed Count

                            if (fileCount == FilesDisplayed + FileCount - Settingsfile.Fields) //prove the position for this Text
                            {
                                _fileListe[FilesDisplayed].Text = file.Substring(directoryPathLength); //Set the Text of the Textblocks (just the Filename, not Path)
                                FilesDisplayed++; //incrase FilesDisplayed Count
                            }

                            fileCount++; //increase filecount
                        }
                    }
                    catch (Exception e) //Clearing the text of every File Textblock text
                    {
                        FilesNotDisplayed = 0; //reset V.
                        for (int i = 0; i < Fields; i++) //loop
                        {
                            if (i < FilesNotDisplayed) //check if FilesNotDisplayed is bigger than the loop count
                            {
                                _fileListe[FilesNotDisplayed].Text = null; //Remove the text out of the Textblock
                            }

                            FilesNotDisplayed++; //Increase Count of V.
                        }

                        FilesNotDisplayed = 0; //reset V.
                    }
                }
                else //clearing the file L.
                {
                    for (int i = 0; i < Fields; i++)
                    {
                        _fileListe[i].Text = null;
                    }
                }
                
            }
            catch (Exception e) //clearing all th Fields
            {
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
            
            if (!NotTrueClose) //check for V. to know what kind of close
            {
                var phexorProcesses = Process.GetProcessesByName("Phexor"); //check for every active process
                foreach (var process in phexorProcesses) 
                {
                    try //prevent crashes
                    {
                        process.Kill(); //"KIll" every running process
                        process.WaitForExit(); //Wait until it got definitly closed
                    }
                    catch (Exception) //laterly for Logs used
                    {
                    }
                } 
            }
            else
            {
                NotTrueClose = false; //set the V. again to false
            }
        }
    }
}
