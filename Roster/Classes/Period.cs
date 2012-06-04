using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace HeadCount
{
    public class Period : BaseEntity<Period>
    {
        public Int64 PeriodID { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        
    }
}
