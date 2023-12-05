﻿using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    public class VpnServer
    {
        [Key]
        public int Id { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Passsword { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserCount { get; set; }

        public List<Access> Access { get; set; }
    }
}
