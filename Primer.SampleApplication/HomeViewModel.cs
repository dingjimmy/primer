namespace Primer.SampleApplication
{

    using Primer;
    using System;
    using System.Windows.Input;

    class HomeViewModel : ViewModel
    {
        private string firstName;
        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                SetProperty(() => FirstName, ref this.firstName, value);
            }
        }

        private string familyName;
        public string FamilyName
        {
            get { return this.familyName; }
            set
            {
                SetProperty(() => FamilyName, ref this.familyName, value);
            }
        }

        private DateTime? dateOfBirth;
        public DateTime? DateOfBirth
        {
            get { return this.dateOfBirth; }
            set
            {
                SetProperty(() => DateOfBirth, ref this.dateOfBirth, value);
            }
        }

        private string country;
        public string Country
        {
            get { return this.country; }
            set
            {
                SetProperty(() => this.Country, ref this.country, value);
            }
        }           

        public ILookup<string> AvailableTitles { get; set; }

        public ILookup<string> AvailableCountries { get; set; }

        public ICommand OkCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public HomeViewModel(IMessagingChannel msgChannel, IValidator validator, ILogger log)
        {
            this.Channel = msgChannel;
            this.Validator = validator;
            this.Logger = log;
        }

        public void Load()
        {

            this.Channel.Listen<PropertyChangedMessage>((m) => PropChanged(m.Name) );
            this.Channel.Listen<PropertyChangedMessage>((m) => m.Name == "FamilyName", (m) => FamilyNameChanged());


            this.Channel.Listen<PropertyChangedMessage>().AndInvoke(m => FamilyNameChanged()).Always();
            this.Channel.Listen<PropertyChangedMessage>().AndInvoke(m => FamilyNameChanged()).When(m => m.Name == "FamilyName");

        }

        public void Ok()
        {

        }

        public void Cancel()
        {

        }

        public void PropChanged(string property)
        {

        }

        public void FamilyNameChanged()
        {

        }
    }
}
