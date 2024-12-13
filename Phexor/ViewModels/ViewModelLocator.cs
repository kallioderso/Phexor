using System;
using Microsoft.Extensions.DependencyInjection;

namespace Phexor.ViewModels;

public class ViewModelLocator
{
    private readonly IServiceProvider _serviceProvider = null!;

    public ViewModelLocator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ViewModelLocator()
    {
        
    }

    public MainViewModel MainViewModel => _serviceProvider.GetRequiredService<MainViewModel>();
    public SettingsViewModel SettingsViewModel => _serviceProvider.GetRequiredService<SettingsViewModel>();
}