using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primer.SmartProperties
{
    public class DataProperty<T>:IDataProperty<T>
    {

        #region Constructors


        /// <summary>
        /// Default constructor. Only visible internally to assembly as the Init class is used to instantiate and initialse all DataProperties
        /// </summary>
        internal DataProperty(string name, T initialValue, ViewModel parentViewModel) : this(name, initialValue, parentViewModel, false) { }



        /// <summary>
        /// Alternative constructor that allows readonly flag to be set. Only visible internally to assembly as the Init class is used to instantiate and initialse all DataProperties
        /// </summary>
        internal DataProperty(string name, T initialValue, ViewModel parentViewModel, bool readOnly)
        {
            _IsReadOnly = readOnly;
            _Name = name;
            _CurrentValue = initialValue;
            _ViewModel = parentViewModel;
        }


        #endregion


        #region IDataProperty Support


        // private backing fields
        private bool _IsReadOnly;
        private string _Name;
        private T _CurrentValue;
        private ViewModel _ViewModel;



        /// <summary>
        /// Gets or sets the readonly state of the property. If true the data value will not be modified and the parent ViewModel will not be updated.
        /// </summary>
        public bool IsReadOnly
        {
            get { return _IsReadOnly; }
            set { _IsReadOnly = value; }
        }



        /// <summary>
        /// Gets the name of this property. It is used by a ViewModel to uniqely identify the property when updating, validating and recording errors.
        /// </summary>
        public string Name
        {
            get { return _Name; }
            internal set { _Name = value; }
        }



        /// <summary>
        /// Gets or sets the value of the data this proprty is responsible while automatically updating the state of the parent ViewModel.  
        /// </summary>
        public T Data
        {
            get { return _CurrentValue; }
            set
            {
                if (!_IsReadOnly)
                {
                    _CurrentValue = _ViewModel.UpdateProperty(_Name, _CurrentValue, value);
                }
            }
        }



        /// <summary>
        /// Gets the parent ViewModel.
        /// </summary>
        public ViewModel ViewModel
        {
            get { return _ViewModel; }
        }


        #endregion

    }
}
