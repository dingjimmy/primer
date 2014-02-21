using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primer
{


    public class Data<T>
    {

        #region Private Fields

        private ViewModel _Viewmodel;
        private string _Name;
        private bool _IsReadOnly;
        private T _CurrentValue;

        #endregion
     

        #region Public Properties


        public string Name 
        { 
            get 
            { 
                return _Name; 
            } 
            set 
            {
                _Name = value;
            } 
        }


        public bool IsReadOnly 
        { 
            get 
            { 
                return _IsReadOnly; 
            }
            set 
            { 
                _IsReadOnly = value;
            } 
        }


        public T Value
        {
            get
            {
                return _CurrentValue;
            }
            set
            {
                if (!IsReadOnly)
                {
                    _CurrentValue = _Viewmodel.UpdateProperty(Name, _CurrentValue, value);
                }
            }
        }


        #endregion


        #region Constructor


        public Data(ViewModel vm)
        {
            _IsReadOnly = false;
            _Viewmodel = vm;
        }

        public Data(string name, ViewModel vm, T initialValue)
        {
            _Name = name;
            _IsReadOnly = false;
            _Viewmodel = vm;
            _CurrentValue = initialValue;
        }


        #endregion

    }


   


}
