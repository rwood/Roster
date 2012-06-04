using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeadCount
{
    public class Student : BaseEntity<Student>
    {
        public Int64 StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Contact ContactInfo { get; set; }
        public School HomeSchool { get; set; }

    }
}
