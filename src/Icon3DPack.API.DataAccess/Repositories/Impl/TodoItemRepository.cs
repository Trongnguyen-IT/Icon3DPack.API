using Icon3DPack.API.DataAccess.Persistence;
using Icon3DPack.API.DataAccess.Repositories;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.DataAccess.Repositories.Impl;

public class TodoItemRepository : BaseRepository<TodoItem>, ITodoItemRepository
{
    public TodoItemRepository(DatabaseContext context) : base(context) { }
}
