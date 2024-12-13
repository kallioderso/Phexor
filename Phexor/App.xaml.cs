using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Phexor.Config;
using Phexor.Services;
using Phexor.ViewModels;
using Phexor.Views;

namespace Phexor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static ServiceProvider ServiceProvider { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            Application.Current.Resources["ViewModelLocator"] = ServiceProvider.GetRequiredService<ViewModelLocator>();
            
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            LoadSettings();
            mainWindow.Show();
        }
        
        private void ConfigureServices(IServiceCollection services)
        {
            var appSettings = new ApplicationSettings();
            services.AddSingleton(appSettings);
            services.AddSingleton<SettingsService>();
            
            services.AddSingleton<ViewModelLocator>();
            
            services.AddTransient<MainViewModel>();
            services.AddTransient<SettingsViewModel>();
            
            services.AddSingleton<MainWindow>();
            services.AddTransient<SettingsWindow>();
            
            
            
           
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Speichere die aktuellen Einstellungen beim Schließen der App
            SaveSettings();
            base.OnExit(e);
        }
        
        private void SaveSettings()
        {
            var settingsService = ServiceProvider.GetRequiredService<SettingsService>();
            settingsService.SaveSettings();
        }

        private void LoadSettings()
        {
            var settingsService = ServiceProvider.GetRequiredService<SettingsService>();
            settingsService.LoadSettings();
        }
    }
}