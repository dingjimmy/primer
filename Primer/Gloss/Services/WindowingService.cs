// Copyright (c) James Dingle

using System;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace Primer.Windows
{
    public class WindowingService :IWindowingService
    {

        bool _IsInitialized = false;
        ResourceDictionary _BaseWindowStyleResource;

        public void Initialize()
        {

            _BaseWindowStyleResource = new ResourceDictionary();
            _BaseWindowStyleResource.Source = new Uri("/primerlib;component/windows/styles/windowstyle.xaml", UriKind.Relative);
            //_BaseWindowStyleResource.(new Uri("/primerlib;component/windows/resources/close16.png", UriKind.Relative)); 

            //System.Uri resourceLocater = new Uri("/primerlib;component/windows/window.xaml", UriKind.Relative);
            //System.Windows.Application.LoadComponent(this, resourceLocater);

            _IsInitialized = true;

        }


        public bool ShowPopupWindow(IViewModel content, System.Windows.Window owner)
        {
            throw new NotImplementedException();
        }



        public string ShowFilePicker(FilePickerMode mode, string filename, System.Windows.Window owner, string defaultExtension, params string[] validExtensions)
        {
            throw new NotImplementedException();
        }



        public System.Windows.Window ShowWindow(string identifier, IViewModel content, System.Windows.Window owner)
        {
            
            //check service has been initialized
            if (!_IsInitialized) throw new InvalidOperationException("The WindowingService must be initialized first.");


            // init the window
            var win = new Primer.Windows.Window();
            win.Name = identifier;
            win.SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
            win.Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.IetfLanguageTag);
            win.Resources.MergedDictionaries.Add(_BaseWindowStyleResource);
            //win.WindowStyle = System.Windows.WindowStyle.None;
            //win.Style = (Style)_BaseWindowStyleResource["WindowStyle"];
           


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



        public System.Windows.Window ShowAsyncWindow(string identifier, IViewModel content)
        {
            throw new NotImplementedException();
        }

    }
}
