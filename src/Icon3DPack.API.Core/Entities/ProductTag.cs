namespace Icon3DPack.API.Core.Entities
{
    public class ProductTag
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }

}
