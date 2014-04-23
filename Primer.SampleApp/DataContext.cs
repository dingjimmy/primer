using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primer.SampleApp
{
    public class DataContext
    {

        public DataSet<OrderDetail> Details { get; set; }
        public DataSet<Supplier> Suppliers { get; set; }

        public DataContext() 
        { 
            Details = new DataSet<OrderDetail>(); 
            Suppliers = new DataSet<Supplier>(); 
        }

        public void SaveChanges() { }

    }


    public class DataSet<T> :IQueryable<T>
    {

        List<T> _Data;


        public DataSet()
        {
            _Data = new List<T>();
        }


        public void Add(T item) 
        { 
            _Data.Add(item);
        }


        public IEnumerator<T> GetEnumerator()
        {
 	        return _Data.AsQueryable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
 	        return _Data.AsQueryable().GetEnumerator();
        }

        public Type ElementType
        {
	        get { return typeof(T); }
        }

        public System.Linq.Expressions.Expression Expression
        {
	        get { return _Data.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
	        get { return _Data.AsQueryable().Provider; }
        }
    }


    public class OrderDetail
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
    }


    public class Supplier
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Branch { get; set; }
    }
}
