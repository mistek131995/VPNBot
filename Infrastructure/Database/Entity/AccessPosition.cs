﻿using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    public class AccessPosition
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int MonthCount { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string GooglePlayIdentifier { get; set; }

        public List<Payment> Payment { get; set; }
    }
}
