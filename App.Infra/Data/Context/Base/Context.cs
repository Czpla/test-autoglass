namespace App.Infra.Data.Context.Base
{
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;
    using Core.Domain.Entities;
    using Core.Domain.Entities.Base;
    using System.Threading.Tasks;
    using System.Threading;

    public class ContextBase : DbContext
    {
        public DbSet<Product> Product { get; set; } = default!;

        public ContextBase() : base() { }
        public ContextBase(DbContextOptions<ContextBase> options) : base(options) { }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Entity>())
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.Modify();
                        break;
                }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
