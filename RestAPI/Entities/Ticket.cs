using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Entities
{
    public class Ticket
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } 

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public string SolverId { get; set; }

        public ApplicationUser Solver { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Answer { get; set; }

        // 'Unassigned', 'Assigned', 'WaitingForAnswers', 'Solved'
        public string State { get; set; }

        public List<SecondaryTicket> SecondaryTickets { get; set; }
    }
}