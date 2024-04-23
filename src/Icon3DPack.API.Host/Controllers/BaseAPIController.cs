using AutoMapper;
using Icon3DPack.API.Application.Models;
using Icon3DPack.API.Application.Models.BaseModel;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Icon3DPack.API.Host.Controllers
{
    public class BaseAPIController<TEntity, TRequest, TResponse> : ApiController
        where TEntity : BaseEntity
        where TRequest : BaseAuditRequestModel
        where TResponse : BaseAuditResponseModel
    {
        private readonly IBaseService<TEntity> _baseService;
        private readonly IMapper _mapper;

        public BaseAPIController(IBaseService<TEntity> baseService, IMapper mapper)
        {
            _baseService = baseService;
            _mapper = mapper;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            return Ok(ApiResult<IEnumerable<TResponse>>.Success(_mapper.Map<IEnumerable<TResponse>>(await _baseService.GetAllAsync())));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(ApiResult<TResponse>.Success(_mapper.Map<TResponse>(await _baseService.GetFirstAsync(x => x.Id == id))));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(TRequest createTodoListModel)
        {
            return Ok(ApiResult<TResponse>.Success(
                _mapper.Map<TResponse>(await _baseService.AddAsync(_mapper.Map<TEntity>(createTodoListModel)))));
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, TRequest updateTodoListModel)
        {
            if (id != updateTodoListModel.Id)
            {
                return BadRequest();
            }

            return Ok(ApiResult<TResponse>.Success(
                _mapper.Map<TResponse>(await _baseService.UpdateAsync(_mapper.Map<TEntity>(updateTodoListModel)))));
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Ok(ApiResult<TResponse>.Success(
                _mapper.Map<TResponse>(await _baseService.DeleteAsync(id))));
        }
    }
}
