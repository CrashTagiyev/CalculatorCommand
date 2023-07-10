using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CalculatorCommand.View_Model
{
    public class RelayCommand : ICommand
    {

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        Action<object?>? _Execute { get; set; }
        Predicate<object?>? _CanExecute { get; set; }
        public RelayCommand(Action<object?>? execute, Predicate<object?>? canExecute)
        {
            _Execute = execute;
            _CanExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _CanExecute != null || _CanExecute.Invoke(parameter);
        }

        public void Execute(object? parameter)
        {
            _Execute?.Invoke(parameter);
        }
    }
}
