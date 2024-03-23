using AutoMapper;
using Icon3DPack.API.Application.Models.FileType;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Icon3DPack.API.Host.Controllers
{
    public class FileTypeController : BaseAPIController<FileType, FileTypeRequestModel, FileTypeResponseModel>
    {
        public FileTypeController(IFileTypeService baseService, IMapper mapper) : base(baseService, mapper)
        {
        }
    }
}
