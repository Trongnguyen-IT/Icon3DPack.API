using Icon3DPack.API.Application.Models.Paging;

namespace Icon3DPack.API.Application.Models.Product
{
    public class ProductFilter : BaseFilterDto
    {
        public Guid? CategoryId { get; set; }
    }
}
