using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        }

        public void Initialize()
        {
            Colorize();
        }

        private void Colorize()
        {
            var log = new Logging("Colorize", "Explorer", false);
            SettingsControl.GetSettings();
            Headbar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color1)!);
            ButtonField.Background =
                new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color2)!);
            DirectorysPanel.Background =
                new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color3)!);
            FilesPanel.Background =
                new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color4)!);
        }

        private void ClearFields()
        {
            Directorys.Children.Clear();
            Files.Children.Clear();
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

        private void PathInputEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var log = new Logging("StartPathSearch", "Explorer", false);
                PathSearcher.Path = PathInput.Text;
                PathSearcher.SavePath = PathInput.Text;
                PathSearcher.DirectoryRemoveCount = 0;
                PathSearcher.FileRemoveCount = 0;
                PathSearcherInitialize();
            }
        }

        private void PathSearcherInitialize()
        {
            ClearFields();
            PathSearcher searcher = new PathSearcher(this);
            searcher.SearchPath();
        }

        private void DirectoryScrollingWithMouse(object sender, MouseWheelEventArgs e)
        {
            var log = new Logging("DirectoryScrolling", "Explorer", false);
            if (e.Delta < 0) // check in which direction got scrolled
            {
                PathSearcher.DirectoryRemoveCount++;
                PathSearcherInitialize();
            }
            else if (e.Delta > 0) // check if the other direction was it
            {
                if (PathSearcher.DirectoryRemoveCount != 0)
                {
                    PathSearcher.DirectoryRemoveCount--;
                    PathSearcherInitialize();
                }
            }
        }

        private void FileScrollingWithMouse(object sender, MouseWheelEventArgs e)
        {
            var log = new Logging("FileScrolling", "Explorer", false);
            if (e.Delta < 0) // check in which direction got scrolled
            {
                PathSearcher.FileRemoveCount++;
                PathSearcherInitialize();
            }
            else if (e.Delta > 0) // check if the other direction was it
            {
                if (PathSearcher.FileRemoveCount != 0)
                {
                    PathSearcher.FileRemoveCount--;
                    PathSearcherInitialize();
                }
            }
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            var log = new Logging("OpenSettings", "Explorer", false);
            var settings = new Settings(this);
            settings.Show();
        }

        private void OpenPath(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var log = new Logging("OpenDirectory", "Explorer", false);
                if (sender is TextBlock textBlock) PathFunctions.OpenPath(PathInput.Text, textBlock.Text);
                PathSearcher.SavePath = PathSearcher.Path;
                PathInput.Text = PathSearcher.Path;
                PathSearcherInitialize();
            }
            catch (Exception exception)
            {
                var log = new Logging(exception.ToString(), "Explorer", true);
            }
        }

        private void OpenFile(object sender, MouseButtonEventArgs e)
        {
            var log = new Logging("OpenFile", "Explorer", false);
            var textBlock = sender as TextBlock;
            PathFunctions.OpenFile(textBlock?.Text);
        }
        
        private void Undo(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PathSearcher.Path) && PathSearcher.Path != "C:\\")
            {
                var log = new Logging("Undo", "Explorer", false);
                var newPath = PathFunctions.Undo();
                PathSearcherInitialize();
                PathInput.Text = newPath;
            }
        }

        private void Redo(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(PathSearcher.SavePath) && PathSearcher.SavePath != PathSearcher.Path)
            {
                var log = new Logging("Redo", "Explorer", false);
                var newPath = PathFunctions.Redo();

                if (!string.IsNullOrEmpty(newPath))
                {
                    PathSearcher.Path = newPath;
                    PathSearcher.DirectoryRemoveCount = 0;
                    PathSearcher.FileRemoveCount = 0;
                    PathSearcherInitialize();
                    PathInput.Text = PathSearcher.Path;
                }
            }
        }
        
        private void OpenFieldPopup(object sender, RoutedEventArgs e)
        {
            var log = new Logging("OpenFieldPopup", "Explorer", false);
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
            var log = new Logging("CloseFieldPopup", "Explorer", false);
            FieldPopup.IsOpen = false;
            PopupStackpanel.Children.Clear();
        }
    }
}