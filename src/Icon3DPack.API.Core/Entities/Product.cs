using Icon3DPack.API.Core.Common;

namespace Icon3DPack.API.Core.Entities
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsShow { get; set; }
        public string[]? ShowTypes { get; set; }

        public Guid CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
