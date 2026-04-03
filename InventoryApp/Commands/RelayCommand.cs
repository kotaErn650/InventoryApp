using System.Windows.Input;

namespace InventoryApp.Commands;

public class RelayCommand : ICommand
{
    private readonly Func<object?, Task>? _asyncExecute;
    private readonly Action<object?>? _execute;

    public RelayCommand(Action<object?> execute)
    {
        _execute = execute;
    }

    public RelayCommand(Func<object?, Task> execute)
    {
        _asyncExecute = execute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;

    public async void Execute(object? parameter)
    {
        try
        {
            if (_asyncExecute != null)
                await _asyncExecute(parameter);
            else
                _execute!(parameter);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[RelayCommand] Unhandled exception: {ex}");
        }
    }
}
