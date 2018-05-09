using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Entities
{
    public class SecondaryTicket
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public int TicketId { get; set; }

        public Ticket Ticket { get; set; }

        public string Department { get; set; }

        public string Tittle { get; set; }

        public string Description { get; set; }

        public string Answer { get; set; }
    }
}