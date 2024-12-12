using System.IO;
using System.Text.Json;
using Phexor.Config;
using Phexor.Utilities;

namespace Phexor.Services;


public class SettingsService
{
    private readonly ApplicationSettings _settings;

    public SettingsService(ApplicationSettings settings)
    {
        _settings = settings;
        AppPaths.EnsureAppDataFolderExists();
    }

    public void SaveSettings()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var settingsJson = JsonSerializer.Serialize(_settings, options);
        File.WriteAllText(AppPaths.SettingsFilePath, settingsJson);
    }

    public void LoadSettings()
    {
        if (File.Exists(AppPaths.SettingsFilePath))
        {
            var loadedSettings = JsonSerializer.Deserialize<ApplicationSettings>(File.ReadAllText(AppPaths.SettingsFilePath));
            if (loadedSettings != null)
            {
                _settings.ForegroundColor = loadedSettings.ForegroundColor;
                _settings.BackgroundColor = loadedSettings.BackgroundColor;
                _settings.SpecialColor = loadedSettings.SpecialColor;
            }
        }
        else
        {
            SaveSettings();
        }
    }
}
