using MyApplication.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MyApplication.DAL
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]  // This will cause controller/view scoffolding to fail.  Create scaffolding first, then add...
    public class MyDbContext : DbContext
    {

        public MyDbContext() : base("MyDbContextConnectionString")
        {
            Database.SetInitializer<MyDbContext>(new MyDbInitializer());
        }


        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}