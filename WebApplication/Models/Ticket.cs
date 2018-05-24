using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public String Title { get; set; }
        [Required]
        public String Description { get; set; }

        public String Answer { get; set; }

        public String State { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
