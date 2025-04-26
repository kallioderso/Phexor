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
    public partial class Explorer
    {
        public Explorer()
        {
            InitializeComponent();
            Initialize();
            
            this.KeyDown += (sender, e) => ShortCuts.KeyPress(e, this);
        }

        public void Initialize()
        {
            Colorize();
        }

        private void Colorize()
        {
            Logging.Log("Colorize", "Explorer", false);
            SettingsControl.GetSettings();
            Headbar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color1)!);
            ButtonField.Background =
                new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color2)!);
            DirectorysPanel.Background =
                new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color3)!);
            FilesPanel.Background =
                new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color4)!);
        }

        public void AddDirectory(TextBlock directory)
        {
            directory.MouseLeftButtonDown += OpenPath;
            directory.MouseRightButtonDown += OpenFieldPopup;
            Directorys.Children.Add(directory);
        }

        public void AddFile(TextBlock file)
        {
            file.MouseLeftButtonDown += OpenFile;
            file.MouseRightButtonDown += OpenFieldPopup;
            Files.Children.Add(file);
        }
        public void InputFieldPath() => PathInputEnter();
        private void PathInputEnter() { Logging.Log("StartPathSearch", "Explorer", false); PathSearcher.Path = PathInput.Text; PathSearcher.SavePath = PathInput.Text; PathSearcher.DirectoryRemoveCount = 0; PathSearcher.FileRemoveCount = 0; PathSearcher.SearchPath(this); }

        private void DirectoryScrollingWithMouse(object sender, MouseWheelEventArgs e)
        {
            Logging.Log("DirectoryScrolling", "Explorer", false);
            if (e.Delta < 0) // check in which direction got scrolled
            {
                PathSearcher.DirectoryRemoveCount++;
                PathSearcher.SearchPath(this);
            }
            else if (e.Delta > 0) // check if the other direction was it
            {
                if (PathSearcher.DirectoryRemoveCount != 0)
                {
                    PathSearcher.DirectoryRemoveCount--;
                    PathSearcher.SearchPath(this);
                }
            }
        }

        private void FileScrollingWithMouse(object sender, MouseWheelEventArgs e)
        {
            Logging.Log("FileScrolling", "Explorer", false);
            if (e.Delta < 0) // check in which direction got scrolled
            {
                PathSearcher.FileRemoveCount++;
                PathSearcher.SearchPath(this);
            }
            else if (e.Delta > 0) // check if the other direction was it
            {
                if (PathSearcher.FileRemoveCount != 0)
                {
                    PathSearcher.FileRemoveCount--;
                    PathSearcher.SearchPath(this);
                }
            }
        }

        public void Settings() => OpenSettings(null, null);
        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            Logging.Log("OpenSettings", "Explorer", false);
            var settings = new Settings(this);
            settings.Show();
        }

        private void OpenPath(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Logging.Log("OpenDirectory", "Explorer", false);
                if (sender is TextBlock textBlock) PathFunctions.OpenPath(PathInput.Text, textBlock.Text);
                PathSearcher.SavePath = PathSearcher.Path;
                PathInput.Text = PathSearcher.Path;
                PathSearcher.SearchPath(this);
            }
            catch (Exception exception)
            {
                Logging.Log(exception.ToString(), "Explorer", true);
            }
        }

        private void OpenFile(object sender, MouseButtonEventArgs e)
        {
            Logging.Log("OpenFile", "Explorer", false);
            var textBlock = sender as TextBlock;
            PathFunctions.OpenFile(textBlock?.Text);
        }
        
        private void Undo(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PathSearcher.Path) && PathSearcher.Path != "C:\\")
            {
                Logging.Log("Undo", "Explorer", false);
                var newPath = PathFunctions.Undo();
                PathSearcher.SearchPath(this);
                PathInput.Text = newPath;
            }
        }

        public void UndoPath() => Undo(null, null);

        private void Redo(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(PathSearcher.SavePath) && PathSearcher.SavePath != PathSearcher.Path)
            {
                Logging.Log("Redo", "Explorer", false);
                var newPath = PathFunctions.Redo();

                if (!string.IsNullOrEmpty(newPath))
                {
                    PathSearcher.Path = newPath;
                    PathSearcher.DirectoryRemoveCount = 0;
                    PathSearcher.FileRemoveCount = 0;
                    PathSearcher.SearchPath(this);
                    PathInput.Text = PathSearcher.Path;
                }
            }
        }

        public void RedoPath() => Redo(null, null);
        
        private void OpenFieldPopup(object sender, RoutedEventArgs e)
        {
            Logging.Log("OpenFieldPopup", "Explorer", false);
            FieldPopup.IsOpen = true;
            if (Directorys.Children.Contains(sender as TextBlock))
            {
                Scripts.PopUps.Directory directory = new Scripts.PopUps.Directory(this, sender as TextBlock);
                directory.PopUp();
            }
            else if (Files.Children.Contains(sender as TextBlock))
            {
                Scripts.PopUps.File file = new Scripts.PopUps.File(this, sender as TextBlock);
                file.PopUp();
            }
        }
        
        private void CloseFieldPopup(object sender, EventArgs eventArgs)
        {
            Logging.Log("CloseFieldPopup", "Explorer", false);
            FieldPopup.IsOpen = false;
            PopupStackpanel.Children.Clear();
        }
    }
}