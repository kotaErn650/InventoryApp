using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Input;

namespace InventoryApp.Commands;

public class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;

    public RelayCommand(Action<object?> execute)
    {
        _execute = execute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        _execute(parameter);
    }
}
