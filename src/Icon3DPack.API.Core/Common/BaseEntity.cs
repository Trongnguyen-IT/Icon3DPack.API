namespace Icon3DPack.API.Core.Common
{
    public abstract class BaseEntity:IAuditedEntity
    {
        public Guid Id { get; set; }
        public string? CreatedBy { get ; set ; }
        public DateTime CreatedOn { get ; set ; }
        public string? UpdatedBy { get ; set ; }
        public DateTime? UpdatedOn { get ; set ; }
    }
}
