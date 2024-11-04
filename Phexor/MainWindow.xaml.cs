﻿using System;
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
using System.Linq;
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
        private SolidColorBrush foregroundBrush; //C. V. for Xc. & C#c.
        private SolidColorBrush backgroundBrush; //C. V. for Xc. & C#c.
        private SolidColorBrush optionalBrush; //C. V. for Xc. & C#c.
        private SolidColorBrush Placeholders; //C. V. for Xc. & C#c.
        private List<TextBlock> DirectoryList = new List<TextBlock>(); //C. L. for LoadAllFields M.
        private List<TextBlock> fileList = new List<TextBlock>(); //C. L. for LoadAllFields M.
        private List<Image> fileImageList = new List<Image>(); //C. L. for LoadAllFields M.
        private List<Border> PlaceholderDirectoryList = new List<Border>(); //C. L. for LoadAllFields M.
        private List<Border> PlaceholderfileList = new List<Border>(); //C. L. for LoadAllFields M.
        private string Redostring; //C. V. for an Redo path string
        private string Path; //C. L. for LoadAllFields M.
        private string LastPath; //C. L. for LoadAllFields M.
        private int FileCount; //C. L. for LoadAllFields M.
        private int FolderCount; //C. L. for LoadAllFields M.

        public MainWindow()
        {
            Logging.Log("Initialize", "MainWindow"); //C. Log Entry
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/Grafiks/Icon.ico")); //Set Icon for Xc.
            InitializeComponent(); //Initialize Xc.
            var borders = new Border[] { Border1, Border2, Border3, Border4}; //C. L. with Xc. Objects
            var textBlocks = new TextBlock[] { TextBlock1, TextBlock2}; //C. L. with Xc. Objects
            CheckForSettings(); //Start M.
            foregroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settingsfile.ForegroundColor)!); //set V. for Xc. & C#c.
            backgroundBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settingsfile.BackgroundColor)!); //set V. for Xc. & C#c.
            optionalBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settingsfile.SpecialColor)!); //set V. for Xc. & C#c.
            Placeholders = Brushes.Transparent; //set V. C. for Xc. & C#c.
            Fields = Settingsfile.Fields; //set V. for C#c.
            FileCount = Fields; //set V. for LoadAllFields
            FolderCount = Fields; //set V. for LoadAllFields
            
            try //try to prevent Crashes
            {
                SettingsButton.Background = foregroundBrush; //set Xc. Colors
                BackButton.Background = foregroundBrush; //set Xc. Colors
                ForeButton.Background = foregroundBrush; //set Xc. Colors

                foreach (var border in borders) //loop for every Border in Borders
                {
                    border.BorderBrush = backgroundBrush; //set Xc. Colors
                }

                foreach (var textBlock in textBlocks) //loop for every textblock in textblocks
                {
                    textBlock.Foreground = foregroundBrush; //set Xc. Colors
                }

                Border5.Background = backgroundBrush; //set Xc. Colors
                Border6.Background = backgroundBrush; //set Xc. Colors
                PathInput.Foreground = foregroundBrush; //set Xc. Colors
            }
            catch (Exception e) //used for logs
            {
                Logging.CatchLog(Convert.ToString(e), "Mainwindow"); //use CatchLog M.
            }
            
            Logging.Log("Create Xc. Objects", "MainWindow"); //C. Log Entry
            for (int i = 0; i < Fields; i++) // Generating Folder & file Fields
            {
                TextBlock order = new TextBlock(); //G. L. Xc. Object
                order.Height = (Directorys.Height/Settingsfile.Fields - 1); //set Xc. Size
                order.Width = (Directorys.Width); //set Xc. Size
                order.Name = $"Directory{i}"; //set identification Number
                order.Foreground = foregroundBrush; //set Xc. Color
                order.FontSize = 10; //set Xc. Font Settings
                order.Text = ""; //set Xc. Font Settings
                order.MouseEnter += Border_MouseEnter; //set Xc. Event M.
                order.MouseLeave += Border_MouseLeave; //set Xc. Event M.
                order.MouseLeftButtonDown += Folder_Click; //set Xc. Event M.
                order.MouseRightButtonDown += Field_Setting; //set Xc. Event M.
                order.VerticalAlignment = VerticalAlignment.Center; //set Xc. Font Settings
                DirectoryList.Add(order); //add to L. 
                Directorys.Children.Add(order); //add to Xc.
                
                Border PlaceholderOrder = new Border(); //C. Xc. Object as Placeholder
                PlaceholderOrder.Height = 1; //set Height of the Xc. Placeholder
                PlaceholderOrder.Width = (Directorys.Width - 5); //set Width of the Xc. Placeholder
                PlaceholderDirectoryList.Add(PlaceholderOrder); //Add the Xc. Placeholder to an L.
                Directorys.Children.Add(PlaceholderOrder); //Add the Xc. Placeholder to the Xc.
                
                
                TextBlock Datei = new TextBlock(); //G. L. Xc. Object
                Datei.Height = (Files.Height/Settingsfile.Fields - 1); //set Xc. Size
                Datei.Width = (Files.Width); //set Xc. Size
                Datei.Name = $"File{i}"; //set identification Number
                Datei.Foreground = foregroundBrush; //set Xc. Color
                Datei.FontSize = 10; //set Xc. Font Settings
                Datei.Text = ""; //set Xc. Font Settings
                Datei.MouseEnter += Border_MouseEnter; //set Xc. Event M.
                Datei.MouseLeave += Border_MouseLeave; //set Xc. Event M.
                Datei.MouseLeftButtonDown += OpenFile; //set Xc. Event M.
                Datei.MouseRightButtonDown += File_RightClick; //set Xc. Event M.
                Datei.VerticalAlignment = VerticalAlignment.Center; //set Xc. Font Settings
                fileList.Add(Datei); //add to L. 
                Files.Children.Add(Datei); //add to Xc.

                Border PlaceholderFile = new Border(); //C. Xc. Object as Placeholder
                PlaceholderFile.Height = 1; //set Height of the Xc. Placeholder
                PlaceholderFile.Width = (Files.Width - 5); //set Width of the Xc. Placeholder
                PlaceholderfileList.Add(PlaceholderFile); //Add the Xc. Placeholder to an L.
                Files.Children.Add(PlaceholderFile); //Add the Xc. Placeholder to the Xc.
                
                
                Image FileImage = new Image(); //C. new FileImage Image Xc. Object
                FileImage.Height = (Files.Height/Settingsfile.Fields  -1); //set Xc. Size
                FileImage.Width = (Files.Height/Settingsfile.Fields + 2); //set Xc. Size
                FileImage.Stretch = Stretch.Uniform; //Prevent Cuttet Images in Xc. 
                FileImage.HorizontalAlignment = HorizontalAlignment.Right; //set Xc. Aligments
                FileImage.VerticalAlignment = VerticalAlignment.Center; //set Xc. Aligments
                FileImage.Name = $"FileImage{i}"; //set idendification Number
                fileImageList.Add(FileImage); //add to L.
                FileImages.Children.Add(FileImage); //add to Xc.

                Border PlaceholderFileImages = new Border(); //C. Xc. Object as Placeholder
                PlaceholderFileImages.Height = 1; //set Height of the Xc. Placeholder
                PlaceholderFileImages.Width = (Files.Width - 5); //set Width of the Xc. Placeholder
                FileImages.Children.Add(PlaceholderFileImages); //Add the Xc. Placeholder to the Xc.
            }
        }
        
        private void CheckForSettings() // search/Set Settings
        {
            Logging.Log("Check for Settings", "MainWindow"); //C. Log Entry
            if (!File.Exists(Settingsfile.SettingsFiles)) //check for File existens
            {
                Settingsfile.SetSettings("#FFFFF8DC", "#FFF0FFFF", "#FFE6E6FA", 20); //set settings in txt
                Settingsfile.GetSettings(); //get Settings from txt
                this.Hide(); //vanish this Window
            }
            else
            {
                Settingsfile.GetSettings(); //get Settings from txt
            }
        }
        
        private void ShortCuts(object sender, KeyEventArgs e) //ShortCut M.
        {
            if (e.Key == Key.I && !PathInput.IsKeyboardFocused) //check if pressed Key is I (Input) and PathInput is not Keyboard Focused
            {
                Logging.Log("Using Shortcut I", "MainWindow"); //C. Log Entry
                Keyboard.Focus(PathInput); //Set the Focus to PathInput Textbox
            }
            else if (e.Key == Key.P && !PathInput.IsKeyboardFocused) //check if pressed Key is P (Placheolders) and PathInput is not Keyboard Focused
            {
                if (Placeholders == Brushes.Transparent) //If Placheolders beeing Invisibel
                {
                    Logging.Log("Using Shortcut P (Activate)", "MainWindow"); //C. Log Entry
                    Placeholders = optionalBrush; //Coloring the Placeholders
                }
                else //if Placeholder arent Invisibel
                {
                    Logging.Log("Using Shortcut P (Deactivate)", "MainWindow"); //C. Log Entry
                    Placeholders = Brushes.Transparent; //Make Placeholders Invisibel
                }

                if (Path != null && Path != String.Empty && Path != "") //Check for an Empty Path
                {
                    LoadAllFields(Path); //call LoadALlFields M.
                } 
            }
            else if (e.Key == Key.U && !PathInput.IsKeyboardFocused) //check if pressed Key is U (Undo) and PathInput is not Keyboard Focused
            {

                Logging.Log("Using Shortcut U", "MainWindow"); //C. Log Entry
                UndoFunction(null, null); //call UndoFunction M.
            }
            else if (e.Key == Key.R && !PathInput.IsKeyboardFocused) //check if pressed Key is R (Redo) and PathInput is not Keyboard Focused
            {
                Logging.Log("Using Shortcut R", "MainWindow"); //C. Log Entry
                RedoFunction(null, null); //call RedoFunction M.
            }
            else if (e.Key == Key.S && !PathInput.IsKeyboardFocused) //check if pressed Key is S (Settings) and PathInput is not Keyboard Focused
            {
                Logging.Log("Using Shortcut S", "MainWindow"); //C. Log Entry
                SettingsWindow(null, null); // call SettingsWindow M.
            }
        }
        
        private void MouseShortCuts(object sender, MouseButtonEventArgs e) //M. for Mouseshortcuts
        {
            if (e.ChangedButton == MouseButton.Left && PathInput.IsKeyboardFocused) //Check if Pressed Mousebutton is Leftclick and PathInput is not Keyboard Focused
            {
                Logging.Log("Using Shortcut LeftClick", "MainWindow"); //C. Log Entry
                Keyboard.ClearFocus(); //Clear Every Focus from Keyboard
                PathInput.Focusable = false; //Set PathInputs Focusable ability to false
                Keyboard.Focus(Window); //Focus Every Object in Window (everything)
                PathInput.Focusable = true; //Set PathInputs Focusable ability to true
            }
        }
        
        private void PfadInput_OnKeyDown(object sender, KeyEventArgs e) //Start Exploring
        {
            if (e.Key == Key.Enter) //check which Button got pressed
            {
                Logging.Log("Enter Input", "MainWindow"); //C. Log Entry
                setPath(PathInput.Text); //set Path to Path from textbox
                LoadAllFields(Path); //start the LoadAllFields M.
                Redostring = Path;//Add the Current Path to the Redostring
                Keyboard.ClearFocus(); //Clear Every Focus from Keyboard
                PathInput.Focusable = false; //Set PathInputs Focusable ability to false
                Keyboard.Focus(Window); //Focus Every Object in Window (everything)
                PathInput.Focusable = true; //Set PathInputs Focusable ability to true
            }
        }
        
        private void DirectoryScrollingWithMouse(object sender, MouseWheelEventArgs e) //M. to react to scrolling
        {
            if (e.Delta < 0) //check in which direction got scrolled
            {
                Logging.Log("Directory Scrolling (+)", "MainWindow"); //C. Log Entry
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
                Logging.Log("Directory Scrolling (-)", "MainWindow"); //C. Log Entry
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
                Logging.Log("File Scrolling (+)", "MainWindow"); //C. Log Entry
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
                Logging.Log("File Scrolling (-)", "MainWindow"); //C. Log Entry
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
            if (textBlock.Text != "" && textBlock.Text != null) //check if textblocks text is empty or null
            {
                Logging.Log("Select new Underfolder", "MainWindow"); //C. Log Entry
                PathInput.Text = Path + textBlock.Text; //set new text for the path input textbox
                LastPath = textBlock.Text; //set new lastpath
                FolderCount = Fields; //reset the Foldercount
                FileCount = Fields; //reset the Filecount
                LoadAllFields(Path + textBlock.Text); //start LoadAllFields M.
                Redostring = Path; //Add the Current Path to the Redostring
            }
            else
            {
                Logging.CatchLog("Underfolder cant be Selected (Empty)", "MainWindow"); //C. Log Entry
            }
        }
        
        private void OpenFile(object sender, MouseButtonEventArgs e) //Open File on Click event
        {
            var filePath = sender as TextBlock; //C. V. for the TextBlock

            try //try to prevent Crashes (if file can´t be opened)
            {
                Logging.Log("Open File", "MainWindow"); //C. Log Entry
                if (filePath != null && (filePath.Text != null && filePath.Text != "")) //check if filepath is not null and Text also not null and not empty
                {
                    if (filePath.Text != null) Process.Start(Path + @"\" + filePath.Text); //Start the Selected File 
                }
            }
            catch (Exception exception) //used for logs
            {
                Logging.CatchLog(Convert.ToString(e), "Mainwindow"); //use CatchLog M.
            }
        }

        private void Border_MouseEnter(object sender, MouseEventArgs mouseEventArgs) //M. to make an event if an Refered Object got an MousEnter event
        {
            if (sender is Border) //check the Type of Sender
            {
                var border = sender as Border; //C. V. for sender (Border)
                if (border != null)
                {
                    border.Background = optionalBrush; //change the color of the Xc. V.
                    border.Height = 32; //change the size of the Xc. V.
                    border.Width = 32; //change the size of the Xc. V.
                }
            }
            else if (sender is TextBlock) //check the Type of Sender
            {
                var TextBlock = sender as TextBlock; //C. V. for sender (Textblock)
                if (TextBlock.Text != null && TextBlock.Text != "") //check if Textblocks text is not empty
                {
                    TextBlock.Foreground = optionalBrush; //change the Font color of the Xc. V.
                }
            }
            else if (sender is TextBox) //check the Type of Sender
            {
                if (Equals(sender, PathInput)) //check if sender and PathInput are the same
                {
                    if (Path == "" || Path == null) //check for an Empty Path
                    {
                        PathInput.Text = String.Empty; //clear the Path input textboxs text
                    }
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
                    border.Background = foregroundBrush; //change the color of the Xc. V.
                    border.Height = 30; //change the size of the Xc. V.
                    border.Width = 30; //change the size of the Xc. V.
                }
            }
            else if (sender is TextBlock) //check the Type of Sender
            {
                var TextBlock = sender as TextBlock; //C. V. for sender (Textblock)
                if (TextBlock.Text != null && TextBlock.Text != "") //check if Textblocks text is not empty
                {
                    TextBlock.Foreground = foregroundBrush; //change the Font color of the Xc. V.
                }
            }
            else if (sender is TextBox) //check the Type of Sender
            {
                if (Equals(sender, PathInput) && !PathInput.IsKeyboardFocused) //check if sender and PathInput are the same
                {
                    if (Path == "" || Path == null) //Check for an Empty Path
                    {
                        PathInput.Text = "Füge Pfad ein..."; //set the Path input Textboxs text
                    }
                }
            }
        }
        
        private void SettingsWindow(object sender, MouseButtonEventArgs e) //Open Settings Window
        {
            Logging.Log("Open Settings", "MainWindow"); //C. Log Entry
            Phexor.SettingsWindow settings = new SettingsWindow(); //C. new Settings window
            settings.Show(); //show the Created Settings window
            NotTrueClose = true; //prevent an close of the program
            this.Close(); // close this window
        }

        private void UndoFunction(object sender, MouseButtonEventArgs mouseButtonEventArgs) //Undo Path Change
        {
            if (Path != null && Path != String.Empty && Path != "") //Check for Path emptynes
            {
                Logging.Log("Undo M.", "MainWindow"); //C. Log Entry
                List<string> Pathparts = new List<string>(Path.Split('\\')); //Split the Current Path
                Pathparts.RemoveAll(s => s == ""); //Remove every empty Path part
                var pathPartCount = Pathparts.Count(); // get the Count of the remaining Path parts
                var UndoPath = ""; //C. V. for the new Undo path
                for (int i = 0; i < pathPartCount - 1; i++) // loop for every existing path part
                {
                    UndoPath = UndoPath + Pathparts[i] + @"\"; //C. the new Path from the parts
                }
                setPath(UndoPath); // set created Path
                LoadAllFields(Path); //use M. LoadAllFields with the new Path
                PathInput.Text = Path; //change the Pathinput text to the new Path
            }
            else //use for logs
            {
                Logging.CatchLog("Cant Undo (Path Empty)", "MainWindow"); //C. Log Entry
            }
        }

        private void RedoFunction(object sender, MouseButtonEventArgs mouseButtonEventArgs) //Redo Path change
        {
            if (Redostring != null && Redostring != "" && Redostring != Path) //checks if Redostring is Empty and not the same as Path
            {
                Logging.Log("Redo M.", "MainWindow"); //C. Log Entry
                List<string> setPathParts = new List<string>(Path.Split('\\')); //Split the Path into path parts
                setPathParts.RemoveAll(s => s == ""); //Remove every Empty Path part
                
                List<string> setRedoStringParts = new List<string>(Redostring.Split('\\')); //Split the Redostring into Redostring parts
                setRedoStringParts.RemoveAll(s => s == ""); //Remove every Empty Redostring part

                if (setRedoStringParts.Count > setPathParts.Count) //check if after Removing empty parts Redostring still is higher
                {
                    int RedoPartsCount = setPathParts.Count + 1; //C. V. RedoPartsCount to check how much Redoparts need to form the new Path
                    Path = ""; //Remove the Existing Path
                    foreach (var RedoStringPart in setRedoStringParts) //Loop for every RedoString
                    {
                        if (RedoPartsCount > 0) //prevent to much Parts geting added
                        {
                            Path = Path + RedoStringPart + @"\"; //Adding Part to the Path
                        }
                        RedoPartsCount--; //decrease the RedoPartsCount
                    }
                    LoadAllFields(Path); //use M. LoadAllFields with the new Path
                    PathInput.Text = Path; //Set the Pathinput textbox content to the new Path
                }
            }
            else //use for Logs
            {
                Logging.CatchLog("Cant Redo (Nothing to Redo)", "MainWindow"); //C. Log Entry
            }
        }

        private void setPath(string Input) //Path setting M.
        {
            Logging.Log("Set new Path", "MainWindow"); //C. Log Entry
            List<string> setPathParts = new List<string>(Input.Split('\\')); //Split the Input into path parts
            setPathParts.RemoveAll(s => s == ""); //removing every empty path part
            Path = ""; //set parth to empty
            foreach (var setPathPart in setPathParts) //loop for every path part
            {
                Path = Path + setPathPart + @"\"; //C. new Path
            }
        }

        private void LoadAllFields(string myPath) //Set Folder/File-Fields Content
        {
            Logging.Log("Load new Path", "MainWindow"); //C. Log Entry
            if (Directory.Exists(myPath) || File.Exists(myPath)) //check for an not Existing Path
            {
                string directoryPath = myPath + @"\"; //set the directorypath to the M. input
                setPath(directoryPath); //set the path to the directorypath
                int directoryPathLength = directoryPath.Length; //C. V. for the length of the Path

                var directoryCount = 0; //C. V. for the directorys
                var DirectorysDisplayed = 0; //C. V. for the directorys
                var DirectorysNotDisplayed = 0; //C. V. for the directorys
            
                var fileCount = 0; //C. V. for the files
                var FilesDisplayed = 0; //C. V. for the files
                var FilesNotDisplayed = 0; //C. V. for the files
                
                for (int i = 0; i < Fields; i++) //Reset the Input and Color of every Field 
                {
                    DirectoryList[i].Text = null; //Reset text of Directory Textblock
                    DirectoryList[i].Foreground = foregroundBrush; //Reset Foregroundbrush of Directory Textblock
                    fileList[i].Text = null; //Reset text of File Textblock
                    fileList[i].Foreground = foregroundBrush; //Reset Foregroundbrush of File Textblock
                    fileImageList[i].Source = null; //clear the fileImage Image-source
                    PlaceholderDirectoryList[i].Background = Brushes.Transparent; //Clear the Placeholders Visbility (with Background Color)
                    PlaceholderfileList[i].Background = Brushes.Transparent; //Clear the Placeholders Visbility (with Background Color)
                }
                
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
                                        DirectoryList[DirectorysNotDisplayed].Text = null; //Removing the text from the Textblock
                                        PlaceholderDirectoryList[DirectorysNotDisplayed].Background = Brushes.Transparent; //Clear the Placeholders Visbility (with Background Color)
                                    }

                                    DirectorysNotDisplayed++; //increase DirectorysNotDisplayed count
                                }

                                DirectorysNotDisplayed = 0; //reset DirectorysNotDisplayed count

                                if (directoryCount == DirectorysDisplayed + FolderCount - Settingsfile.Fields) //check for the position of the Textblock
                                {
                                    DirectoryList[DirectorysDisplayed].Text = directory.Substring(directoryPathLength); //setting the Textblocks Text to the directory name (just name, without Path)
                                    PlaceholderDirectoryList[DirectorysDisplayed].Background = Placeholders; //Set the Placeholders Visibility (with background Color)
                                    DirectorysDisplayed++; //increase DirectorysDisplayed count
                                }

                                directoryCount++; //increase directorycount
                            }
                        }
                        catch (Exception e) //used for logs
                        {
                            Logging.CatchLog(Convert.ToString(e), "Mainwindow"); //use CatchLog M.
                        }
                    }
                    else
                    {
                        for (int i = 0; i < Fields; i++)
                        {
                            DirectoryList[i].Text = null; //Clear the Text from every TextBlock
                            PlaceholderDirectoryList[i].Background = Brushes.Transparent; //Clear the Placeholders Visbility (with Background Color)
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
                                        fileList[FilesNotDisplayed].Text = null; //Set Text of Textblock to null
                                        PlaceholderfileList[FilesNotDisplayed].Background = Brushes.Transparent; //Clear the Placeholders Visbility (with Background Color)
                                    }

                                    FilesNotDisplayed++; //Increase FilesNotDisplayed count
                                }

                                FilesNotDisplayed = 0; //reset FilesNotDisplayed Count

                                if (fileCount == FilesDisplayed + FileCount - Settingsfile.Fields) //prove the position for this Text
                                {
                                    fileList[FilesDisplayed].Text = file.Substring(directoryPathLength); //Set the Text of the Textblocks (just the Filename, not Path)
                                    PlaceholderfileList[FilesDisplayed].Background = Placeholders; //Set the Placeholders Visibility (with background Color)
                                    if (file.EndsWith(".txt") || file.EndsWith(".doc") || file.EndsWith(".docx") || file.EndsWith("odt") || file.EndsWith(".rtf")) //Check if file is text-file
                                    {
                                        fileImageList[FilesDisplayed].Source = new BitmapImage(new Uri("Grafiks/TXT.png", UriKind. Relative)); //Set matching File-Image
                                    }
                                    else if (file.EndsWith(".xls") || file.EndsWith(".xlsx") || file.EndsWith(".ods") || file.EndsWith(".csv")) //Check if file is an Excel-file or similiar
                                    {
                                        fileImageList[FilesDisplayed].Source = new BitmapImage(new Uri("Grafiks/XLS.png", UriKind. Relative)); //Set matching File-Image
                                    }
                                    else if (file.EndsWith(".ppt") || file.EndsWith("-pptx") || file.EndsWith(".odp")) //Check if file is an Presentation-file
                                    {
                                        fileImageList[FilesDisplayed].Source = new BitmapImage(new Uri("Grafiks/PPTX.png", UriKind. Relative)); //Set matching File-Image
                                    }
                                    else if (file.EndsWith(".pdf")) //Check if file is an PDF-file
                                    {
                                        fileImageList[FilesDisplayed].Source = new BitmapImage(new Uri("Grafiks/PDF.png", UriKind. Relative)); //Set matching File-Image
                                    }
                                    FilesDisplayed++; //incrase FilesDisplayed Count
                                }

                                fileCount++; //increase filecount
                            }
                        }
                        catch (Exception e) //used for logs
                        { 
                            Logging.CatchLog(Convert.ToString(e), "Mainwindow"); //use CatchLog M.
                        }
                    }
                    else //clearing the file L.
                    {
                        for (int i = 0; i < Fields; i++)
                        {
                            fileList[i].Text = null; //Clear the Text from every TextBlock
                            PlaceholderfileList[i].Background = Brushes.Transparent; //Clear the Placeholders Visbility (with Background Color)
                        }
                    }
                    
                }
                catch (Exception e) //clearing all th Fields and Write a Log
                {
                    Logging.CatchLog(Convert.ToString(e), "Mainwindow"); //use CatchLog M.
                }
            }
            else
            {
                Logging.CatchLog("Path Not Exists", "Mainwindow"); //use CatchLog M.
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
            Logging.Log("Close Started", "MainWindow"); //C. Log Entry
            if (!NotTrueClose) //check for V. to know what kind of close
            {
                var phexorProcesses = Process.GetProcessesByName("Phexor"); //check for every active process
                foreach (var process in phexorProcesses) 
                {
                    try //prevent crashes
                    {
                        process.Kill(); //"Kill" every running process
                        process.WaitForExit(); //Wait until it got definitly closed
                    }
                    catch (Exception) //for Logs used
                    {
                        Logging.CatchLog(Convert.ToString(e), "Mainwindow"); //use CatchLog M.
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
