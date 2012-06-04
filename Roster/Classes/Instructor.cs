using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeadCount
{
    public class Instructor : BaseEntity<Instructor>
    {
        public Int64 InstructorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Contact ContactInfo { get; set; }

    }
}
