using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Persistence;

namespace Icon3DPack.API.DataAccess.Repositories.Impl
{
    public class FileTypeRepository: BaseRepository<FileType> , IFileTypeRepository
    {
        public FileTypeRepository(DatabaseContext context) : base(context) { }
    }
}
