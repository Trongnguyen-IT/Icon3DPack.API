using Icon3DPack.API.Core.Common;

namespace Icon3DPack.API.Core.Entities
{
    public class Category : BaseEntity, ISlug
    {
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Slug { get; set; }

        public virtual ICollection<Product> Products { get; set; } = [];

        public virtual ICollection<Tag> Tags { get; set; } = [];
        public virtual ICollection<CategoryTag> CategoryTags { get; set; } = [];
    }
}
