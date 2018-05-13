using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class SecondaryTicket
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        public Ticket Ticket { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public String Answer { get; set; }

        public String Department { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
