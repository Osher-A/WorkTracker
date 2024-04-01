using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkTracker.WPF.Utilities
{
    public class CustomCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;
        private readonly object canSelecteEvent;
        private readonly ICommand selectedEventCommand;

        public CustomCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public CustomCommand(object canSelecteEvent, ICommand selectedEventCommand)
        {
            this.canSelecteEvent = canSelecteEvent;
            this.selectedEventCommand = selectedEventCommand;
        }

        public CustomCommand(Action<object> canSelecteEvent, ICommand selectedEventCommand)
        {
            this.canSelecteEvent = canSelecteEvent;
            this.selectedEventCommand = selectedEventCommand;
        }

        public bool CanExecute(object parameter)
        {
            bool b = canExecute == null || canExecute(parameter);
            return b;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
