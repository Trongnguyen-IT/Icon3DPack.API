using Icon3DPack.API.DataAccess.Persistence;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.DataAccess.Repositories.Impl;

public class TodoListRepository : BaseRepository<TodoList>, ITodoListRepository
{
    public TodoListRepository(DatabaseContext context) : base(context) { }
}
