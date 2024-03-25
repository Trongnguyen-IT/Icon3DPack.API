using Icon3DPack.API.Core.Common;

namespace Icon3DPack.API.Core.Entities
{
    public class TodoList : BaseEntity, IAuditedEntity
    {
        public string Title { get; set; }

        public List<TodoItem> Items { get; } = new List<TodoItem>();
      
    }
}
