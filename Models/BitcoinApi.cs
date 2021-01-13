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
        public float Last { get; set; }
        public float Bid { get; set; }
        public float Ask { get; set; }
        public float Vwap { get; set; }
        public float Average { get; set; }
        public float Volume { get; set; }
    }

}
