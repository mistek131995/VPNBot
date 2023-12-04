﻿using Core.Model.User;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public long TelegramUserId { get; set; }
        public long TelegramChatId { get; set; }
        public string Login {  get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public DateTime RegisterDate { get; set; }

        public Access Access { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
