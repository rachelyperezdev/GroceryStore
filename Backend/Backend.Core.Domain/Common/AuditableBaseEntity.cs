namespace Backend.Core.Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        public string CreatedBy { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; }
        public string DeletedBy { get; set; } = string.Empty;
        public DateTime DeletedAt { get; set; }

    }
}
