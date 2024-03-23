using Icon3DPack.API.Application.Models.BaseModel;

namespace Icon3DPack.API.Application.Models.TodoList;

public class TodoListResponseModel : BaseResponseModel
{
    public string? Title { get; set; }
}
