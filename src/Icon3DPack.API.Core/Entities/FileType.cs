using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Enums;

namespace Icon3DPack.API.Core.Entities
{
    public class FileType : BaseEntity
    {
        public required string Name { get; set; }
        public string? ThumbnailUrl { get; set; }
        public required string FileTypeCode { get; set; }
    }

}
