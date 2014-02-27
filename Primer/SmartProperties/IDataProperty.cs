// Copyright (c) James Dingle

namespace Primer.SmartProperties
{
    interface IDataProperty<T>
    {
        bool IsReadOnly { get; set; }
        string Name { get; }
        T Data { get; set; }
        ViewModel ViewModel { get; }
    }
}
