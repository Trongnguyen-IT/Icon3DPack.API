using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Icon3DPack.API.DataAccess.Repositories.Impl
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly DatabaseContext _context;
        public ProductRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<List<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public override async Task<Product> AddAsync(Product entity)
        {
            entity.Id = Guid.NewGuid();
            var addedEntity = (await _context.Products.AddAsync(entity)).Entity;
            var category = await _context.Categories.FirstOrDefaultAsync(p => p.Id == entity.CategoryId);

            if (category != null)
            {
                category.ProductAmount++;
                _context.Categories.Update(category);
            }

            await Context.SaveChangesAsync();

            return addedEntity;
        }

        public override async Task<Product> DeleteAsync(Product entity)
        {
            var removedEntity = _context.Products.Remove(entity).Entity;

            var category = await _context.Categories.FirstOrDefaultAsync(p => p.Id == entity.CategoryId);

            if (category != null)
            {
                category.ProductAmount--;
                _context.Categories.Update(category);
            }

            await _context.SaveChangesAsync();

            return removedEntity;
        }
    }
}
