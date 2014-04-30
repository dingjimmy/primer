#Primer

A simple MVVM framework for.NET

###Usage

#####The new way...

    class CustomerViewModel:ViewModel
    {
    
        public Field<int> ID { get; set; }
        public Field<string> FirstName { get; set; }
        public Field<string> FamilyName { get; set; }
        
        public CustomerViewModel() : base() { }

        public override void InitialiseDataProperties(ViewModelInitialiser initialise)
        {
            ID = initialise.Field<int>("ID").WithValue(1280571);
            FirstName = initialise.Field<string>("FirstName").WithValue("Joeseph");
            FamilyName = initialise.Field<string>("FamilyName").WithValue("Bloggs");
        }
  
    }
    
#####The old way...

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
