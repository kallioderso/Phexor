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
            SettingsControl.GetSettings();
            Headbar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color1)!);
            ButtonField.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color2)!);
            DirectorysPanel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color3)!);
            FilesPanel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(SettingsControl.Color4)!);
        }
        
        private void ClearFields()
        {
            Directorys.Children.Clear();
            Files.Children.Clear();
        }

        public void AddDirectory(TextBlock directory)
        {
            directory.MouseDown += OpenPath;
            Directorys.Children.Add(directory);
        }
        
        public void AddFile(TextBlock file)
        {
            Files.Children.Add(file);
        }
        
        private void PathInputEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PathSearcher.Path = PathInput.Text;
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
            var settings = new Settings(this);
            settings.Show();
        }
        
        private void OpenPath(object sender, MouseButtonEventArgs e)
        {
            var textBlock = (TextBlock)sender;
            PathInput.Text = PathInput.Text + @"/" + textBlock.Text;
            PathSearcher.Path = PathInput.Text;
            PathSearcher.DirectoryRemoveCount = 0;
            PathSearcher.FileRemoveCount = 0;
            PathSearcherInitialize();
        }
    }
}
