using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blatant.Models
{

    /// <summary>
    /// Lookup table for options available. 
    /// </summary>
    public class Option
    {
        public int ID { get; set; }
        public string OptionName { get; set; }
        public decimal? OptionPrice { get; set; }
        public decimal? Discount { get; set; }
    }
}
