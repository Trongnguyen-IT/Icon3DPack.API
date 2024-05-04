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

        public override Task<IReadOnlyList<FileExtension>> GetAllAsync()
        {
            return _fileTypeRepository.GetAllAsync(orderBy: p => p.OrderByDescending(pp => pp.Order));
        }
    }
}
