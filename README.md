# Primer

A simple MVVM based application framework for WPF and .NET, designed to work well with a domain model based upon the principals of Domain Driven Design.

### Usage

There are four main dependacies on a ViewModel; a Validator, a Messaging-Channel, a ViewModel-Initialiser and of course the Model. They can be injected-in, or created in the constructor or any other method; whichever you prefer. The below example injects three of the dependancies using object initialisers and then loads the Model and a supporting Lookup using two LINQ queries inside the <code>Load(customers, statuses)</code> method. 

<em>This is the recommended way to implement a ViewModel, but you are free to do it however you please!</em>

##### Fire-Up a ViewModel instance

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
        public Command OkCommand { get; set;}
        public Command CancelCommand { get; set;}
        
        public CustomerViewModel() {}

        public void Load(IQueryable<Customer> customers, IQueryable<CustomerStatus> statuses)
        {
        
            // load the Model
            Model = customers.First();
            
            // initialise the Lookup
            AvailableStatuses = Initialise.Lookup<CustomerStatus>(statuses, (status) => status.ID.ToString(), (status) => status.Name, (status) => status);

            // initialise commands
            OkCommand= Initialise.Command(true, p => Save(p));
            CancelCommand = Initialise.Command(true, p => Cancel(p));
            
            // listen for properties that have changed
            Listen<PropertyChanged>((msg) => OnAnyPropertyChanged(msg));
            Listen<PropertyChanged>((msg) msg.Sender == this.Model, (msg) => OnModelPropertyChanged(msg.Name));
            
        }
        
        
        public void OnAnyPropertyChanged(message PropertyChanged) { ... }
        public void OnModelPropertyChanged(property String) { ... }
        public void Save(object property) { ... }
        public void Cancel(object property) { ... }
  
    }
    
