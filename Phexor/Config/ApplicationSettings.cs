using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Phexor.Config;

public class ApplicationSettings : INotifyPropertyChanged
{
    private string _foregroundColor = "#FFFFF8DC";
    private string _backgroundColor = "#FFF0FFFF";
    private string _specialColor = "#FFE6E6FA";

    public string ForegroundColor
    {
        get => _foregroundColor;
        set => SetProperty(ref _foregroundColor, value);
    }

    public string BackgroundColor
    {
        get => _backgroundColor;
        set => SetProperty(ref _backgroundColor, value);
    }

    public string SpecialColor
    {
        get => _specialColor;
        set => SetProperty(ref _specialColor, value);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}