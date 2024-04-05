using AutoMapper;
using Icon3DPack.API.Application.Models.FileExtension;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Host.Controllers
{
    public class AdminFileExtensionController : AdminBaseController<FileExtension, FileExtensionRequestModel, FileExtensionResponseModel>
    {
        public AdminFileExtensionController(IBaseService<FileExtension>  baseService, IMapper mapper) : base(baseService, mapper)
        {
        }
    }
}
