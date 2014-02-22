Primer
=

A simple MVVM framework for.NET

Usage
---

    class CustomerViewModel:ViewModel
    {
    
      public Data<int> ID { get; set; }
      public Data<string> FirstName { get; set; }
      public Data<string> FamilyName { get; set; }
      public Data<string> TelephoneNumber { get; set; }
      public Data<DateTime> StartDate { get; set; }
      public Data<DateTime?> EndDate { get; set; }
      public Data<List<OrderViewModel>> Orders { get; set; }
      
    
      public Action Save { get; set; }
      public Action Cancel { get; set; }
    
    
      public SampleViewModel()
      {
        ID = this.Init("ID", 987654321);
        FirstName = this.Init("FirstName", "Jeremy");
        FamilyName = this.Init("FamilyName", "Kyle");
        TelNumber = this.Init("TelNumber", "01225 436921");
        StartDate = this.InitDate("StartDate", "2014-02-22");
        EndDate = this.InitNullableDate("EndDate", null);
      }
  
    }
