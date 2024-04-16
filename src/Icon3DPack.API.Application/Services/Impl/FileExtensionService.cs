using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Repositories;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class FileExtensionService : BaseService<FileExtension>, IFileExtensionService
    {
        private readonly IFileExtensionRepository _fileTypeRepository;

        public FileExtensionService(IFileExtensionRepository fileTypeRepository) : base(fileTypeRepository)
        {
            _fileTypeRepository = fileTypeRepository;
        }
    }
}
