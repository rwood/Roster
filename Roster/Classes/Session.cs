using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeadCount
{
    public class Session : BaseEntity<Session>
    {
        public Int64 SessionID { get; set; }
        public string Name { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        
    }
}
