// Copyright (c) James Dingle

using System.Windows;
using System.Windows.Input;

namespace Primer.Windows
{
    public class WindowBase : System.Windows.Window
    {

        CommandBinding minimize;
        CommandBinding maximize;
        CommandBinding close;

        public WindowBase()
        {

            // init command bindings
            close = new CommandBinding(SystemCommands.CloseWindowCommand, (s, e) => SystemCommands.CloseWindow((Window)this));
            minimize = new CommandBinding(SystemCommands.MinimizeWindowCommand, (s, e) => SystemCommands.MinimizeWindow((Window)this));
            maximize = new CommandBinding(SystemCommands.MaximizeWindowCommand, (s, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                    SystemCommands.RestoreWindow((Window)this);
                else
                    SystemCommands.MaximizeWindow((Window)this);
            });


            // add command bidings to this windows commandbindingcollection
            if (!CommandBindings.Contains(close)) CommandBindings.Add(close);
            if (!CommandBindings.Contains(minimize)) CommandBindings.Add(minimize);
            if (!CommandBindings.Contains(maximize)) CommandBindings.Add(maximize);

        }
        
    }
}
