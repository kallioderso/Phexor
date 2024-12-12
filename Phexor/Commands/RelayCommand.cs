using System;
using System.Windows.Input;

namespace Phexor.Commands;

public class RelayCommand<T> : ICommand
{
    private readonly Action<T> _execute;
    private readonly Func<T, bool> _canExecute;

    // Generischer Konstruktor
    public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    // Nicht-generischer Konstruktor für Komfort
    public RelayCommand(Action execute, Func<bool> canExecute = null)
        : this(_ => execute(), _ => canExecute == null || canExecute())
    {
    }

    public bool CanExecute(object parameter)
    {
        if (parameter == null && typeof(T).IsValueType && Nullable.GetUnderlyingType(typeof(T)) == null)
        {
            // Null ist für nicht-nullbare Werttypen nicht zulässig
            return false;
        }

        var typedParameter = parameter is T param ? param : default!;
        return _canExecute?.Invoke(typedParameter) ?? true;
    }

    public void Execute(object parameter)
    {
        if (parameter == null && typeof(T).IsValueType && Nullable.GetUnderlyingType(typeof(T)) == null)
        {
            // Null ist für nicht-nullbare Werttypen nicht zulässig
            throw new ArgumentException($"Parameter vom Typ '{typeof(T)}' darf nicht null sein.");
        }

        var typedParameter = parameter is T param ? param : default!;
        _execute(typedParameter);
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}
public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool>? _canExecute;

    public RelayCommand(Action execute, Func<bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute?.Invoke() ?? true;
    }

    public void Execute(object? parameter)
    {
        _execute();
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}
