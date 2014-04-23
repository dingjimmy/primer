// Copyright (c) James Dingle

namespace Primer
{
    public interface IDataField<T>
    {
        bool IsReadOnly { get; set; }
        string Name { get; }
        T Data { get; set; }
        ViewModel ViewModel { get; }
    }
}
