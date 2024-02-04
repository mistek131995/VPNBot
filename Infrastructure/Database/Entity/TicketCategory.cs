﻿using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    internal class TicketCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}
