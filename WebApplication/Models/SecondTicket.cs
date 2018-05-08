using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class SecondTicket
    {
        public int ID { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public String Answer { get; set; }

        public String Authour { get; set; }

        public String Solver { get; set; }

        public String Department { get; set; }

        public DateTime createdAt { get; set; }
    }
}
