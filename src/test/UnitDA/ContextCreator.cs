using Microsoft.EntityFrameworkCore;
using db_cp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitDA
{
    public class ContextCreator
    {
        public static AppDBContext CreateContext()
        {
            var name = "database_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: name)
                .Options;

            return new AppDBContext(options);
        }
    }
}
