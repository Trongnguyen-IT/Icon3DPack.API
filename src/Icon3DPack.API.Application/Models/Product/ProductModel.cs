using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Application.Models.Tag;
using Icon3DPack.API.Core.Common;

namespace Icon3DPack.API.Application.Models.Product
{
    public class ProductRequestModel : BaseAuditRequestModel, IOrder, ISlug
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsPublish { get; set; }
        public List<Guid>? TagIds { get; set; }
        public Guid CategoryId { get; set; }
        public int? Order { get; set; }
        public string? Slug { get; set; }
    }

    public class ProductResponseModel : BaseAuditResponseModel, IOrder, ISlug
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsPublish { get; set; }
        public List<TagResponseModel>? Tags { get; set; }
        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int? Order { get; set; }
        public string? Slug { get; set; }

    }
}
