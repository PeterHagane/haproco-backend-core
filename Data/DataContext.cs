using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DotNetEnv;

namespace haproco_backend_core.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {



            //object? instance;
            //object? result = null;
            //Type? dbConnType = Type.GetType("haproco_backend_core.Properties.DbConnection");
            //MethodInfo? getString = dbConnType?.GetMethod("GetString");
            //if (dbConnType != null && getString != null)
            //{
            //    instance = Activator.CreateInstance(dbConnType);
            //    result = getString.Invoke(instance, null);
            //    dbconnection = result!.ToString();
            //}

            //dbconnection = Environment.GetEnvironmentVariable("DB_CONNECTION");

            //You need an .env file with DB_CONNECTION=yourconnectionstring here
            string? dbconnection;
            DotNetEnv.Env.Load();
            dbconnection = System.Environment.GetEnvironmentVariable("DB_CONNECTION");

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(dbconnection);
        }

        public DbSet<Entities.TestTable> testtable => base.Set<Entities.TestTable>();
    }
}
