using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.User
{
    public class UserUserPromoCode
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PromoCodeId { get; set; }
        public DateTime UsedDate { get; set; }
    }
}
