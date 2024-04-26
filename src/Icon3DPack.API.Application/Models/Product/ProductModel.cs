using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Application.Models.FileEntity;
using Icon3DPack.API.Application.Models.Tag;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.Models.Product
{
    public class ProductRequestModel : BaseAuditRequestModel, ISlug
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsPublish { get; set; }
    
        public Guid CategoryId { get; set; }
        public string? Slug { get; set; }
        public List<TagRequestModel>? Tags { get; set; } = new List<TagRequestModel>();
        public List<FileEntityRequestModel>? FileEntities { get; set; } = new List<FileEntityRequestModel>();
    }

    public class ProductResponseModel : BaseAuditResponseModel, ISlug
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsPublish { get; set; }
        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Slug { get; set; }
        public List<TagResponseModel>? Tags { get; set; } = new List<TagResponseModel>();
        public List<FileEntityResponseModel>? FileEntities { get; set; } = new List<FileEntityResponseModel>();
    }
}
