using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Application.Models.Tag;
using Icon3DPack.API.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icon3DPack.API.Application.Models.FileEntity
{
    public class FileEntityRequestModel : BaseAuditRequestModel
    {
        public string Name { get; set; }
        public string FileUrl { get; set; }
        public Guid FileExtensionId { get; set; }
        public Guid ProductId { get; set; }
    }

    public class FileEntityResponseModel : BaseAuditResponseModel
    {
        public string Name { get; set; }
        public string FileUrl { get; set; }
        public Guid FileExtensionId { get; set; }
        public Guid ProductId { get; set; }
    }
}
