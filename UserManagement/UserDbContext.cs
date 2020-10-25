using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Entities;

namespace UserManagement
{
    public class UserDbContext : DbContext
    {
        public DbSet<Address> Address { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Geo> Geo { get; set; }

        public UserDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
