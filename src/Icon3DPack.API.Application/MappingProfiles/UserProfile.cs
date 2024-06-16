using AutoMapper;
using Icon3DPack.API.Application.Models.User;
using Icon3DPack.API.DataAccess.Identity;

namespace Icon3DPack.API.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserModel, ApplicationUser>();
        CreateMap<UpdateUserModel, ApplicationUser>();
        CreateMap<UserRequestModel, ApplicationUser>();
        CreateMap<ApplicationUser, UserResponsetModel>();
        CreateMap<ApplicationUser, ProfileResponseModel>();
    }
}
