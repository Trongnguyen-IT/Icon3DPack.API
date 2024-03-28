using Icon3DPack.API.Core.Common;

namespace Icon3DPack.API.Core.Entities
{
    public class Post : BaseEntity
    {
        public required string Name { get; set; }
        public string? Content { get; set; }
    }
}
