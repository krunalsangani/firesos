using Microsoft.EntityFrameworkCore;
using remote_poc_webapi.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace remote_poc_webapi.Controllers
{
    // DbContext class
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<RequestLog> RequestLog { get; set; }
        public DbSet<DeployedDrones> DeployedDrones { get; set; }
        public DbSet<PatrolLocations> PatrolLocations { get; set; }

        
        public DbSet<Patrol> Patrol { get; set; }

        public DbSet<UserProfile> UserProfiles{ get; set; }
        public DbSet<DroneLocation> DroneLocation { get; set; }
        public DbSet<VIP_Request> VIP_Request{ get; set; }



    }
}
