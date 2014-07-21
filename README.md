# Primer

A simple MVVM based application framework for WPF and .NET, designed to work well with a domain model based upon the principals of Domain Driven Design.

### Usage

##### Fire-Up a ViewModel instance

When loading-up a new viewmodel, we reccommend you inject any dependancies in the constructor or using object initialisers (as shown below) and inject any data into the Initialise() method.

    var customerQuery = from c in Customers where c.ID = 123456 select c;
    var statusQuery = From s in CustomerStatus where select s;
    
    var vm = new CustomerViewModel() 
        { 
            Validator = new CustomerValidator(),
            Channel = new MessagingChannel(),
            Initialiser = new ViewModelInitialiser()
        };
        
    vm.Load(customerQuery, statusQuery);


##### Declaring a ViewModel

    class CustomerViewModel:ViewModel<Customer>
    {
    
        public Lookup<CustomerStatus> AvailableStatuses { get; set; }
        public Command Ok { get; set;}
        public Command Cancel { get; set;}
        
    
        public CustomerViewModel() {}
        

        public void Load(IQueryable<Customer> customers, IQueryable<CustomerStatus> statuses)
        {
        
        
            Model = customers.First();
            
            AvailableStatuses = Initialise.Lookup<CustomerStatus>(statuses, (status) => status.ID.ToString(), (status) => status.Name, (status) => status);

                
            Ok = Initialise.Command(true, p => Save(p));
            Cancel = Initialise.Command(true, p => Cancel(p));
            
        }
  
    }
    
