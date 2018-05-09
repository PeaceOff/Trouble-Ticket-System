using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Entities
{
    public class Ticket
    {
        public int ID { get; set; }

        public DateTime CreatedAt { get; set; } 

        public ApplicationUser Author { get; set; }

        public ApplicationUser Solver { get; set; }

        public string Tittle { get; set; }

        public string Description { get; set; }

        public string Answer { get; set; }

        public string State { get; set; }
    }
}