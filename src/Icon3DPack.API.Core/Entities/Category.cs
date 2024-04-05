using Icon3DPack.API.Core.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Icon3DPack.API.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int ProductAmount { get; set; }

        public virtual List<Product>? Products { get; set; }
    }
}
