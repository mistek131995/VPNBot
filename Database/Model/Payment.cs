﻿using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AccessPositionId { get; set; }
        public DateTime Date { get; set; }

        public AccessPosition AccessPosition { get; set; }
    }
}
