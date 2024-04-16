using Icon3DPack.API.Application.Models.BaseModel;

namespace Icon3DPack.API.Application.Models.Tag
{
    public class TagRequestModel : BaseAuditRequestModel
    {
        public string Name { get; set; }
    }

    public class TagResponseModel : BaseAuditResponseModel
    {
        public string Name { get; set; }
    }
}
