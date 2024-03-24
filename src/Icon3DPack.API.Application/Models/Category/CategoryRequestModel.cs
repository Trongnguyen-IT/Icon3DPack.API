using Icon3DPack.API.Application.Models.BaseModel;

namespace Icon3DPack.API.Application.Models.Category
{
    public class CategoryRequestModel : BaseRequestModel
    {
        public string Name { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
