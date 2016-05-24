using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HeartPharam.Models
{
    public class Users
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UsrDBContext : DbContext
    {
        public DbSet<Users> UsersList { get; set; }
    }
}