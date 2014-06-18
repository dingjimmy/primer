using Primer.Messages;
using System;
using Primer;

namespace Primer.SampleApp
{

    public class CustomerFacade : Facade
    {

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if (UpdateProperty("ID", ref _ID, value, false))
                {
                    Broadcast(new PropertyChanged() { Name = "ID" });
                }
            }
        }


        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                if (UpdateProperty("FirstName", ref _FirstName, value, false))
                {
                    Broadcast(new PropertyChanged() { Name = "FirstName" });
                }
            }
        }


        private string _FamilyName;
        public string FamilyName
        {
            get { return _FamilyName; }
            set
            {
                if (UpdateProperty("FamilyName", ref _FamilyName, value, false))
                {
                    Broadcast(new PropertyChanged() { Name = "FamilyName" });
                }
            }
        }


        private DateTime _StartDate;
        public DateTime StartDate
        {
            get { return _StartDate; }
            set
            {
                if (UpdateProperty("StartDate", ref _StartDate, value, false))
                {
                    Broadcast(new PropertyChanged() { Name = "StartDate" });
                }
            }
        }


        private DateTime? _EndDate;
        public DateTime? EndDate
        {
            get { return _EndDate; }
            set
            {
                if (UpdateProperty("EndDate", ref _EndDate, value, false))
                {
                    Broadcast(new PropertyChanged() { Name = "EndDate" });
                }
            }
        }


        private string _TestProperty;
        public string TestProperty
        {
            get { return _TestProperty; }
            set
            {
                if (UpdateProperty("TestProperty", ref _TestProperty, value, false))
                {
                    Broadcast(new PropertyChanged() { Name = "TestProperty" });
                }
            }
        }


        private string _TestOne;
        public string TestOne
        {
            get { return _TestOne; }
            set
            {
                if (UpdateProperty("TestOne", ref _TestOne, value, false))
                {
                    Broadcast(new PropertyChanged() { Name = "TestOne" });
                }
            }
        }


        private string _TestTwo;
        public string TestTwo
        {
            get { return _TestTwo; }
            set
            {
                if (UpdateProperty("TestTwo", ref _TestTwo, value, false))
                {
                    Broadcast(new PropertyChanged() { Name = "TestTwo" });
                }
            }
        }


        private string _TestThree;
        public string TestThree
        {
            get { return _TestThree; }
            set
            {
                if (UpdateProperty("TestThree", ref _TestThree, value, false))
                {
                    Broadcast(new PropertyChanged() { Name = "TestThree" });
                }
            }
        }


        public CustomerFacade(Customer customer)
        {
            _ID = customer.ID;
            _FirstName = customer.FirstName;
            _FamilyName = customer.FamilyName;
            _StartDate = customer.StartDate;
            _EndDate = customer.EndDate;
            _TestProperty = customer.TestProperty;
        }
    }


    public class OrderDetailFacade : Facade
    {

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if (UpdateProperty("ID", ref _ID, value, false))
                {
                    Broadcast(new PropertyChanged() { Name = "ID" });
                }
            }
        }


        public OrderDetailFacade(OrderDetail detail)
        {
            _ID = detail.ID;
        }

    }
}
