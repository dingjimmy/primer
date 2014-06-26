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
                    Broadcast(new PropertyChanged() { Name = "ID", Sender = this });
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
                    Broadcast(new PropertyChanged() { Name = "FirstName", Sender = this });
                }
            }
        }


        public string _FamilyName;
        public string FamilyName
        {
            get { return _FamilyName; }
            set
            {
                if (UpdateProperty("FamilyName", ref _FamilyName, value, false))
                {
                    Broadcast(new PropertyChanged() { Name = "FamilyName", Sender = this });
                }
            }
        }

        public void SetFamilyName(string proposedValue, bool broadcastMessage, bool forceUpdate)
        {
            if (UpdateProperty("FamilyName", ref _FamilyName, proposedValue, forceUpdate) && broadcastMessage)
            {
                Broadcast(new PropertyChanged() { Name = "FamilyName", Sender = this });
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
                    Broadcast(new PropertyChanged() { Name = "StartDate", Sender = this });
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
                    Broadcast(new PropertyChanged() { Name = "EndDate", Sender = this });
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
                    Broadcast(new PropertyChanged() { Name = "TestProperty", Sender = this });
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
                    Broadcast(new PropertyChanged() { Name = "TestOne", Sender = this });
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
                    Broadcast(new PropertyChanged() { Name = "TestTwo", Sender = this });
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
                    Broadcast(new PropertyChanged() { Name = "TestThree", Sender = this });
                }
            }
        }


        public CustomerFacade(Customer customer, IMessagingChannel channel)
        {
            this.Channel = channel;
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
                    Broadcast(new PropertyChanged() { Name = "ID", Sender = this });
                }
            }
        }


        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                if (UpdateProperty("Description", ref _Description, value, false))
                {
                    Broadcast(new PropertyChanged() { Name = "Description", Sender = this });
                }
            }
        }


        private int _Quantity;
        public int Quantity
        {
            get { return _Quantity; }
            set
            {
                if (UpdateProperty("Quantity", ref _Quantity, value, false))
                {
                    Broadcast(new PropertyChanged() { Name = "Quantity", Sender = this });
                }
            }
        }



        public OrderDetailFacade(OrderDetail detail, IMessagingChannel channel)
        {
            this.Channel = channel;
            _ID = detail.ID;
            _Description = detail.Description;
            _Quantity = detail.Quantity;
        }

    }

}
