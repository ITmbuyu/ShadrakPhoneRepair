using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ShadrackPhoneRepair.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName) { }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<DeviceDescription> DeviceDescriptions { get; set; }
        public DbSet<DeviceProblem> DeviceProblems { get; set; }
        public DbSet<DeviceStatus> DeviceStatuses { get; set; }
        public DbSet<DeviceStatusWalkIns> DeviceStatusesWalkIns { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestPayments> requestPayments { get; set; }
        public DbSet<RepairStatus> RepairStatuses { get; set; }
        public DbSet<WalkInPayments> WalkInPayments { get; set; }
        public DbSet<WalkInRequest> WalkInRequests { get; set; }
        public DbSet<WalkInTimes> WalkInTimes { get; set; }
        public DbSet<Employee> Employee { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ShadrackPhoneRepair.Models.Storage> Storages { get; set; }


        public System.Data.Entity.DbSet<ShadrackPhoneRepair.Models.PaymentStatus> PaymentStatus { get; set; }
    }
}