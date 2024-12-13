using System.Windows.Input;
using Phexor.Commands;
using Phexor.Config;
using Phexor.Services;

namespace Phexor.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    
    private readonly SettingsService _settingsService;
    private ApplicationSettings _settings = null!;
    public ApplicationSettings ApplicationSettings
    {
        get => _settings;
        set => SetProperty(ref _settings, value);
    }

    public SettingsViewModel(ApplicationSettings settings, SettingsService settingsService)
    {
        ApplicationSettings = settings;
        _settingsService = settingsService;

        SaveCommand = new RelayCommand(SaveSettings);
    }
    

    public ICommand SaveCommand { get; }

    private void SaveSettings()
    {
        _settingsService.SaveSettings();
    }
}
