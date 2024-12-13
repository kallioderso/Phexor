using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Phexor.Config;
using Phexor.Scripts;
using Phexor.ViewModels;
using SolidColorBrush = System.Windows.Media.SolidColorBrush;

namespace Phexor.Views
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
        public ApplicationSettings ApplicationSettings { get; set; }
        

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="appSettings"></param>
        public MainWindow(ApplicationSettings appSettings)
        {
            Logging.Log("Initialize", "MainWindow");
            
            //Init Components
            InitializeComponent();
            
            ApplicationSettings = appSettings;
           
        }
        
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (DataContext is MainViewModel viewModel)
                {
                    viewModel.CurrentPath = (sender as TextBox)?.Text!;
                }
            }
        }
        
    //     private void CheckForSettings() // search/Set Settings
    //     {
    //         Logging.Log("Check for Settings", "MainWindow"); //C. Log Entry
    //         if (!File.Exists(Settingsfile.SettingsFiles)) //check for File existens
    //         {
    //             Settingsfile.SetSettings("#FFFFF8DC", "#FFF0FFFF", "#FFE6E6FA", 20, 7); //set settings in txt
    //             Settingsfile.GetSettings(); //get Settings from txt
    //             this.Hide(); //vanish this Window
    //         }
    //         else
    //         {
    //             Settingsfile.GetSettings(); //get Settings from txt
    //         }
    //     }
    //     
    //     private void ShortCuts(object sender, KeyEventArgs e) //ShortCut M.
    //     {
    //         if (e.Key == Key.I && !PathInput.IsKeyboardFocused && !FileCreation && !FolderCreation) //check if pressed Key is I (Input) and PathInput is not Keyboard Focused and no File or Folder gets Created
    //         {
    //             Logging.Log("Using Shortcut I", "MainWindow"); //C. Log Entry
    //             Keyboard.Focus(PathInput); //Set the Focus to PathInput Textbox
    //             if (Path == "" || Path == null || Path == String.Empty) //checks for an empty Path
    //             {
    //                 PathInput.Text = ""; //resets PathInputs Text
    //             }
    //             e.Handled = true; //set Keydownevent to complet
    //         }
    //         else if (e.Key == Key.Escape && PathInput.IsKeyboardFocused && !FileCreation && !FolderCreation) //check if pressed Key is ESC (Escape) and Pathinput is Keyboard Focused and no File or Folder gets Created
    //         {
    //             Logging.Log("Using Shortcut Esc", "MainWindow"); //C. Log Entry
    //             Keyboard.ClearFocus(); //Clear Every Focus from Keyboard
    //             PathInput.Focusable = false; //Set PathInputs Focusable ability to false
    //             Keyboard.Focus(Window); //Focus Every Object in Window (everything)
    //             PathInput.Focusable = true; //Set PathInputs Focusable ability to true
    //
    //             if (Path == "" || Path == null || Path == String.Empty) //checks for an empty path
    //             {
    //                 PathInput.Text = "Füge Pfad ein..."; //set the Path input Textboxs text
    //             }
    //         }
    //         else if (e.Key == Key.P && !PathInput.IsKeyboardFocused && !FileCreation && !FolderCreation) //check if pressed Key is P (Placheolders) and PathInput is not Keyboard Focused and no File or Folder gets Created
    //         {
    //             if (Placeholders == Brushes.Transparent) //If Placheolders beeing Invisibel
    //             {
    //                 Logging.Log("Using Shortcut P (Activate)", "MainWindow"); //C. Log Entry
    //                 Placeholders = optionalBrush; //Coloring the Placeholders
    //             }
    //             else //if Placeholder arent Invisibel
    //             {
    //                 Logging.Log("Using Shortcut P (Deactivate)", "MainWindow"); //C. Log Entry
    //                 Placeholders = Brushes.Transparent; //Make Placeholders Invisibel
    //             }
    //
    //             if (Path != null && Path != String.Empty && Path != "") //Check for an Empty Path
    //             {
    //                 LoadAllFields(Path); //call LoadALlFields M.
    //             } 
    //         }
    //         else if (e.Key == Key.U && !PathInput.IsKeyboardFocused && !FileCreation && !FolderCreation) //check if pressed Key is U (Undo) and PathInput is not Keyboard Focused and no File or Folder gets Created
    //         {
    //             Logging.Log("Using Shortcut U", "MainWindow"); //C. Log Entry
    //             UndoFunction(null, null); //call UndoFunction M.
    //         }
    //         else if (e.Key == Key.R && !PathInput.IsKeyboardFocused && !FileCreation && !FolderCreation) //check if pressed Key is R (Redo) and PathInput is not Keyboard Focused and no File or Folder gets Created
    //         {
    //             Logging.Log("Using Shortcut R", "MainWindow"); //C. Log Entry
    //             RedoFunction(null, null); //call RedoFunction M.
    //         }
    //         else if (e.Key == Key.S && !PathInput.IsKeyboardFocused && !FileCreation && !FolderCreation) //check if pressed Key is S (Settings) and PathInput is not Keyboard Focused and no File or Folder gets Created
    //         {
    //             Logging.Log("Using Shortcut S", "MainWindow"); //C. Log Entry
    //             SettingsWindow(null, null); // call SettingsWindow M.
    //         }
    //         else if (e.Key == Key.D && !PathInput.IsKeyboardFocused && !FileCreation && !FolderCreation) //check if pressed Key is D (Directorys) and PathInput is not Keyboard Focused and no File or Folder gets Created
    //         {
    //             Logging.Log("Using Shortcut D", "MainWindow"); //C. Log Entry
    //             if (DirectoryScrollActiv) //check if Directory Scrolling with Keyboard is Active
    //             {
    //                 DirectoryScrollActiv = false; //set Directory Scrolling with Keyboard to false
    //             }
    //             else if (!DirectoryScrollActiv) //check if Directory Scrolling with Keyboard is not Active
    //             {
    //                 FilesSrollActiv = false; //set File Scrolling with Keyboard to false
    //                 DirectoryScrollActiv = true; //set Directory Scrolling with Keyboard to true
    //             }
    //             LoadAllFields(Path); //call LoadAllFields M.
    //         }
    //         else if (e.Key == Key.F && !PathInput.IsKeyboardFocused && !FileCreation && !FolderCreation) //check if pressed Key is F (Files) and PathInput is not Keyboard Focused and no File or Folder gets Created
    //         {
    //             Logging.Log("Using Shortcut F", "MainWindow"); //C. Log Entry
    //             if (FilesSrollActiv) //check if File Scrolling with Keyboard is Active
    //             {
    //                 FilesSrollActiv = false; //set File Scrolling with Keyboard to false
    //             }
    //             else if (!FilesSrollActiv) //check if File Scrolling with Keyboard is not Active
    //             {
    //                 DirectoryScrollActiv = false; //set Directory Scrolling with Keyboard to false
    //                 FilesSrollActiv = true; //set File Scrolling with Keyboard to true
    //             }
    //             LoadAllFields(Path); //call LoadAllFields M.
    //         }
    //         else if (e.Key == Key.Up && !PathInput.IsKeyboardFocused && !FileCreation && !FolderCreation) //check if pressed Key is ⌃ (Scroll Up) and PathInput is not Keyboard Focused and no File or Folder gets Created
    //         {
    //             Logging.Log("Using Shortcut ⌃", "MainWindow"); //C. Log Entry
    //             if (DirectoryScrollActiv && FolderCount != Settingsfile.Fields) //check if Directory Scrolling with Keyboard is Activ and FolderCount not lower than Field Setting
    //             {
    //                 FolderCount--; //decrease the Foldercount for LoadAllFields
    //                 LoadAllFields(PathInput.Text); //start LoadAllFields M.   
    //             }
    //             else if (FilesSrollActiv && FileCount != Settingsfile.Fields) //check if File Scrolling with Keyboard is Activ and FileCount not lower than Field Setting
    //             {
    //                 FileCount--; //decrease the Foldercount for LoadAllFields
    //                 LoadAllFields(PathInput.Text); //start LoadAllFields M.   
    //             }
    //             LoadAllFields(Path); //call LoadAllFields M.
    //         }
    //         else if (e.Key == Key.Down && !PathInput.IsKeyboardFocused && !FileCreation && !FolderCreation) //check if pressed Key is ⌄ (Scroll Down) and PathInput is not Keyboard Focused and no File or Folder gets Created
    //         {
    //             Logging.Log("Using Shortcut ⌄", "MainWindow"); //C. Log Entry
    //             
    //             if (DirectoryScrollActiv) //check if Directory Scrolling with Keyboard is Activ 
    //             {
    //                 FolderCount++; //Increase  the Foldercount for LoadAllFields
    //                 LoadAllFields(PathInput.Text); //start LoadAllFields M.
    //             }
    //             else if (FilesSrollActiv) //check if File Scrolling with Keyboard is Activ
    //             {
    //                 FileCount++; //Increase  the Foldercount for LoadAllFields
    //                 LoadAllFields(PathInput.Text); //start LoadAllFields M.
    //             }
    //             LoadAllFields(Path); //call LoadAllFields M.
    //         }
    //         else if (e.Key == Key.Enter) //check if pressed Key is Enter
    //         {
    //             Logging.Log("Using Shortcut Enter", "MainWindow"); //C. Log Entry
    //             if (FileCreation) //if FileCreation is true
    //             {
    //                 CreateingFile(null, null); //call CreateingFile M. for final Creation
    //             }
    //             else if (FolderCreation) //if FolderCreation is True
    //             {
    //                 CreateingFolder(null, null); //call CreateingFolder M. for final Creation
    //             }
    //             else if (!PathInput.IsKeyboardFocused) //check if PathInput is not Keyboard Focused
    //             {
    //                 if (DirectoryScrollActiv) //check if Directory Scrolling with Keyboard is Activ
    //                 {
    //                     Folder_Click(DirectoryList[0], null); //call Folder_Click M. for the first Directory Textblock
    //                 }
    //                 else if (FilesSrollActiv) //check if File Scrolling with Keyboard is Activ
    //                 {
    //                     OpenFile(fileList[0], null); //call OpenFile M. for the first File Textblock
    //                 }
    //             }
    //             else if (PathInput.IsKeyboardFocused) //check if PathInput is Keyboard Focused
    //             {
    //                 Logging.Log("Enter Input", "MainWindow"); //C. Log Entry
    //                 setPath(PathInput.Text); //set Path to Path from textbox
    //                 LoadAllFields(Path); //start the LoadAllFields M.
    //                 Redostring = Path;//Add the Current Path to the Redostring
    //                 Keyboard.ClearFocus(); //Clear Every Focus from Keyboard
    //                 PathInput.Focusable = false; //Set PathInputs Focusable ability to false
    //                 Keyboard.Focus(Window); //Focus Every Object in Window (everything)
    //                 PathInput.Focusable = true; //Set PathInputs Focusable ability to true
    //             }
    //         }
    //     }
    //     
    //     private void MouseShortCuts(object sender, MouseButtonEventArgs e) //M. for Mouseshortcuts
    //     {
    //         if (e.ChangedButton == MouseButton.Left && PathInput.IsKeyboardFocused && !FileCreation && !FolderCreation) //Check if Pressed Mousebutton is Leftclick and PathInput is not Keyboard Focused and no File or Folder gets Created
    //         {
    //             Logging.Log("Using Shortcut LeftClick", "MainWindow"); //C. Log Entry
    //             Keyboard.ClearFocus(); //Clear Every Focus from Keyboard
    //             PathInput.Focusable = false; //Set PathInputs Focusable ability to false
    //             Keyboard.Focus(Window); //Focus Every Object in Window (everything)
    //             PathInput.Focusable = true; //Set PathInputs Focusable ability to true
    //             
    //             if (Path == "" || Path == null || Path == String.Empty) //checks for an empty path
    //             {
    //                 PathInput.Text = "Füge Pfad ein..."; //set the Path input Textboxs text
    //             }
    //             RemoveMenus(null, null); //call RemoveMenus M.
    //         }
    //         e.Handled = true; //set Keydownevent to complet
    //     }
    //
    //     private void RemoveMenus(object sender, MouseButtonEventArgs e) //M. to delete ActiveMenus
    //     {
    //         try //prevent Crashes
    //         {
    //             Logging.Log("RemoveMenus M.", "MainWindow"); //C. Log Entry
    //             if (e == null || e.ChangedButton != MouseButton.Right) //check if e is null or if e is not an Rightclick
    //             {
    //                 if (ActiveMenus.Count > 0 ) //check if ActiveMenus exists
    //                 {
    //                     Canvas.Children.Remove(ActiveMenus[0]); //Removes Active Menu from Grid
    //                     ActiveMenus.RemoveAt(0); //removes Active Menu from the list
    //                     LoadAllFields(Path); //call LoadAllFields M. to remove the marked Fields mark
    //                 }
    //         
    //                 if (FileCreation) //if FileCreation is true
    //                 {
    //                     Canvas.Children.Remove(InputName); //Remove InputName as Child of Grid
    //                     FileCreation = false; //Set FileCreation to False
    //                 }
    //                 else if (FolderCreation) //if FolderCreation is True
    //                 {
    //                     Canvas.Children.Remove(InputName);
    //                     FolderCreation = false; //Set FolderCreation to False
    //                 } 
    //             }
    //         }
    //         catch (Exception exception) //Catch Crash
    //         {
    //             Logging.CatchLog("MouseButton was null", "MainWindow"); //C. Catch Log Entry
    //         }
    //             
    //     }
    //     private void DirectoryScrollingWithMouse(object sender, MouseWheelEventArgs e) //M. to react to scrolling
    //     {
    //         if (e.Delta < 0) //check in which direction got scrolled
    //         {
    //             Logging.Log("Directory Scrolling (+)", "MainWindow"); //C. Log Entry
    //             if (Directory.Exists(PathInput.Text)) //check if Directory Exists
    //             {
    //                 FolderCount++; //Increase  the Foldercount for LoadAllFields
    //                 LoadAllFields(PathInput.Text); //start LoadAllFields M.
    //             }
    //             else
    //             {
    //             }
    //         }
    //         else if (e.Delta > 0) //check if the other direction was it
    //         {
    //             Logging.Log("Directory Scrolling (-)", "MainWindow"); //C. Log Entry
    //             if (FolderCount != Settingsfile.Fields) //check if FolderCount goes to low
    //             {
    //                 FolderCount--; //decrease the Foldercount for LoadAllFields
    //                 LoadAllFields(PathInput.Text); //start LoadAllFields M.   
    //             }
    //             else
    //             {
    //             }
    //         }
    //     }
    //
    //     private void FileScrollingWithMouse(object sender, MouseWheelEventArgs e) //M. to react to scrolling
    //     {
    //         if (e.Delta < 0) //check in which direction got scrolled
    //         {
    //             Logging.Log("File Scrolling (+)", "MainWindow"); //C. Log Entry
    //             if (Directory.Exists(PathInput.Text)) //check if Directory Exists
    //             {
    //                 FileCount++; //increase Filecount for LoadAllFields M.
    //                 LoadAllFields(PathInput.Text); //start LoadAllFields M.
    //             }
    //             else
    //             {
    //             }
    //         }
    //         else if (e.Delta > 0) //check if the other direction was it
    //         {
    //             Logging.Log("File Scrolling (-)", "MainWindow"); //C. Log Entry
    //             if (FileCount != Settingsfile.Fields) //check if FolderCount goes to low
    //             {
    //                 FileCount--; //decrease the Filecount for the LoadAllFields M.
    //                 LoadAllFields(PathInput.Text); //start LoadAllFields M.
    //             }
    //             else
    //             {
    //             }
    //         }
    //     }
    //     
    //     private void Folder_Click(object sender, RoutedEventArgs e) //M. to react on Folderclick
    //     {
    //         var textBlock = sender as System.Windows.Controls.TextBlock; //C. an V. for the TextBlock
    //         if (textBlock.Text != "" && textBlock.Text != null) //check if textblocks text is empty or null
    //         {
    //             Logging.Log("Select new Underfolder", "MainWindow"); //C. Log Entry
    //             PathInput.Text = Path + textBlock.Text; //set new text for the path input textbox
    //             LastPath = textBlock.Text; //set new lastpath
    //             FolderCount = Fields; //reset the Foldercount
    //             FileCount = Fields; //reset the Filecount
    //             LoadAllFields(Path + textBlock.Text); //start LoadAllFields M.
    //             Redostring = Path; //Add the Current Path to the Redostring
    //             RemoveMenus(null, null); //call RemoveMenus M.
    //         }
    //         else
    //         {
    //             Logging.CatchLog("Underfolder cant be Selected (Empty)", "MainWindow"); //C. Log Entry
    //         }
    //     }
    //     
    //     private void OpenFile(object sender, MouseButtonEventArgs e) //Open File on Click event
    //     {
    //         var filePath = sender as TextBlock; //C. V. for the TextBlock
    //
    //         try //try to prevent Crashes (if file can´t be opened)
    //         {
    //             Logging.Log("Open File", "MainWindow"); //C. Log Entry
    //             if (filePath != null && (filePath.Text != null && filePath.Text != "")) //check if filepath is not null and Text also not null and not empty
    //             {
    //                 if (filePath.Text != null) Process.Start(Path + @"\" + filePath.Text); //Start the Selected File 
    //             }
    //         }
    //         catch (Exception exception) //used for logs
    //         {
    //             Logging.CatchLog(Convert.ToString(e), "MainWindow"); //use CatchLog M.
    //         }
    //         RemoveMenus(null, null); //call RemoveMenus M.
    //     }
    //
    //     private void Deleter(object sender, MouseButtonEventArgs e) //Delete File or Directory
    //     {
    //         Logging.Log("Delete Started", "MainWindow"); //C. Log Entry
    //         TextBlock RemoveObject = sender as TextBlock; //use Sender as TextBlock
    //         if (Directory.Exists(Path + @"\" + RemoveObject.Text)) //check if sender Field is referenz to a Directory
    //         {
    //             Logging.Log("Delete Folder", "MainWindow"); //C. Log Entry
    //             Directory.Delete(Path + @"\" + RemoveObject.Text, true); //Delete the referenz Directory
    //         }
    //         else if (File.Exists(Path + @"\" + RemoveObject.Text)) //check if sender Field is referenz to a File
    //         {
    //             Logging.Log("Delete File", "MainWindow"); //C. Log Entry
    //             File.Delete(Path + @"\" + RemoveObject.Text); //Delete the referenz File
    //         }
    //         LoadAllFields(Path); //call LoadAllFields M.
    //         RemoveMenus(null, null); //call RemoveMenus M.
    //     }
    //         
    //         
    //     private void Border_MouseEnter(object sender, MouseEventArgs mouseEventArgs) //M. to make an event if an Refered Object got an MousEnter event
    //     {
    //         if (sender is Border) //check the Type of Sender
    //         {
    //             var border = sender as Border; //C. V. for sender (Border)
    //             if (border != null)
    //             {
    //                 border.Background = optionalBrush; //change the color of the Xc. V.
    //                 border.Height = 32; //change the size of the Xc. V.
    //                 border.Width = 32; //change the size of the Xc. V.
    //             }
    //         }
    //         else if (sender is TextBlock) //check the Type of Sender
    //         {
    //             var TextBlock = sender as TextBlock; //C. V. for sender (Textblock)
    //             if (TextBlock.Text != null && TextBlock.Text != "") //check if Textblocks text is not empty
    //             {
    //                 TextBlock.Foreground = optionalBrush; //change the Font color of the Xc. V.
    //             }
    //         }
    //         else if (sender is TextBox) //check the Type of Sender
    //         {
    //             if (Equals(sender, PathInput)) //check if sender and PathInput are the same
    //             {
    //                 if (Path == "" || Path == null) //check for an Empty Path
    //                 {
    //                     PathInput.Text = String.Empty; //clear the Path input textboxs text
    //                 }
    //             }
    //         }
    //     }
    //
    //     private void Border_MouseLeave(object sender, MouseEventArgs e) //M. to make an event if an Refered Object got an MousLeave event
    //     {
    //         if (sender is Border) //check the Type of Sender
    //         {
    //             var border = sender as Border; //C. V. for sender (Border)
    //             if (border != null)
    //             {
    //                 border.Background = foregroundBrush; //change the color of the Xc. V.
    //                 border.Height = 30; //change the size of the Xc. V.
    //                 border.Width = 30; //change the size of the Xc. V.
    //             }
    //         }
    //         else if (sender is TextBlock) //check the Type of Sender
    //         {
    //             var TextBlock = sender as TextBlock; //C. V. for sender (Textblock)
    //             if (TextBlock.Text != null && TextBlock.Text != "") //check if Textblocks text is not empty
    //             {
    //                 TextBlock.Foreground = foregroundBrush; //change the Font color of the Xc. V.
    //             }
    //         }
    //         else if (sender is TextBox) //check the Type of Sender
    //         {
    //             if (Equals(sender, PathInput) && !PathInput.IsKeyboardFocused) //check if sender and PathInput are the same
    //             {
    //                 if (Path == "" || Path == null) //Check for an Empty Path
    //                 {
    //                     PathInput.Text = "Füge Pfad ein..."; //set the Path input Textboxs text
    //                 }
    //             }
    //         }
    //     }
    //     
    //     private void SettingsWindow(object sender, MouseButtonEventArgs e) //Open Settings Window
    //     {
    //         Logging.Log("Open Settings", "MainWindow"); //C. Log Entry
    //         Phexor.SettingsWindow settings = new SettingsWindow(); //C. new Settings window
    //         settings.Show(); //show the Created Settings window
    //         NotTrueClose = true; //prevent an close of the program
    //         this.Close(); // close this window
    //     }
    //
    //     private void UndoFunction(object sender, MouseButtonEventArgs mouseButtonEventArgs) //Undo Path Change
    //     {
    //         if (Path != null && Path != String.Empty && Path != "") //Check for Path emptynes
    //         {
    //             Logging.Log("Undo M.", "MainWindow"); //C. Log Entry
    //             List<string> Pathparts = new List<string>(Path.Split('\\')); //Split the Current Path
    //             Pathparts.RemoveAll(s => s == ""); //Remove every empty Path part
    //             var pathPartCount = Pathparts.Count(); // get the Count of the remaining Path parts
    //             var UndoPath = ""; //C. V. for the new Undo path
    //             for (int i = 0; i < pathPartCount - 1; i++) // loop for every existing path part
    //             {
    //                 UndoPath = UndoPath + Pathparts[i] + @"\"; //C. the new Path from the parts
    //             }
    //             setPath(UndoPath); // set created Path
    //             LoadAllFields(Path); //use M. LoadAllFields with the new Path
    //             PathInput.Text = Path; //change the Pathinput text to the new Path
    //         }
    //         else //use for logs
    //         {
    //             Logging.CatchLog("Cant Undo (Path Empty)", "MainWindow"); //C. Log Entry
    //         }
    //     }
    //
    //     private void RedoFunction(object sender, MouseButtonEventArgs mouseButtonEventArgs) //Redo Path change
    //     {
    //         if (Redostring != null && Redostring != "" && Redostring != Path) //checks if Redostring is Empty and not the same as Path
    //         {
    //             Logging.Log("Redo M.", "MainWindow"); //C. Log Entry
    //             List<string> setPathParts = new List<string>(Path.Split('\\')); //Split the Path into path parts
    //             setPathParts.RemoveAll(s => s == ""); //Remove every Empty Path part
    //             
    //             List<string> setRedoStringParts = new List<string>(Redostring.Split('\\')); //Split the Redostring into Redostring parts
    //             setRedoStringParts.RemoveAll(s => s == ""); //Remove every Empty Redostring part
    //
    //             if (setRedoStringParts.Count > setPathParts.Count) //check if after Removing empty parts Redostring still is higher
    //             {
    //                 int RedoPartsCount = setPathParts.Count + 1; //C. V. RedoPartsCount to check how much Redoparts need to form the new Path
    //                 Path = ""; //Remove the Existing Path
    //                 foreach (var RedoStringPart in setRedoStringParts) //Loop for every RedoString
    //                 {
    //                     if (RedoPartsCount > 0) //prevent to much Parts geting added
    //                     {
    //                         Path = Path + RedoStringPart + @"\"; //Adding Part to the Path
    //                     }
    //                     RedoPartsCount--; //decrease the RedoPartsCount
    //                 }
    //                 LoadAllFields(Path); //use M. LoadAllFields with the new Path
    //                 PathInput.Text = Path; //Set the Pathinput textbox content to the new Path
    //             }
    //         }
    //         else //use for Logs
    //         {
    //             Logging.CatchLog("Cant Redo (Nothing to Redo)", "MainWindow"); //C. Log Entry
    //         }
    //     }
    //     
    //     private void setPath(string Input) //Path setting M.
    //     {
    //         Logging.Log("Set new Path", "MainWindow"); //C. Log Entry
    //         List<string> setPathParts = new List<string>(Input.Split('\\')); //Split the Input into path parts
    //         setPathParts.RemoveAll(s => s == ""); //removing every empty path part
    //         Path = ""; //set parth to empty
    //         foreach (var setPathPart in setPathParts) //loop for every path part
    //         {
    //             Path = Path + setPathPart + @"\"; //C. new Path
    //         }
    //     }
    //
    //     private void LoadAllFields(string myPath) //Set Folder/File-Fields Content
    //     {
    //         if (ActiveMenus.Count == 0)
    //         {
    //             Logging.Log("Load new Path", "MainWindow"); //C. Log Entry
    //         
    //             for (int i = 0; i < Fields; i++) //Reset the Input and Color of every Field 
    //             {
    //                 DirectoryList[i].Text = null; //Reset text of Directory Textblock
    //                 DirectoryList[i].Foreground = foregroundBrush; //Reset Foregroundbrush of Directory Textblock
    //                 fileList[i].Text = null; //Reset text of File Textblock
    //                 fileList[i].Foreground = foregroundBrush; //Reset Foregroundbrush of File Textblock
    //                 fileImageList[i].Source = null; //clear the fileImage Image-source
    //                 PlaceholderDirectoryList[i].Background = Brushes.Transparent; //Clear the Placeholders Visbility (with Background Color)
    //                 PlaceholderfileList[i].Background = Brushes.Transparent; //Clear the Placeholders Visbility (with Background Color)
    //             }
    //             
    //             if (Directory.Exists(myPath) || File.Exists(myPath)) //check for an not Existing Path
    //             {
    //                 string directoryPath = myPath + @"\"; //set the directorypath to the M. input
    //                 setPath(directoryPath); //set the path to the directorypath
    //                 int directoryPathLength = directoryPath.Length; //C. V. for the length of the Path
    //
    //                 var directoryCount = 0; //C. V. for the directorys
    //                 var DirectorysDisplayed = 0; //C. V. for the directorys
    //                 var DirectorysNotDisplayed = 0; //C. V. for the directorys
    //             
    //                 var fileCount = 0; //C. V. for the files
    //                 var FilesDisplayed = 0; //C. V. for the files
    //                 var FilesNotDisplayed = 0; //C. V. for the files
    //                 
    //                 try //prevent crashes
    //                 {
    //                     var directors = Directory.GetDirectories(directoryPath); //Adding Every Directory to the directory L.
    //                     if (directors != null) //check if directorys are empty
    //                     {
    //                         try //prevent crashes
    //                         {
    //                             foreach (var directory in directors) //loop for each directory in the path
    //                             {
    //                                 for (int i = 0; i < Fields; i++) //loop for the Amount of Fields
    //                                 {
    //                                     if (directoryCount < DirectorysNotDisplayed + FolderCount - Settingsfile.Fields) //prove the position for this text removing 
    //                                     {
    //                                         DirectoryList[DirectorysNotDisplayed].Text = null; //Removing the text from the Textblock
    //                                         PlaceholderDirectoryList[DirectorysNotDisplayed].Background = Brushes.Transparent; //Clear the Placeholders Visbility (with Background Color)
    //                                     }
    //
    //                                     DirectorysNotDisplayed++; //increase DirectorysNotDisplayed count
    //                                 }
    //
    //                                 DirectorysNotDisplayed = 0; //reset DirectorysNotDisplayed count
    //
    //                                 if (directoryCount == DirectorysDisplayed + FolderCount - Settingsfile.Fields) //check for the position of the Textblock
    //                                 {
    //                                     DirectoryList[DirectorysDisplayed].Text = directory.Substring(directoryPathLength); //setting the Textblocks Text to the directory name (just name, without Path)
    //                                     PlaceholderDirectoryList[DirectorysDisplayed].Background = Placeholders; //Set the Placeholders Visibility (with background Color)
    //                                     DirectorysDisplayed++; //increase DirectorysDisplayed count
    //                                 }
    //
    //                                 directoryCount++; //increase directorycount
    //                             }
    //                         }
    //                         catch (Exception e) //used for logs
    //                         {
    //                             Logging.CatchLog(Convert.ToString(e), "Mainwindow"); //use CatchLog M.
    //                         }
    //                     }
    //                     else
    //                     {
    //                         for (int i = 0; i < Fields; i++)
    //                         {
    //                             DirectoryList[i].Text = null; //Clear the Text from every TextBlock
    //                             PlaceholderDirectoryList[i].Background = Brushes.Transparent; //Clear the Placeholders Visbility (with Background Color)
    //                         }
    //                     }
    //                     
    //                     var files = Directory.GetFiles(directoryPath); //get every File from the Path
    //                     if (files != null) //check if Files L. is empty
    //                     {
    //                         try //prevent Crashes
    //                         {
    //                             foreach (var file in files) // Loop for every file 
    //                             {
    //                                 for (int i = 0; i < Fields; i++) //loop for the maximum amount of settet Fields
    //                                 {
    //                                     if (fileCount < FilesNotDisplayed + FileCount - Settingsfile.Fields) //prove the position for this text removing 
    //                                     {
    //                                         fileList[FilesNotDisplayed].Text = null; //Set Text of Textblock to null
    //                                         PlaceholderfileList[FilesNotDisplayed].Background = Brushes.Transparent; //Clear the Placeholders Visbility (with Background Color)
    //                                     }
    //
    //                                     FilesNotDisplayed++; //Increase FilesNotDisplayed count
    //                                 }
    //
    //                                 FilesNotDisplayed = 0; //reset FilesNotDisplayed Count
    //
    //                                 if (fileCount == FilesDisplayed + FileCount - Settingsfile.Fields) //prove the position for this Text
    //                                 {
    //                                     fileList[FilesDisplayed].Text = file.Substring(directoryPathLength); //Set the Text of the Textblocks (just the Filename, not Path)
    //                                     PlaceholderfileList[FilesDisplayed].Background = Placeholders; //Set the Placeholders Visibility (with background Color)
    //                                     if (file.EndsWith(".txt") || file.EndsWith(".doc") || file.EndsWith(".docx") || file.EndsWith("odt") || file.EndsWith(".rtf")) //Check if file is text-file
    //                                     {
    //                                         fileImageList[FilesDisplayed].Source = new BitmapImage(new Uri("Grafiks/TXT.png", UriKind. Relative)); //Set matching File-Image
    //                                     }
    //                                     else if (file.EndsWith(".xls") || file.EndsWith(".xlsx") || file.EndsWith(".ods") || file.EndsWith(".csv")) //Check if file is an Excel-file or similiar
    //                                     {
    //                                         fileImageList[FilesDisplayed].Source = new BitmapImage(new Uri("Grafiks/XLS.png", UriKind. Relative)); //Set matching File-Image
    //                                     }
    //                                     else if (file.EndsWith(".ppt") || file.EndsWith(".pptx") || file.EndsWith(".odp")) //Check if file is an Presentation-file
    //                                     {
    //                                         fileImageList[FilesDisplayed].Source = new BitmapImage(new Uri("Grafiks/PPTX.png", UriKind. Relative)); //Set matching File-Image
    //                                     }
    //                                     else if (file.EndsWith(".pdf")) //Check if file is an PDF-file
    //                                     {
    //                                         fileImageList[FilesDisplayed].Source = new BitmapImage(new Uri("Grafiks/PDF.png", UriKind. Relative)); //Set matching File-Image
    //                                     }
    //                                     FilesDisplayed++; //incrase FilesDisplayed Count
    //                                 }
    //
    //                                 fileCount++; //increase filecount
    //                             }
    //                         }
    //                         catch (Exception e) //used for logs
    //                         { 
    //                             Logging.CatchLog(Convert.ToString(e), "Mainwindow"); //use CatchLog M.
    //                         }
    //                     }
    //                     else //clearing the file L.
    //                     {
    //                         for (int i = 0; i < Fields; i++)
    //                         {
    //                             fileList[i].Text = null; //Clear the Text from every TextBlock
    //                             PlaceholderfileList[i].Background = Brushes.Transparent; //Clear the Placeholders Visbility (with Background Color)
    //                         }
    //                     }
    //                     
    //                 }
    //                 catch (Exception e) //clearing all th Fields and Write a Log
    //                 {
    //                     Logging.CatchLog(Convert.ToString(e), "Mainwindow"); //use CatchLog M.
    //                 }
    //             }
    //             else
    //             {
    //                 Logging.CatchLog("Path Not Exists", "Mainwindow"); //use CatchLog M.
    //             }
    //
    //             if (DirectoryScrollActiv) //check if Directory Scrolling with Keyboard is Active
    //             {
    //                 DirectoryList[0].Foreground = optionalBrush; //Set the brush for First Directory Textblock to Optional brush
    //             }
    //             else if (FilesSrollActiv) //check if File Scrolling with Keyboard is Active
    //             {
    //                 fileList[0].Foreground = optionalBrush; //Set the brush for First File Textblock to Optional brush
    //             }
    //         }
    //     }
    //     
    //     private void Folder_RightClick(object sender, MouseButtonEventArgs e) //M. to create an Folder Option Menu
    //     {
    //         Logging.Log("Open Folder-Settings-Menu", "MainWindow"); //C. Log Entry
    //         RemoveMenus(null, null); //call RemoveMenus M.
    //         TextBlock Field = sender as TextBlock; //C. V. for sender Textblock
    //         Field.Foreground = optionalBrush; //marking Textblocks color
    //         StackPanel FolderMenu = new StackPanel(); //Create FolderMenu Stackpanel
    //         FolderMenu.MouseEnter += (s, args) => Border_MouseEnter(sender, e); //Call on Entering Button the MouseEnter M.
    //         FolderMenu.MouseLeave += (s, args) => Border_MouseLeave(sender, e); //Call on Leaving Button the MouseLeave M.
    //
    //         //Start to Create all Buttons
    //         if (Field.Text != "" && Field.Text != null) //checks if TextBlock is not Empty
    //         {
    //             Button Open = new Button(); //C. Button for Stackpanel
    //             ButtonDesigner(Open, "Open"); //Set Buttons Settings and Text
    //             Open.Click += (s, args) => Folder_Click(sender, e); //Call on Mouse the Click Folder_Click M.
    //             FolderMenu.Children.Add(Open); //Add Button to Stackpanel
    //
    //             Button Delete = new Button(); //C. Button for Stackpanel
    //             ButtonDesigner(Delete, "Delete"); //Set Buttons Settings and Text
    //             Delete.Click += (s, args) => Deleter(sender, e); //Call on Mouse Click the Deleter M.
    //             FolderMenu.Children.Add(Delete); //Add Button to Stackpanel
    //         }
    //         else if (Path != null && Path != "") //if TextBlock is Empty and Path not Empty
    //         {
    //             Button Create = new Button(); //C. Button for Stackpanel
    //             ButtonDesigner(Create, "Create"); //Set Buttons Settings and Text
    //             Create.Click += (sender, args) => AskFolderName(sender, e); //call on Mouse click AskFolderName M.
    //             FolderMenu.Children.Add(Create); //Add Button to Stackpanel
    //         }
    //         
    //         //Finish the Process
    //         Point clickPosition = e.GetPosition(Canvas); //Get Position of Mouse
    //         FolderMenu.Height = Directorys.Height / Settingsfile.Fields * FolderMenu.Children.Count; //Set Stackpanels Height
    //         FolderMenu.Width = 100; //Set Stackpanels Width
    //         FolderMenu.Margin = new Thickness(clickPosition.X, clickPosition.Y, 5, 5); //Set Stackpanels Position
    //         ActiveMenus.Add(FolderMenu); //Add Stackpanel to ActiveMenus list
    //         Canvas.Children.Add(FolderMenu); //Add Stackpanel to Grid
    //     }
    //
    //     private void File_RightClick(object sender, MouseButtonEventArgs e) //M. to Create an File Option Menu
    //     {
    //         Logging.Log("Open File-Settings-Menu", "MainWindow"); //C. Log Entry
    //         RemoveMenus(null, null); //call RemoveMenus M.
    //         TextBlock Field = sender as TextBlock; //C. V. for sender Textblock
    //         Field.Foreground = optionalBrush; //marking TextBlocks Color
    //         StackPanel FileMenu = new StackPanel(); //Create FileMenu Stackpanel
    //         FileMenu.MouseEnter += (s, args) => Border_MouseEnter(sender, e); //Call on Entering Button the MouseEnter M.
    //         FileMenu.MouseLeave += (s, args) => Border_MouseLeave(sender, e); //Call on Leaving Button the MouseLeave M.
    //         
    //         //Start to Create all Buttons
    //         if (Field.Text != "" && Field.Text != null) //checks if TextBlock is not Empty
    //         {
    //             Button Open = new Button(); //C. Button for Stackpanel
    //             ButtonDesigner(Open, "Open"); //Set Buttons Settings and Text
    //             Open.Click += (s, args) => OpenFile(sender, e); //Call on Mouse Click the OpenFile M.
    //             FileMenu.Children.Add(Open); //Add Button to Stackpanel
    //         
    //             Button Delete = new Button(); //C. Button for Stackpanel
    //             ButtonDesigner(Delete, "Delete"); //Set Buttons Settings and Text
    //             Delete.Click += (s, args) => Deleter(sender, e); //Call on Mouse Click the Deleter M.
    //             FileMenu.Children.Add(Delete); //Add Button to Stackpanel
    //         }
    //         else if (Path != null && Path != "") //If TextBlock is empty and Path is not Empty
    //         {
    //             Button Create = new Button(); //C. Button for Stackpanel
    //             ButtonDesigner(Create, "Create"); //Set Buttons Settings and Text
    //             Create.Click += (sender, args) => AskFileName(sender, e); //call on Mouse click AskFileName M.
    //             FileMenu.Children.Add(Create); //Add Button to Stackpanel
    //         }
    //         
    //         //Finish the Process
    //         Point clickPosition = e.GetPosition(Canvas); //Get Position of Mouse
    //         FileMenu.Height = Files.Height/Settingsfile.Fields * FileMenu.Children.Count; //Set Stackpanels Height
    //         FileMenu.Width = 100; //Set Stackpanels Width
    //         FileMenu.Margin = new Thickness(clickPosition.X, clickPosition.Y, 5, 5); //Set Stackpanels Position
    //         ActiveMenus.Add(FileMenu); //Add Stackpanel to ActiveMenus list
    //         Canvas.Children.Add(FileMenu); //Add Stackpanel to Grid
    //     }
    //
    //     private void AskFileName(object sender, MouseButtonEventArgs e) //M. to Ask for a File name to Create
    //     {
    //         Logging.Log("Create File-Name Field", "MainWindow"); //C. Log Entry
    //         if (Path != null && Path != "") //if Path is not Empty
    //         {
    //             if (!FileCreation) //if FileCreation is false
    //             {
    //                 InputNameText.Text = "Füge Namen Ein"; //Set InputNameTexts Text
    //         
    //                 Point clickPosition = e.GetPosition(Canvas); //Get Position of Mouse
    //                 Canvas.Children.Add(InputName); //Add InputName to the Grid
    //                 InputName.Margin = new Thickness(clickPosition.X, clickPosition.Y, 5, 5); //set InptName to Mouse Positon
    //                 FileCreation = true; //Set FileCreation to True
    //             }
    //         }
    //     }
    //
    //     private void CreateingFile(object sender, MouseButtonEventArgs e) //M. to C. an File
    //     {
    //         Logging.Log("Create File", "MainWindow"); //C. Log Entry
    //         RemoveMenus(null, null); //call RemoveMenus M.
    //         File.Create(Path + @"\" + InputNameText.Text); //Create File
    //         LoadAllFields(Path); //cal LoadAllFields M.
    //     }
    //     
    //     private void AskFolderName(object sender, MouseButtonEventArgs e) //M. to Ask for a Folder name to Create
    //     {
    //         Logging.Log("Create Folder-Name Field", "MainWindow"); //C. Log Entry
    //         if (Path != null && Path != "") //if Path is not Empty
    //         {
    //             if (!FolderCreation) //if FolderCreation is false
    //             {
    //                 InputNameText.Text = "Füge Namen Ein"; //Set InputNameTexts Text
    //         
    //                 Point clickPosition = e.GetPosition(Canvas); //Get Position of Mouse
    //                 Canvas.Children.Add(InputName); //Add InputName to the Grid
    //                 InputName.Margin = new Thickness(clickPosition.X, clickPosition.Y, 5, 5); //set InptName to Mouse Positon
    //                 FolderCreation = true; //Set FolderCreation to True
    //             }
    //         }
    //     }
    //
    //     private void CreateingFolder(object sender, MouseButtonEventArgs e) //M. to C. an Folder
    //     {
    //         Logging.Log("Create Folder", "MainWindow"); //C. Log Entry
    //         RemoveMenus(null, null); //call RemoveMenus M.
    //         Directory.CreateDirectory(Path + @"\" + InputNameText.Text); //Create Folder
    //         LoadAllFields(Path); //cal LoadAllFields M.
    //     }
    //     
    //     private void ButtonDesigner(Button Name, string Content) //M. To Create Buttons for Option Menus
    //     {
    //         Logging.Log("Create Button", "MainWindow"); //C. Log Entry
    //         Name.Height = Directorys.Height/Settingsfile.Fields; //Set Height of Button
    //         Name.Foreground = backgroundBrush; //set Foreground of Button
    //         Name.BorderBrush = backgroundBrush; //Set BorderBrush of Button
    //         Name.BorderThickness = new Thickness(2);
    //         Name.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#263238")); //Set Background of Button
    //         Name.Content = Content; //Set Buttons Content
    //         Name.FontSize = 8; //Set Buttons Name
    //     }
    //     private void OnClose(object sender, EventArgs e) // Stop Running Prozesees
    //     {
    //         Logging.Log("Close Started", "MainWindow"); //C. Log Entry
    //         if (!NotTrueClose) //check for V. to know what kind of close
    //         {
    //             var phexorProcesses = Process.GetProcessesByName("Phexor"); //check for every active process
    //             foreach (var process in phexorProcesses) 
    //             {
    //                 try //prevent crashes
    //                 {
    //                     process.Kill(); //"Kill" every running process
    //                     process.WaitForExit(); //Wait until it got definitly closed
    //                 }
    //                 catch (Exception) //for Logs used
    //                 {
    //                     Logging.CatchLog(Convert.ToString(e), "Mainwindow"); //use CatchLog M.
    //                 }
    //             } 
    //         }
    //         else
    //         {
    //             NotTrueClose = false; //set the V. again to false
    //         }
    //     }
    // }
    }
}
