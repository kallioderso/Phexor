using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Phexor.Commands;

namespace Phexor.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;

    private string _currentPath = string.Empty;
    private ObservableCollection<string> _directories = new();
    private ObservableCollection<string> _files = new();

    private readonly Stack<string> _undoStack = new(); // Historie für Rückwärtsnavigierung
    private readonly Stack<string> _redoStack = new(); // Historie für Vorwärtsnavigierung

    private bool _isNavigating; // Flag, um Benutzeraktionen von internen Navigationsaktionen zu unterscheiden

    public string CurrentPath
    {
        get => _currentPath;
        set
        {
            if (SetProperty(ref _currentPath, value))
            {
                if (!_isNavigating)
                {
                    AddToUndoStack(value); // Füge Benutzeränderung zur Historie hinzu
                }

                LoadAllFields();
            }
        }
    }

    public ObservableCollection<string> Directories
    {
        get => _directories;
        private set => SetProperty(ref _directories, value);
    }

    public ObservableCollection<string> Files
    {
        get => _files;
        private set => SetProperty(ref _files, value);
    }

    public ICommand OpenSettingsCommand { get; }
    public ICommand UndoCommand { get; }
    public ICommand RedoCommand { get; }

    public ICommand OpenDirectoryCommand { get; }
    public ICommand OpenFileCommand { get; }

    public MainViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        OpenSettingsCommand = new RelayCommand(OpenSettings);
        UndoCommand = new RelayCommand(UndoPath, CanUndo);
        RedoCommand = new RelayCommand(RedoPath, CanRedo);
        OpenDirectoryCommand = new RelayCommand<string>(OpenDirectory, CanOpenDirectory);
        OpenFileCommand = new RelayCommand<string>(OpenFile);

        CurrentPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    }

    private void AddToUndoStack(string path)
    {
        if (_undoStack.Count == 0 || _undoStack.Peek() != path)
        {
            _undoStack.Push(path);
            _redoStack.Clear(); // Lösche Redo-Stack bei neuer Benutzeraktion
        }
    }

    private void LoadAllFields()
    {
        if (string.IsNullOrWhiteSpace(CurrentPath) || !Directory.Exists(CurrentPath))
        {
            Directories = new ObservableCollection<string>();
            Files = new ObservableCollection<string>();
            return;
        }

        Directories = new ObservableCollection<string>(
            Directory.GetDirectories(CurrentPath).Select(Path.GetFileName));

        Files = new ObservableCollection<string>(
            Directory.GetFiles(CurrentPath).Select(Path.GetFileName));
    }

    private void OpenSettings()
    {
        var settingsWindow = _serviceProvider.GetRequiredService<SettingsWindow>();
        settingsWindow.ShowDialog();
    }

    private bool CanUndo() => _undoStack.Count > 1;

    private void UndoPath()
    {
        if (_undoStack.Count > 1)
        {
            _isNavigating = true; // Navigation beginnt
            var currentPath = _undoStack.Pop();
            _redoStack.Push(currentPath); // Verschiebe aktuellen Pfad in den Redo-Stack
            CurrentPath = _undoStack.Peek(); // Navigiere rückwärts
            _isNavigating = false; // Navigation beendet
        }
    }

    private bool CanRedo() => _redoStack.Count > 0;

    private void RedoPath()
    {
        if (_redoStack.Count > 0)
        {
            _isNavigating = true; // Navigation beginnt
            var redoPath = _redoStack.Pop();
            _undoStack.Push(redoPath); // Verschiebe Redo-Pfad in den Undo-Stack
            CurrentPath = redoPath; // Navigiere vorwärts
            _isNavigating = false; // Navigation beendet
        }
    }

    private bool CanOpenDirectory(string directoryName)
    {
        return !string.IsNullOrWhiteSpace(directoryName) && Directory.Exists(Path.Combine(CurrentPath, directoryName));
    }

    private void OpenDirectory(string directoryName)
    {
        if (!string.IsNullOrWhiteSpace(directoryName))
        {
            CurrentPath = Path.Combine(CurrentPath, directoryName);
        }
    }

    private bool CanOpenFile(string fileName)
    {
        return !string.IsNullOrWhiteSpace(fileName) && File.Exists(Path.Combine(CurrentPath, fileName));
    }

    private void OpenFile(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return;

        var filePath = Path.Combine(CurrentPath, fileName);
        if (File.Exists(filePath))
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true // Öffnet die Datei mit dem Standardprogramm
                });
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung, z. B. Log oder Meldung an den Benutzer
                Debug.WriteLine($"Fehler beim Öffnen der Datei: {ex.Message}");
            }
        }
    }
}