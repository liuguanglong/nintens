using Microsoft.EntityFrameworkCore;
using repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class ApplicationIdentityService
    {
        private IDbContextFactory<ApplicationDbContext> contextFactory;

        public ApplicationIdentityService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<ApplicationUser> getUser(String name)
        {
            using var context = contextFactory.CreateDbContext();
            return await context.Users.Where(u => u.UserName == name).FirstOrDefaultAsync();
        }

        public async Task updateTimeZone(ApplicationUser user)
        {
            using var context = contextFactory.CreateDbContext();
            var userinfo = await context.Users.Where(u => u.UserName == user.UserName).FirstOrDefaultAsync();
            if (userinfo != null)
            {
                userinfo.TimeZoneId = user.TimeZoneId;
                await context.SaveChangesAsync();
            }
        }
    }
}
