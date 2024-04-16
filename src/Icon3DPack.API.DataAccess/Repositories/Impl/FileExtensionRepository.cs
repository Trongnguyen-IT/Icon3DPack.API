using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Persistence;

namespace Icon3DPack.API.DataAccess.Repositories.Impl
{
    public class FileExtensionRepository: BaseRepository<FileExtension> , IFileExtensionRepository
    {
        public FileExtensionRepository(DatabaseContext context) : base(context) { }
    }
}
