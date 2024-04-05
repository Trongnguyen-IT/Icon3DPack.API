namespace Icon3DPack.API.Application.Models.BaseModel
{
    public class BaseAuditRequestModel : BaseModel
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
    }

    public class BaseAuditResponseModel : BaseModel
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? ModifiedTime { get; set; }
        public DateTime? ModifiedBy { get; set; }
    }
}
