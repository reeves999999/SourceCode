using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SourceCode.Web.Domain.Entities;
using SourceCode.Web.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SourceCode.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly IUserService _userService;

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserService userService)
            : base(options)
        {
            _userService = userService;
        }

        public virtual DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Client>()
                .HasIndex(x => x.Name)
                .IsUnique();

        }

        


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            var modified = ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted);

            //TODO - make sure this is testable
            var currentUser = _userService.GetLoggedInUserId();

            foreach (var item in modified)
            {
                if (item.Entity is IAuditableEntity entity)
                {
                    item.CurrentValues[nameof(IAuditableEntity.UpdatedBy)] = currentUser;
                    item.CurrentValues[nameof(IAuditableEntity.DateUpdated)] = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
