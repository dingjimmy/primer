using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primer;

namespace Primer.SampleApplication
{
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
                SetProperty(() => Country, ref this.country, value);
            }
        }           

        public ILookup<string> AvailableTitles { get; set; }

        public ILookup<string> AvailableCountries { get; set; }

        public HomeViewModel(IMessagingChannel msgChannel, IValidator validator, ILogger log)
        {
            this.Channel = msgChannel;
            this.Validator = validator;
            this.Logger = log;
        }
    }
}
