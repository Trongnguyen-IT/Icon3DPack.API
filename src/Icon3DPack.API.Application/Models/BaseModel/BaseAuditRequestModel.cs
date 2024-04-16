namespace Icon3DPack.API.Application.Models.BaseModel
{
    public class BaseAuditRequestModel : BaseRequestModel
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
    }

    public class BaseAuditResponseModel : BaseResponseModel
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
    }
}
