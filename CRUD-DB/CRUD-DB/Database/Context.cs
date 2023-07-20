using Microsoft.EntityFrameworkCore;
using System;

namespace CRUD_DB.Database
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) {}

        //public virtual DbSet<Car> Cars { get; set; }
    }
}
