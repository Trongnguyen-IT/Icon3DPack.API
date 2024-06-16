using AutoMapper;
using Icon3DPack.API.Application.Models;
using Icon3DPack.API.Application.Models.FileExtension;
using Icon3DPack.API.Application.Models.Paging;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Icon3DPack.API.Host.Controllers
{
    public class FileExtensionController : BaseAPIController<FileExtension, FileExtensionRequestModel, FileExtensionResponseModel>
    {
        private readonly IFileExtensionService _fileExtensionService;
        public FileExtensionController(IFileExtensionService  fileExtensionService, IMapper mapper) : base(fileExtensionService, mapper)
        {
            _fileExtensionService = fileExtensionService;
        }


        [HttpPost("extensions")]
        public async Task<IActionResult> GetAll(BaseFilterDto filter)
        {
            return Ok(ApiResult<PaginationResult<FileExtensionResponseModel>>.Success(await _fileExtensionService.GetAll(filter)));
        }
    }
}
