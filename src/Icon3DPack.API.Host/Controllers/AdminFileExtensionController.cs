using AutoMapper;
using Icon3DPack.API.Application.Models.FileExtension;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Host.Controllers
{
    public class AdminFileExtensionController : AdminBaseController<FileExtension, FileExtensionRequestModel, FileExtensionResponseModel>
    {
        public AdminFileExtensionController(IFileExtensionService fileExtensionService, IMapper mapper) : base(fileExtensionService, mapper)
        {
        }
    }
}
