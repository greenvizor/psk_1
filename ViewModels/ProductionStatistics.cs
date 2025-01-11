using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace psk_1.ViewModels
{
    public class ProductionStatistics
    {
        public string ProductType { get; set; }
        public DateTime? ProductionDate { get; set; }
        public int ProductionCount { get; set; }
    }
}