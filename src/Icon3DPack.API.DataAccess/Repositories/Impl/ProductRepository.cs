using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.Core.Exceptions;
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

        public override async Task<Product> GetFirstAsync(Expression<Func<Product, bool>> predicate)
        {
            var entity = await _context.Products.Include(p => p.Category).Where(predicate).FirstOrDefaultAsync();

            if (entity == null) throw new ResourceNotFoundException(typeof(Product));

            return entity;
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

        public async Task<PaginatedList<Product>> ProductFilter(string? name, string? categoryId, string? sortOrder, int? pageNumber, int? pageSize)
        {
            var query = _context.Products.AsQueryable().Where(p => (string.IsNullOrEmpty(name) || p.Name.ToLower() == name.ToLower())
            && (string.IsNullOrEmpty(categoryId) || p.CategoryId.ToString() == categoryId));

            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "name_asc":
                        query = query.OrderBy(p => p.Name);
                        break;
                    case "name_desc":
                        query = query.OrderByDescending(p => p.Name);
                        break;
                    case "date_asc":
                        query = query.OrderBy(p => p.CreatedTime);
                        break;
                    case "date_desc":
                        query = query.OrderByDescending(p => p.CreatedTime);
                        break;
                    default:
                        query = query.OrderByDescending(p => p.CreatedTime);
                        break;
                }
            }

            return await PaginatedList<Product>.CreateAsync(query, pageNumber ?? 1, pageSize ?? 200);
        }
    }
}
