using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
   
    public class BitcoinApi
    {
        public float Max { get; set; }
        public float Min { get; set; }
        public int Last { get; set; }
        public float Bid { get; set; }
        public int Ask { get; set; }
        public int Vwap { get; set; }
        public int Average { get; set; }
        public float Volume { get; set; }
    }

}
