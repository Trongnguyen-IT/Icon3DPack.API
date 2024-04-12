using Icon3DPack.API.Core.Common;

namespace Icon3DPack.API.Core.Entities
{
    public class FileExtension : BaseEntity,IOrder
    {
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public virtual FileEntity? FileEntity { get; set; }
        public int? Order { get; set; }
    }
}
