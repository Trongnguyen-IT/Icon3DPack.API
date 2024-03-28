using Icon3DPack.API.Application.Models.BaseModel;

namespace Icon3DPack.API.Application.Models.FileType
{
    public class FileTypeRequestModel : BaseRequestModel
    {
        public required string Name { get; set; }
        public required string FileTypeCode { get; set; }
        public string? ThumbnailUrl { get; set; }
    }

    public class FileTypeResponseModel : BaseResponseModel
    {
        public required string Name { get; set; }
        public required string FileTypeCode { get; set; }
        public string? ThumbnailUrl { get; set; }
    }
}
