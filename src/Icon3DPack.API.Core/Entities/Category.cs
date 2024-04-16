using Icon3DPack.API.Core.Common;

namespace Icon3DPack.API.Core.Entities
{
    public class Category : BaseEntity, ISlug
    {
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int ProductAmount { get; set; }
        public string? Slug { get; set; }

        public virtual List<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<CategoryTag> CategoryTags { get; set; } = new HashSet<CategoryTag>();
    }
}
