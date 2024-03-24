using Icon3DPack.API.Application.Models.BaseModel;

namespace Icon3DPack.API.Application.Models.Category
{
    public class CategoryResponseModel : BaseResponseModel
    {
        public string Name { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
