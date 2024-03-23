using FluentValidation;
using Icon3DPack.API.Application.Models.TodoList;

namespace Icon3DPack.API.Application.Models.Validators.TodoList;

public class CreateTodoListModelValidator : AbstractValidator<CreateTodoListModel>
{
    public CreateTodoListModelValidator()
    {
        RuleFor(ctl => ctl.Title)
            .MinimumLength(TodoListValidatorConfiguration.MinimumTitleLength)
            .WithMessage(
                $"Todo list title must contain a minimum of {TodoListValidatorConfiguration.MinimumTitleLength} characters")
            .MaximumLength(TodoListValidatorConfiguration.MaximumTitleLength)
            .WithMessage(
                $"Todo list title must contain a maximum of {TodoListValidatorConfiguration.MaximumTitleLength} characters");
    }
}
