using Icon3DPack.API.Application.Models.FileExtension;
using Icon3DPack.API.Application.Models.Paging;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.Services
{
    public interface IFileExtensionService : IBaseService<FileExtension>
    {
        Task<PaginationResult<FileExtensionResponseModel>> GetAll(BaseFilterDto filter);
    }
}
