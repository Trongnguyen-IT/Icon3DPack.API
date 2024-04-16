using Icon3DPack.API.Core.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Icon3DPack.API.Core.Entities
{
    public class FileEntity : BaseEntity
    {
        public string FileUrl { get; set; }

        public Guid FileExtensionId { get; set; }
        public virtual FileExtension FileExtension { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
