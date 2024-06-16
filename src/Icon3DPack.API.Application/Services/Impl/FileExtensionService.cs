using AutoMapper;
using Icon3DPack.API.Application.Helpers;
using Icon3DPack.API.Application.Models.FileExtension;
using Icon3DPack.API.Application.Models.Paging;
using Icon3DPack.API.Application.Models.Product;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.DataAccess.Repositories;
using Icon3DPack.API.DataAccess.Repositories.Impl;
using Microsoft.EntityFrameworkCore;

namespace Icon3DPack.API.Application.Services.Impl
{
    public class FileExtensionService : BaseService<FileExtension>, IFileExtensionService
    {
        private readonly IFileExtensionRepository _fileTypeRepository;
        private readonly IMapper _mapper;

        public FileExtensionService(IFileExtensionRepository fileTypeRepository, IMapper mapper) : base(fileTypeRepository)
        {
            _fileTypeRepository = fileTypeRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResult<FileExtensionResponseModel>> GetAll(BaseFilterDto filter)
        {
            var query = _fileTypeRepository
                 .GetAll()
                 .WhereIf(filter.Keyword.IsNotNullOrEmpty(), p => p.Name.ToLower().Contains(filter.Keyword!.ToLower()));

            var totalCount = await query.CountAsync();

            var items = totalCount > 0 ?
               _mapper.Map<IReadOnlyList<FileExtensionResponseModel>>(await query.OrderAndPaging(filter).ToListAsync())
                : new List<FileExtensionResponseModel>();

            return new PaginationResult<FileExtensionResponseModel>(items, filter.PageNumber ?? 1, filter.PageSize ?? 200, totalCount);
        }

        public override Task<IReadOnlyList<FileExtension>> GetAllAsync()
        {
            return _fileTypeRepository.GetAllAsync(orderBy: p => p.OrderByDescending(pp => pp.Order));
        }
    }
}
