// Copyright (c) James Dingle

namespace Primer.SmartProperties
{
    interface IDataProperty<T>
    {
        bool IsReadOnly { get; set; }
        string Name { get; set; }
        T Value { get; set; }
        ViewModel ViewModel { get; set; }
    }
}
