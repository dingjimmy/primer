#Primer

A simple MVVM based application framework for WPF and .NET, designed to work well with a domain model based upon the principals of Domain Driven Design.

###Usage

#####Init ViewModel

    var customer = new Customer();
    var vm = new CustomerViewModel() 
        { 
            Model = customer, 
            Validator = new CustomerValidator(),
            Channel = new MessagingChannel()
        };
        
    vm.Initialise(


    class CustomerViewModel:ViewModel<Customer>
    {
    
        public CustomerViewModel()
        {
            Initialise()
        }

        protected override void Initialise(ViewModelInitialiser initialise, params object[] additionalData)
        {

        }
  
    }
    
