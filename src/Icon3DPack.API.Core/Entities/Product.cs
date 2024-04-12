using Icon3DPack.API.Core.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Icon3DPack.API.Core.Entities
{
    public class Product : BaseEntity, ISlug
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsShow { get; set; }
        public string[]? ShowTypes { get; set; }

        [ForeignKey(nameof(Category))]
        public Guid? CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public virtual List<FileEntity>? FileEntities { get; set; }
        public string? Slug { get; set; }
    }
}
