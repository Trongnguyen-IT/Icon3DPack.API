using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Repositories;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class FileTypeService : BaseService<FileExtension>, IFileTypeService
    {
        private readonly IFileTypeRepository _fileTypeRepository;

        public FileTypeService(IFileTypeRepository fileTypeRepository) : base(fileTypeRepository)
        {
            _fileTypeRepository = fileTypeRepository;
        }
    }
}
