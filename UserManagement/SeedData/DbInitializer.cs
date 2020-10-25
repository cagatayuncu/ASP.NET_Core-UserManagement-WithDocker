using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Entities;

namespace UserManagement.SeedData
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<UserDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<UserDbContext>())
                {
                    //If there is no data, the data is added first.
                    if (!context.User.Any())
                    {
                        var json = new WebClient().DownloadString("https://jsonplaceholder.typicode.com/users");
                        var users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(json);
                        users.ForEach(s =>
                        {
                            s.Id = 0;
                            s.Password = "123123";
                        });
                        
                        context.User.AddRange(users);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
