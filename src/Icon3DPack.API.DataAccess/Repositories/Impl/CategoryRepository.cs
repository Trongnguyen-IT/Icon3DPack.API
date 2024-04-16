using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.Core.Exceptions;
using Icon3DPack.API.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Icon3DPack.API.DataAccess.Repositories.Impl
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly DatabaseContext _context;

        public CategoryRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<List<Category>> GetAllAsync(Expression<Func<Category, bool>> predicate)
        {
            return await _context.Categories.Include(p => p.CategoryTags).ThenInclude(p => p.Tag).ToListAsync();
        }

        public override Task<Category> GetFirstAsync(Expression<Func<Category, bool>> predicate)
        {
            var entity = _context.Categories.Include(p => p.CategoryTags).ThenInclude(p => p.Tag).Where(predicate).FirstOrDefaultAsync();

            if (entity == null) throw new ResourceNotFoundException(typeof(Category));

            return entity;
        }

        public async Task UpdateCategoryTagsAsync(Guid id)
        {
            var category = await _context.Categories
                .Include(p => p.CategoryTags)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (category == null)
            {
                throw new ResourceNotFoundException(typeof(Category));
            }

            category.CategoryTags.Clear();

            await _context.SaveChangesAsync();
        }
    }
}
