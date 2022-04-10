using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository.test
{

    public class ApplicationDbContextTest
    {
        Mock<IDbContextFactory<ApplicationDbContext>> factory;

        [SetUp]
        public void Setup()
        {
            String connString = "server=localhost; userid=pgo;pwd=pgoplus;port=3306;database=nintens;Allow Zero Datetime=true;Convert Zero Datetime=True";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));

            factory = new Mock<IDbContextFactory<ApplicationDbContext>>();
            factory.Setup(f => f.CreateDbContext())
                .Returns(new ApplicationDbContext(optionsBuilder.Options));
        }

        [Test]
        public void Test1()
        {
            var dbcontext = factory.Object.CreateDbContext();
            var user = dbcontext.Users.Where(u => u.UserName == "liuguanglong@yahoo.com").FirstOrDefault();
            user.CustomTag = "Administartor";
            dbcontext.SaveChanges();    

        }
    }
}
