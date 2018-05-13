using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Ticket
    {
        public int ID { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public string SolverId { get; set; }

        public ApplicationUser Solver { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public String Answer { get; set; }

        public String State { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<SecondaryTicket> SecondaryTickets { get; set; }
    }
}
