using Icon3DPack.API.Core.Common;

namespace Icon3DPack.API.Core.Entities
{
    public class FileType : BaseEntity
    {
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
        public required string FileTypeCode { get; set; }
    }

}
