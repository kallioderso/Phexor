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

    public string ForegroundColor
    {
        get => _settings.ForegroundColor;
        set => _settings.ForegroundColor = value;
    }

    public string BackgroundColor
    {
        get => _settings.BackgroundColor;
        set => _settings.BackgroundColor = value;
    }

    public string SpecialColor
    {
        get => _settings.SpecialColor;
        set => _settings.SpecialColor = value;
    }

    public ICommand SaveCommand { get; }

    private void SaveSettings()
    {
        _settingsService.SaveSettings();
    }
}
