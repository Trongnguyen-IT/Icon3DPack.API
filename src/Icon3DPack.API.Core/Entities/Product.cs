﻿using Icon3DPack.API.Core.Common;

namespace Icon3DPack.API.Core.Entities
{
    public class Product : BaseEntity, ISlug
    {
        public string Name { get; set; }
        //public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsPublish { get; set; } = true;
        public string? Slug { get; set; }
        public long DownloadCount { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<FileEntity> FileEntities { get; set; } = [];
        public virtual ICollection<ProductTag> ProductTags { get; set; } = [];
    }
}
