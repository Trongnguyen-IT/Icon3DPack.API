using AutoMapper;
using Icon3DPack.API.Application.Models.FileExtension;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Host.Controllers
{
    public class FileExtensionController : BaseAPIController<FileExtension, FileExtensionRequestModel, FileExtensionResponseModel>
    {
        public FileExtensionController(IFileTypeService baseService, IMapper mapper) : base(baseService, mapper)
        {
        }
    }
}
