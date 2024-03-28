using Icon3DPack.API.Application.Models.BaseModel;

namespace Icon3DPack.API.Application.Models.Category
{
    public class CategoryRequestModel : BaseRequestModel
    {
        public required string Name { get; set; }
        public string? ThumbnailUrl { get; set; }
        public int ProductAmount { get; set; }
    }

    public class CategoryResponseModel : BaseResponseModel
    {
        public required string Name { get; set; }
        public string? ThumbnailUrl { get; set; }
        public int ProductAmount { get; set; }
    }
}
