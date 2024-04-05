namespace Icon3DPack.API.Core.Common
{
    public interface IAuditedEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
    }
}
