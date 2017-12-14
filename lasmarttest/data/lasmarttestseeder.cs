using LasmartTest.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasmartTest.Data
{
    public class LasmartTestSeeder
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHostingEnvironment _hosting;
        private readonly LasmartTestContext _ctx;

        public LasmartTestSeeder(UserManager<AppUser> userManager,
                                IHostingEnvironment environment,
                                LasmartTestContext ctx)
        {
            _userManager = userManager;
            _hosting = environment;
            _ctx = ctx;
        }

        public async Task Seed()
        {

            _ctx.Database.EnsureCreated();

            var user = await _userManager.FindByEmailAsync("test@test.test");

            if (user == null)
            {
                user = new AppUser()
                {
                    FirstName = "Test",
                    LastName = "Test",
                    UserName = "test@test.test",
                    Email = "test@test.test"

                };
                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default user");
                }
            }


            _ctx.SaveChanges();

        }
      }
}
