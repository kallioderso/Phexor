using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Phexor.Scripts;
using static System.Diagnostics.Process;
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

    public partial class Explorer
    {
        //-----Main Function-----\\
        public Explorer()
        {
            InitializeComponent();
            Initialize(); //C#c. to initialize the explorer
            this.KeyDown += (sender, e) => ShortCuts.KeyPress(e, this); //C#c. to call the KeyPress method
            this.MouseDown += (sender, e) => ShortCuts.Clicked(this); //C#c. to call the Clicked method
        }

        //-----Public Methods-----\\
        public void Initialize() => Colorize(); //M. to initialize the explorer
        public void AddDirectory(TextBlock directory) => AddingDirectory(directory); //M. to add a directory
        public void AddFile(TextBlock file) => AddingFile(file); //M. to add a file
        public void InputFieldPath() => PathInputEnter(); //M. to call the PathInputEnter method
        public void Settings() => OpenSettings(null, null); //M. to call the OpenSettings method
        public void UndoPath() => Undo(null, null); //M. to call the Undo method
        public void RedoPath() => Redo(null, null); //M. to call the Redo method
        public void CloseFieldPopup() => CloseFieldPopup(null, null); //M. to call the CloseFieldPopup method
        
        //-----Private Methods-----\\
        private void Colorize() //M. to colorize the explorer
        {
            Logging.Log("Colorize", "Explorer", false); //log the colorize method
            SettingsControl.GetSettings(); //get the settings
            Headbar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color1)!); // set the headbar color
            ButtonField.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color2)!); // set the button field color
            DirectorysPanel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color3)!); // set the directory panel color
            FilesPanel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color4)!); // set the file panel color
        }
        private void DirectoryScrollingWithMouse(object sender, MouseWheelEventArgs e) //M. to scroll through the directories
        {
            Logging.Log("DirectoryScrolling", "Explorer", false); //log the directory scrolling method
            if (e.Delta < 0) { PathSearcher.DirectoryRemoveCount++; PathSearcher.SearchPath(this); } // check if scrolled Down and increment the directory remove count
            else if (e.Delta > 0) // check if scrolled Up
            { if (PathSearcher.DirectoryRemoveCount != 0) { PathSearcher.DirectoryRemoveCount--; PathSearcher.SearchPath(this); } } // decrement the directory remove count if not zero
        }
        private void FileScrollingWithMouse(object sender, MouseWheelEventArgs e) //M. to scroll through the files
        {
            Logging.Log("FileScrolling", "Explorer", false); //log the file scrolling method
            if (e.Delta < 0) { PathSearcher.FileRemoveCount++; PathSearcher.SearchPath(this); } // check if scrolled Down and increment the file remove count
            else if (e.Delta > 0) // check if scrolled Up
            { if (PathSearcher.FileRemoveCount != 0) { PathSearcher.FileRemoveCount--; PathSearcher.SearchPath(this); } }  // decrement the file remove count if not zero
        }
        private void AddingDirectory(TextBlock directory) //M. to add a directory Object
        {
            directory.MouseLeftButtonDown += OpenPath; directory.MouseRightButtonDown += OpenFieldPopup; //Add event handlers for the directory
            Directorys.Children.Add(directory); //Add the directory to the Directorys panel
        }
        private void AddingFile(TextBlock file) //M. to add a file Object
        {
            file.MouseLeftButtonDown += OpenFile; file.MouseRightButtonDown += OpenFieldPopup; //Add event handlers for the file
            Files.Children.Add(file); //Add the file to the Files panel
        }
        private void PathInputEnter() //M. to enter a path from the PathInput Field
        {
            Logging.Log("StartPathSearch", "Explorer", false); //log the path search method
            PathSearcher.Path = PathInput.Text; PathSearcher.SavePath = PathInput.Text; PathSearcher.DirectoryRemoveCount = 0; PathSearcher.FileRemoveCount = 0; PathSearcher.SearchPath(this); //Search for the path
        }
        private void OpenSettings(object sender, RoutedEventArgs e) //M. to open the settings window
        {
            Logging.Log("OpenSettings", "Explorer", false); //log the settings method
            var settings = new Settings(this); settings.Show(); this.Hide(); //Create a new settings window and show it
        }
        private void OpenPath(object sender, MouseButtonEventArgs e) //M. to open a path
        {
            try //Prevent Axidentally Crash
            {
                Logging.Log("OpenDirectory", "Explorer", false); //log the open directory method
                if (sender is TextBlock textBlock) PathFunctions.OpenPath(PathInput.Text, textBlock.Text); //Open the path
                PathSearcher.SavePath = PathSearcher.Path; PathInput.Text = PathSearcher.Path; PathSearcher.SearchPath(this); //Search for the path
            }
            catch (Exception exception) { Logging.Log(exception.ToString(), "Explorer", true); } //Log the exception
        }
        private void OpenFile(object sender, MouseButtonEventArgs e) //M. to open a file
        {
            Logging.Log("OpenFile", "Explorer", false); //log the open file method
            var textBlock = sender as TextBlock; PathFunctions.OpenFile(textBlock?.Text); //Open the file
        }
        private void Undo(object sender, RoutedEventArgs e) //M. to undo the last action
        {
            if (!string.IsNullOrEmpty(PathSearcher.Path) && PathSearcher.Path != "C:\\") //Check if the path is not empty and not the root path
            {
                Logging.Log("Undo", "Explorer", false); //log the undo method
                var newPath = PathFunctions.Undo(); PathSearcher.SearchPath(this); PathInput.Text = newPath; //Search for the path
            }
        }

        private void Redo(object sender, MouseButtonEventArgs e) //M. to redo the last action
        {
            if (!string.IsNullOrEmpty(PathSearcher.SavePath) && PathSearcher.SavePath != PathSearcher.Path) //Check if the save path is not empty and not the same as the current path
            {
                Logging.Log("Redo", "Explorer", false); //log the redo method 
                var newPath = PathFunctions.Redo(); //Search for the path
                if (!string.IsNullOrEmpty(newPath)) { PathSearcher.Path = newPath; PathSearcher.DirectoryRemoveCount = 0; PathSearcher.FileRemoveCount = 0; PathSearcher.SearchPath(this); PathInput.Text = PathSearcher.Path; } //Search for the path
            }
        }
        private void OpenFieldPopup(object sender, RoutedEventArgs e) //M. to open the field popup
        {
            Logging.Log("OpenFieldPopup", "Explorer", false); //log the field popup method
            FieldPopup.IsOpen = true; PopupStackpanel.Children.Clear(); //Open the field popup and clear the stack panel
            if (Directorys.Children.Contains(sender as TextBlock)) { Scripts.PopUps.Directory.PopUp(this, sender as TextBlock); } //Check if the sender is a directory and call the directory popup
            else if (Files.Children.Contains(sender as TextBlock)) { Scripts.PopUps.File.PopUp(this, sender as TextBlock); } //Check if the sender is a file and call the file popup
        }

        private void CloseFieldPopup(object sender, EventArgs eventArgs) //M. to close the field popup
        {
            Logging.Log("CloseFieldPopup", "Explorer", false); //log the close field popup method
            FieldPopup.IsOpen = false; PopupStackpanel.Children.Clear(); //Close the field popup and clear the stack panel
        }
    }
}