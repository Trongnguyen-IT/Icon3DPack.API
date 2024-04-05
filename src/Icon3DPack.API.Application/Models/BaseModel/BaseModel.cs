namespace Icon3DPack.API.Application.Models.BaseModel
{
    public class BaseRequestModel : BaseModel { }
    public class BaseResponseModel : BaseModel { }

    public class BaseModel
    {
        public Guid Id { get; set; }
    }
}
