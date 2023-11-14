﻿using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public long TelegramId { get; set; }
        public List<Access> Accesses { get; set; }
    }
}
