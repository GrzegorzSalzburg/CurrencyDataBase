using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CurrencyDataBase
{
    public class Currency
    {
        public string Disclaimer { get; set; }
        public string License { get; set; }
        public int Timestamp { get; set; }
        public string Base { get; set; }
        public Dictionary<string, float> Rates { get; set; }
    }

    public class Currency_Base
    {
        public int ID { set; get; }
        public int Index { set; get; }
        public string currencyName { set; get; }
        public float rate { set; get; }
    }


    public class Currency_values : DbContext
    {
        public virtual DbSet<Currency_Base> Currencies { get; set; }
    }
}
