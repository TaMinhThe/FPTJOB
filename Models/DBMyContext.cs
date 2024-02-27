using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FPTJOB.Models
{
    public class DBMyContext : IdentityDbContext
    {
        public DBMyContext(DbContextOptions<DBMyContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ApplyJob> ApplyJobs { get; set; }
        public DbSet<Profile> Profiles { get; set; }

    }
}
