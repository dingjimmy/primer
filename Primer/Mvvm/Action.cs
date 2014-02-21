using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Primer
{
    class Action : ICommand
    {

        public Action()
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object param)
        {
            return true;
        }

        public void Execute(object param)
        {
            return;
        }

    }
}
