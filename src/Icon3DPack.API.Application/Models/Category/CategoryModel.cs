using Icon3DPack.API.Application.Models.BaseModel;

namespace Icon3DPack.API.Application.Models.Category
{
    public class CategoryRequestModel : BaseAuditRequestModel
    {
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int ProductAmount { get; set; }
    }

    public class CategoryResponseModel : BaseAuditResponseModel
    {
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int ProductAmount { get; set; }
    }
}
