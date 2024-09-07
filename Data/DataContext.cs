using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace haproco_backend_core.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string? dbconnection;

            object? instance;
            object? result = null;
            Type? dbConnType = Type.GetType("haproco_backend_core.Properties.DbConnection");
            MethodInfo? getString = dbConnType?.GetMethod("GetString");

            dbconnection = Environment.GetEnvironmentVariable("DB_CONNECTION");

            if (dbConnType != null && getString != null)
            {
                instance = Activator.CreateInstance(dbConnType);
                result = getString.Invoke(instance, null);
                dbconnection = result!.ToString();
            }


            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(dbconnection);
        }

        public DbSet<TestTable> TestTable => Set<TestTable>();
    }
}
