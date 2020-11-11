using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BeautySalon.Commands
{
    class BaseCommand : ICommand
    {
        private Action<object> action;
        private Func<bool> canExecute;
        public event EventHandler CanExecuteChanged;

        public BaseCommand(Action<object> action, Func<bool> canExecute = null)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)=> canExecute?.Invoke() ?? true;
       
        public void Execute(object parameter) => action?.Invoke(parameter);

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
