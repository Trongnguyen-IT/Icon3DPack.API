using Icon3DPack.API.Application.Models.BaseModel;

namespace Icon3DPack.API.Application.Models.Post
{
    public class PostRequestModel : BaseAuditRequestModel
    {
        public required string Name { get; set; }
        public string? Content { get; set; }
    }
    public class PostResponseModel : BaseAuditResponseModel
    {
        public required string Name { get; set; }
        public string? Content { get; set; }
    }
}
