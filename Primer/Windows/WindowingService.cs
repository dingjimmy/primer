// Copyright (c) James Dingle

using System;
using System.Threading;
using System.Windows.Markup;

namespace Primer.Windows
{
    class WindowingService :IWindowingService
    {


        public bool ShowPopupWindow(ViewModel content, System.Windows.Window owner)
        {
            throw new NotImplementedException();
        }



        public string ShowFilePicker(FilePickerMode mode, string filename, System.Windows.Window owner, string defaultExtension, params string[] validExtensions)
        {
            throw new NotImplementedException();
        }



        public System.Windows.Window ShowWindow(string identifier, ViewModel content, System.Windows.Window owner)
        {
            
            // init the window
            var win = new Window();
            win.Name = identifier;
            win.SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
            win.Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.IetfLanguageTag);


            // set the viewmodel for this window to use
            if (content != null)
            {              
                win.Content = content;
                win.DataContext = content;
            }


            // set the window's owner
            if (owner != null) win.Owner = owner;


            // show the window
            win.Show();


            // return reference to caller
            return win;

        }



        public System.Windows.Window ShowAsyncWindow(string identifier, ViewModel content)
        {
            throw new NotImplementedException();
        }

    }
}
