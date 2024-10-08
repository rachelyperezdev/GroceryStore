using Backend.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence.Interceptor
{
    public class AuditableInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData, 
            int result, 
            CancellationToken cancellationToken = default)
        {

            if(eventData is null)
            {
                return base.SavedChangesAsync(eventData, result, cancellationToken);
            }

            IEnumerable<EntityEntry<AuditableBaseEntity>> entityEntries =
                eventData.
                    Context.
                    ChangeTracker.
                    Entries<AuditableBaseEntity>();

            foreach(EntityEntry<AuditableBaseEntity> entry in entityEntries)
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "DefaultUser";
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = "DefaultUser";
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.DeletedBy = "DefaultUser";
                        entry.Entity.DeletedAt = DateTime.UtcNow;
                        break;
                }
            }

            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
    }
}
