using Icon3DPack.API.Application.Models.BaseModel;

namespace Icon3DPack.API.Application.Models.Product
{
    public class ProductRequestModel : BaseAuditRequestModel
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsShow { get; set; }
        public string[]? ShowTypes { get; set; }
        public int? CategoryId { get; set; }
    }

    public class ProductResponseModel : BaseAuditResponseModel
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsShow { get; set; }
        public string[]? ShowTypes { get; set; }
        public int? CategoryId { get; set; }
        public int? CategoryName { get; set; }

    }
}
