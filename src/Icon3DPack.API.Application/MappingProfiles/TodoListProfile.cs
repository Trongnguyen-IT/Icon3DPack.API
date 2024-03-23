using AutoMapper;
using Icon3DPack.API.Application.Models.TodoList;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.MappingProfiles;

public class TodoListProfile : Profile
{
    public TodoListProfile()
    {
        CreateMap<CreateTodoListModel, TodoList>();

        CreateMap<TodoList, TodoListResponseModel>();
    }
}
