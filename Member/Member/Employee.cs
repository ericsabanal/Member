using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;

namespace Member
{
    public class Employee
    {
        public long EmployeeId { get; set; }

        public int WorkersID { get; set; }
     

        public string FirstName { get; set; }
        public string MiddleName { get; set; }

   
        private string _lastname;


        public string LastName
        {
            get { return _lastname; }
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidOperationException("Dapat may laman ang apelyido!");
                }
                _lastname = value;
            }
        }

        private DateTime _birthday;
        public DateTime Birthday
        {
            get { return _birthday; }
            set { 
                if (DateTime.Now.Year - value.Year < 18)
                {
                    throw new InvalidOperationException("You must be at least 18 years old to be employed."); 
                }
                _birthday = value; 
            }
        }

    }
}
