// Copyright (c) James Dingle

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Primer
{
    public class Command : ICommand
    {


        #region Constructors


        // Primary Constructor
        public Command() { }



        // Alternative Constructor
        public Command(Action action)
        {
            _Action = action;
        }


        #endregion


        #region ICommand Support


        // private backing fields
        Action _Action;
        bool _IsEnabled;



        // public event 
        public event EventHandler CanExecuteChanged;



        /// <summary>
        /// Gets or sets the enabled state of the command. If true the action will execute on request, if false nothing will happen.
        /// </summary>
        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                if (value != _IsEnabled)
                {
                    _IsEnabled = value;
                    OnCanExecuteChanged(EventArgs.Empty);
                }
            }
        }



        /// <summary>
        /// Gets or sets the action delegate to call when the command is excuted.
        /// </summary>
        public Action Action
        {
            get
            {
                return _Action;
            }

            set {
                if (value != _Action)
                {
                    _Action = value;
                }
            }
        }



        /// <summary>
        /// Gets a value to indicate if this command is able to execuate at the moment.
        /// </summary>
        public bool CanExecute(object param)
        {
            return true;
        }



        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute(object param)
        {
            return;
        }



        /// <summary>
        ///  Raises the CanExecuateChanged event.
        /// </summary>
        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            EventHandler handler = CanExecuteChanged;
            if(handler != null)
            {
                handler(this, e);
            }
        }


        #endregion

    }
}
