using System.Windows.Input;
using Phexor.Commands;
using Phexor.Config;
using Phexor.Services;

namespace Phexor.ViewModels;

public class SettingsViewModel
{
    private readonly ApplicationSettings _settings;
    private readonly SettingsService _settingsService;

    public SettingsViewModel(ApplicationSettings settings, SettingsService settingsService)
    {
        _settings = settings;
        _settingsService = settingsService;

        SaveCommand = new RelayCommand(SaveSettings);
    }
    

    public ICommand SaveCommand { get; }

    private void SaveSettings()
    {
        _settingsService.SaveSettings();
    }
}
