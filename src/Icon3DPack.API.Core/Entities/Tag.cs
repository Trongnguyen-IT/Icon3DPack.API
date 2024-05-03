using Icon3DPack.API.Core.Common;

namespace Icon3DPack.API.Core.Entities
{
    public class Tag : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductTag> ProductTags { get; set; } = [];

        public virtual ICollection<CategoryTag> CategoryTags { get; set; } = [];
    }
}
