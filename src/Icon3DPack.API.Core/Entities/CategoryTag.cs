﻿namespace Icon3DPack.API.Core.Entities
{
    public class CategoryTag
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }

}
