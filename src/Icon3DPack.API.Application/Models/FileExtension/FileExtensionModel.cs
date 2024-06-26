﻿using Icon3DPack.API.Application.Models.BaseModel;

namespace Icon3DPack.API.Application.Models.FileExtension
{
    public class FileExtensionRequestModel : BaseAuditRequestModel
    {
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int? Order { get; set; }
        public bool IsPublish { get; set; }

    }

    public class FileExtensionResponseModel : BaseAuditResponseModel
    {
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsPublish { get; set; }
        public int? Order { get; set; }
    }
}
