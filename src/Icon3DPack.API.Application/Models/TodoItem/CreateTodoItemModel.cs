using Icon3DPack.API.Application.Models;

namespace Icon3DPack.API.Application.Models.TodoItem;

public class CreateTodoItemModel
{
    public Guid TodoListId { get; set; }

    public string Title { get; set; }

    public string Body { get; set; }
}

public class CreateTodoItemResponseModel : BaseResponseModel { }
