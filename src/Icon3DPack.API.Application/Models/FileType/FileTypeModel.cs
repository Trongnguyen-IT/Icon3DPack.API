using Icon3DPack.API.Application.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icon3DPack.API.Application.Models.FileType
{
    public class FileTypeRequestModel: BaseRequestModel
    {
        public string? Name { get; set; }
        public string? ThumbnailUrl { get; set; }
    }

    public class FileTypeResponseModel : BaseResponseModel
    {
        public string? Name { get; set; }
        public string? ThumbnailUrl { get; set; }
    }
}
