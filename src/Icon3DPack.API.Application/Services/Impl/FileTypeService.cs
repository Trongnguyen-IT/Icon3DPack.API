using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class FileTypeService : BaseService<FileType>, IFileTypeService
    {
        public FileTypeService(IBaseRepository<FileType> baseRepository) : base(baseRepository)
        {
        }
    }
}
