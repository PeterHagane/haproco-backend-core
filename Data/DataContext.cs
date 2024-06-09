using Microsoft.EntityFrameworkCore;

namespace haproco_backend_core.Data
{
    public class DataContext : DbContext
    {
        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbconnection = Environment.GetEnvironmentVariable("DB_CONNECTION");
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(dbconnection);
        }

        public DbSet<TestTable> TestTable => Set<TestTable>();
    }
}
