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
            Channel = new MessagingChannel()
        };
        
    vm.Initialise(customerQuery, statusQuery);


##### Declaring a ViewModel

    class CustomerViewModel:ViewModel<Customer>
    {
    
        public Lookup<CustomerStatus> AvailableStatuses { get; set; }
        public Command Ok { get; set;}
        public Command Cancel { get; set;}
        
    
        public CustomerViewModel() {}
        

        protected override void Initialise(ViewModelInitialiser initialise, params object[] dataSources)
        {
        
            var customers = dataSources[0] as IQueryable<Customer>;
            var statuses = dataSources[1] as IQueryable<CustomerStatus>;
            
            Model = customers.First();
            
            AvailableStatuses = initialise.Lookup<CustomerStatus>(statuses, (status, item) => 
                {
                    item.Key = status.ID.ToString();
                    item.Description = status.Name;
                    item.Entity = status;
                }
                
            Ok = initialise.Command(true, p => Save(p));
            Cancel = initialise.Command(true, p => Cancel(p));
            
        }
  
    }
    
