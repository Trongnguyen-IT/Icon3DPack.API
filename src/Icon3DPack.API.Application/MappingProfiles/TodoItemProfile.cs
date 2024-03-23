using AutoMapper;
using Icon3DPack.API.Application.Models.TodoItem;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.MappingProfiles;

public class TodoItemProfile : Profile
{
    public TodoItemProfile()
    {
        CreateMap<CreateTodoItemModel, TodoItem>()
            .ForMember(ti => ti.IsDone, ti => ti.MapFrom(cti => false));

        CreateMap<UpdateTodoItemModel, TodoItem>();

        CreateMap<TodoItem, TodoItemResponseModel>();
    }
}
