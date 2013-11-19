using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Primer
{

    public class MyViewModel : ViewModel
    {

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = UpdateProperty("ID", _ID, value); }
        }



        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = UpdateProperty("Name", _Name, value); }
        }



        private string _EmailAddress;
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = UpdateProperty("EmailAddress", _EmailAddress, value); }
        }


        public ICommand Save { get; set; }

        public ICommand Cancel { get; set; }


        public MyViewModel()
        {

            

        }


        public void SaveMe()
        {

            Validate("ID", "Name", "EmailAddress");

            if (!InError("ID", "Name", "EmailAddress"))
            {
                // save me to a database!
            }
        }
    }

}
