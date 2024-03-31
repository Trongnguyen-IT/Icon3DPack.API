using Icon3DPack.API.Core.Common;

namespace Icon3DPack.API.Core.Entities
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int ProductAmount { get; set; }

        public virtual ICollection<Product> Products { get; }
    }
}
