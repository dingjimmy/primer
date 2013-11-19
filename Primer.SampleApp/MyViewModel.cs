using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Primer.Validation;

namespace Primer.SampleApp
{

    public class MyViewModel : ViewModel
    {

        private int _ID;

        [NullValueValidator]
        public int ID
        {
            get { return _ID; }
            set { _ID = UpdateProperty("ID", _ID, value); }
        }



        private string _Name;

        [NullValueValidator]
        public string Name
        {
            get { return _Name; }
            set { _Name = UpdateProperty("Name", _Name, value); }
        }



        private string _EmailAddress;

        [NullValueValidator]
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = UpdateProperty("EmailAddress", _EmailAddress, value); }
        }



        public ICommand Ok { get; set; }


        public ICommand Cancel { get; set; }


        public MyViewModel() : base() { }


        public void Save(object parameter)
        {

            Validate("ID", "Name", "EmailAddress");

            if (!InError("ID", "Name", "EmailAddress"))
            {
                // save me to a database!
            }
        }


        public void CancelThis(object parameter)
        {

        }
    }

}
