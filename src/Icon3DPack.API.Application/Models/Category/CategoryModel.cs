using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Application.Models.Tag;
using Icon3DPack.API.Core.Common;

namespace Icon3DPack.API.Application.Models.Category
{
    public class CategoryRequestModel : BaseAuditRequestModel, ISlug
    {
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Slug { get; set; }
        public List<TagRequestModel>? Tags { get; set; } = new List<TagRequestModel>();
    }

    public class CategoryResponseModel : BaseAuditResponseModel, ISlug
    {
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Slug { get; set; }
        public int? ProductAmount { get; set; }
        public List<TagResponseModel>? Tags { get; set; } = new List<TagResponseModel>();
    }
}
