using Core.Model.User;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AccessPositionId { get; set; }
        [Precision(18, 3)]
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public PaymentState State { get; set; }

        public AccessPosition AccessPosition { get; set; }
    }
}
