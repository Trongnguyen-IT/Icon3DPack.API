using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(IBaseRepository<Category> baseRepository) : base(baseRepository)
        {
        }

        //public override Task<List<Category>> GetAllAsync()
        //{
        //    return base.GetAllAsync();
        //}
    }
}
