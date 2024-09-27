using Backend.Core.Domain.Common;

namespace Backend.Core.Domain.Entities
{
    public class Ingredient : AuditableBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
