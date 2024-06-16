namespace Icon3DPack.API.Application.Models.Paging
{
    public class BaseFilterDto
    {
        public string? Keyword { get; set; }
        public bool? Status { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        public int SkipCount => ((PageNumber ?? 1) - 1) * (PageSize ?? 0);
        public string? SortBy { get; set; } = "CreatedTime";
        public string? SortDirection { get; set; } = "desc";
    }
}
