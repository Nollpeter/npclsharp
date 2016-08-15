using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace testWPF.ViewModel
{
    public class DelegateCommand :ICommand
    {
        public DelegateCommand(Action<Object> action)
        {
            _action = action;
        }

        private Action<Object> _action;
        public Boolean CanExecute(Object parameter)
        {
            return true;
                //throw new NotImplementedException();
        }

        public void Execute(Object parameter)
        {
            _action.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
