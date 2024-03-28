using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Core.Enums;

namespace Icon3DPack.API.Application.Models.Product
{
    public class ProductRequestModel : BaseRequestModel
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsShow { get; set; }
        public FileTypeEnum[]? ShowTypes { get; set; }
        public int? CategoryId { get; set; }
    }

    public class ProductResponseModel : BaseResponseModel
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsShow { get; set; }
        public FileTypeEnum[]? ShowTypes { get; set; }
        public int? CategoryId { get; set; }
    }
}
