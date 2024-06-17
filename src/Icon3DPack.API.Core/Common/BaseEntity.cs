namespace Icon3DPack.API.Core.Common
{
    public abstract class BaseEntity : IAuditedEntity
    {
        public Guid Id { get; set; }
        public string? CreatedBy { get; set; } 
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedTime { get; set; } = DateTime.UtcNow;
    }
}
