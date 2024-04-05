using AutoMapper;
using Icon3DPack.API.Application.Models.FileExtension;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.MappingProfiles
{
    public class FileExtensionProfile : Profile
    {
        public FileExtensionProfile()
        {
            CreateMap<FileExtensionRequestModel, FileExtension>();

            CreateMap<FileExtension, FileExtensionResponseModel>();
        }
    }
}
