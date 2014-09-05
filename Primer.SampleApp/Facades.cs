using Primer.Messages;
using System;
using Primer;
using System.ComponentModel;
using FluentValidation;

namespace Primer.SampleApp
{

    public class CustomerFacade : Facade, IDataErrorInfo
    {

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                SetProperty(() => ID, ref _ID, value);
            }
        }


        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                SetProperty(() => FirstName, ref _FirstName, value);
            }
        }


        private string _FamilyName;
        public string FamilyName
        {
            get { return _FamilyName; }
            set
            {
                SetProperty(() => FamilyName, ref _FamilyName, value);
            }
        }


        private DateTime _StartDate;
        public DateTime StartDate
        {
            get { return _StartDate; }
            set
            {
                SetProperty(() => StartDate, ref _StartDate, value);
            }
        }


        private DateTime? _EndDate;
        public DateTime? EndDate
        {
            get { return _EndDate; }
            set
            {
                SetProperty(() => EndDate, ref _EndDate, value);
            }
        }


        private string _TestProperty;
        public string TestProperty
        {
            get { return _TestProperty; }
            set
            {
                SetProperty(() => TestProperty, ref _TestProperty, value);
            }
        }

        private string _TestOne;
        public string TestOne
        {
            get { return _TestOne; }
            set
            {
                SetProperty(() => TestOne, ref _TestOne, value);
            }
        }


        private string _TestTwo;
        public string TestTwo
        {
            get { return _TestTwo; }
            set
            {
                SetProperty(() => TestTwo, ref _TestTwo, value);
            }
        }


        private string _TestThree;
        public string TestThree
        {
            get { return _TestThree; }
            set
            {
                SetProperty(() => TestThree, ref _TestThree, value);
            }
        }





        public void SetID(int proposedValue, bool forceUpdate, bool broadcastMessage)
        {
            SetProperty(() => ID, ref _ID, proposedValue, forceUpdate, broadcastMessage);
        }


        public void SetFirstName(string proposedValue, bool forceUpdate, bool broadcastMessage)
        {
            SetProperty(() => FirstName, ref _FirstName, proposedValue, forceUpdate, broadcastMessage);
        }


        public void SetFamilyName(string proposedValue, bool forceUpdate, bool broadcastMessage)
        {
            SetProperty(() => FamilyName, ref _FamilyName, proposedValue, forceUpdate, broadcastMessage);
        }




        public CustomerFacade(Customer customer, IMessagingChannel channel, IValidator validator)
        {
            this.Channel = channel;
            this.Validator = validator;
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
                SetProperty(() => ID, ref _ID, value);
            }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                SetProperty(() => Description, ref _Description, value);
            }
        }

        private int _Quantity;
        public int Quantity
        {
            get { return _Quantity; }
            set
            {
                SetProperty(() => Quantity, ref _Quantity, value);
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
