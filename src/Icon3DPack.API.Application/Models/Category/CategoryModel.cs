using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Application.Models.Tag;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.Models.Category
{
    public class CategoryRequestModel : BaseAuditRequestModel, IOrder, ISlug
    {
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int ProductAmount { get; set; }
        public int? Order { get; set; }
        public string? Slug { get; set; }
        public List<Guid>? TagIds { get; set; }
    }

    public class CategoryResponseModel : BaseAuditResponseModel, IOrder, ISlug
    {
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int ProductAmount { get; set; }
        public int? Order { get; set; }
        public string? Slug { get; set; }
        public List<TagResponseModel>? Tags { get; set; }
    }
}
