using Icon3DPack.API.Application.Models.BaseModel;

namespace Icon3DPack.API.Application.Models.Post
{
    public class PostRequestModel : BaseRequestModel
    {
        public required string Name { get; set; }
        public string? Content { get; set; }
    }
    public class PostResponseModel : BaseResponseModel
    {
        public required string Name { get; set; }
        public string? Content { get; set; }
    }
}
