using Backend.Core.Domain.Common;
using Backend.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> context) : base(context) { }
        
        public DbSet<Ingredient> Ingredients { get; set;}

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.Now;
                        entry.Entity.UpdatedBy = "DefaultUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Table
            modelBuilder.Entity<Ingredient>().ToTable("Ingredients", tableBuilder =>
            {
                tableBuilder.HasCheckConstraint("CK_Ingredient_Price", "Price > 0");
                tableBuilder.HasCheckConstraint("CK_Ingredient_Stock", "Stock >= 0");
            });
            #endregion

            #region Primary Key
            modelBuilder.Entity<Ingredient>().HasKey(i => i.Id);
            #endregion

            #region Relationships
            #endregion

            #region Properties
            modelBuilder.Entity<Ingredient>()
                .Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(300);

            modelBuilder.Entity<Ingredient>()
                .Property(i => i.Description)
                .IsRequired()
                .HasMaxLength(1000);

            modelBuilder.Entity<Ingredient>()
                .Property(i => i.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Ingredient>()
                .Property(i => i.Stock)
                .IsRequired();
            #endregion

            #region Indexes
            modelBuilder.Entity<Ingredient>()
                .HasIndex(i => i.Id)
                .IsUnique()
                .HasDatabaseName("IX_Ingredient_Id");

            modelBuilder.Entity<Ingredient>()
                .HasIndex(i => i.Name)
                .HasDatabaseName("IX_Ingredient_Name");
            #endregion
        }
    }
}
