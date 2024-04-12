using Icon3DPack.API.Core.Common;

namespace Icon3DPack.API.Core.Entities
{
    public class Post : BaseEntity,IOrder, ISlug
    {
        public string Name { get; set; }
        public string? Content { get; set; }
        public string? Slug { get; set; }
        public int? Order { get; set; }
    }
}
