using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Phexor.ViewModels;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Benachrichtigt die UI über Änderungen an einer Eigenschaft.
    /// </summary>
    /// <param name="propertyName">Der Name der geänderten Eigenschaft.</param>
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Setzt den Wert einer Eigenschaft und löst die Benachrichtigung aus, falls sich der Wert geändert hat.
    /// </summary>
    /// <typeparam name="T">Der Typ der Eigenschaft.</typeparam>
    /// <param name="field">Die Backing-Field-Referenz.</param>
    /// <param name="value">Der neue Wert.</param>
    /// <param name="propertyName">Der Name der Eigenschaft (optional).</param>
    /// <returns>True, wenn der Wert geändert wurde, ansonsten False.</returns>
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false; // Kein Update notwendig
        }

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}