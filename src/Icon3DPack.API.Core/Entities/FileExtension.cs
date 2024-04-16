using Icon3DPack.API.Core.Common;

namespace Icon3DPack.API.Core.Entities
{
    public class FileExtension : BaseEntity, IOrder
    {
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int? Order { get; set; }
        public bool IsPublish { get; set; }

        public virtual ICollection<FileEntity> FileEntities { get; set; } = new List<FileEntity>();
    }
}
