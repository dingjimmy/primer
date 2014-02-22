#Primer

A simple MVVM framework for.NET

###Usage

#####The new way with smart-properties...

    class CustomerViewModel:ViewModel
    {
    
        //
        // Declare Data-Properties
        //
        public Data<int> ID { get; set; }
        public Data<string> FirstName { get; set; }
        public Data<string> FamilyName { get; set; }
        public Data<string> TelNumber { get; set; }
        public Data<DateTime> StartDate { get; set; }
        public Data<DateTime?> EndDate { get; set; }
        public Data<List<OrderViewModel>> Orders { get; set; }
    
    
        //
        // Declare Action-Properties
        //
        public Action Save { get; set; }
        public Action Cancel { get; set; }
        
        
        public CustomerViewModel()
        {

            //
            // Initialise Data-Properties
            //
            ID = Init.Data(this,"ID", 987654321);
            FirstName = Init.Data(this, "FirstName", "Jeremy");
            FamilyName = Init.Data(this, "FamilyName", "Kyle");
            TelNumber = Init.Data(this, "TelNumber", "01225 436921");
            StartDate = Init.Data(this,"StartDate", Convert.ToDateTime("2014-02-22"));
            EndDate = Init.NullableData(this, "End Date", Convert.ToDateTime("2014-11-4"));
            Orders = Init.Data(this, "Order", new List<OrderViewModel>()
            {
                new OrderViewModel(), 
                new OrderViewModel(), 
                new OrderViewModel()
            });


            //
            // TODO: Initialise Action-Properties
            //
            
        }
  
    }
    
#####The old way without smart-properties...

        class CustomerViewModel:ViewModel
        {
    
            //
            // Declare Data-Properties
            //
            
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
            
            
            public CustomerViewModel()
            {
            }
        
        }
