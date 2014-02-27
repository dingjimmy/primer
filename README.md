#Primer

A simple MVVM framework for.NET

###Usage

#####The new way with smart-properties...

    class CustomerViewModel:ViewModel
    {
    
        public DataProperty<int> ID { get; set; }
        public DataProperty<string> FirstName { get; set; }
        public DataProperty<string> FamilyName { get; set; }
        
        public CustomerViewModel() : base() { }

        public override void InitialiseDataProperties(DataPropertyInitialiser pi)
        {
            ID = pi.Initialise<int>("ID", this).WithValue(1280571);
            FirstName = pi.Initialise<string>("FirstName", this).WithValue("Joeseph");
            FamilyName = pi.Initialise<string>("FamilyName", this).WithValue("Bloggs");
        }
  
    }
    
#####The old way without smart-properties...

        class CustomerViewModel:ViewModel
        {
    
            int _ID = 987654321;
            public int ID
            {
                get { return _ID; }
                set { _ID = UpdateProperty("ID", _ID, value); }
            }
            
            string _FirstName = "Jeremy";
            public string FirstName
            {
                get { return _FirstName; }
                set { _FirstName = UpdateProperty("FirstName", _FirstName, value); }
            }
            
            string _FamilyName = "Kyle";
            public string FamilyName
            {
                get { return _FamilyName; }
                set { _FamilyName = UpdateProperty("FamilyName", _FamilyName, value); }
            }
            
            public CustomerViewModel() {}
        
        }
