namespace Icon3DPack.API.Application.Models.BaseModel
{
    public class BaseRequestModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
    public class BaseResponseModel
    {
        public Guid Id { get; set; }
    }
}
