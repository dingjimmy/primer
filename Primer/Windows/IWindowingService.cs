﻿// Copyright (c) James Dingle

namespace Primer.Windows
{

    public interface IWindowingService
    {

        bool ShowPopupWindow(ViewModel content, System.Windows.Window owner);

        string ShowFilePicker(FilePickerMode mode, string filename, System.Windows.Window owner, string defaultExtension, params string validExtensions);

        void ShowWindow(string identifier, ViewModel content, System.Windows.Window owner);

        void ShowAsyncWindow(string identifier, ViewModel content);

    }


    public enum FilePickerMode
    {
        OpenFile,
        SaveFile
    }

}
