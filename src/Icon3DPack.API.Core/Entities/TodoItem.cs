using Icon3DPack.API.Core.Common;

namespace Icon3DPack.API.Core.Entities
{
    public class TodoItem : BaseEntity, IAuditedEntity
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public bool IsDone { get; set; }

        public virtual TodoList List { get; set; }
    }
}
