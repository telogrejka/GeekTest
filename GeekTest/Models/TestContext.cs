using System.Data.Entity;

namespace GeekTest.Models
{
    public class TestContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<GeekTest.Models.TestContext>());

        public TestContext()
            : base("TestDataModel")
        {
        }
        public DbSet<answers> answers { get; set; }
        public DbSet<tests> tests { get; set; }
        public DbSet<questions> questions { get; set; }
        public DbSet<results> results { get; set; }
        public DbSet<ratings> ratings { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<webpages_Roles> roles { get; set; }
        public DbSet<webpages_UsersInRoles> UsersInRoles { get; set; }
    }
}
