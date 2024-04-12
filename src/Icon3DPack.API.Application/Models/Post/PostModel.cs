using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Core.Common;

namespace Icon3DPack.API.Application.Models.Post
{
    public class PostRequestModel : BaseAuditRequestModel, IOrder, ISlug
    {
        public string Name { get; set; }
        public string? Content { get; set; }
        public string? Slug { get; set; }
        public int? Order { get; set; }
    }
    public class PostResponseModel : BaseAuditResponseModel, IOrder, ISlug
    {
        public string Name { get; set; }
        public string? Content { get; set; }
        public int? Order { get; set; } 
        public string? Slug { get; set; }
    }
}
