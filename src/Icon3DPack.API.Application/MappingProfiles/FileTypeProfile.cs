using AutoMapper;
using Icon3DPack.API.Application.Models.FileType;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.MappingProfiles
{
    public class FileTypeProfile : Profile
    {
        public FileTypeProfile()
        {
            CreateMap<FileTypeRequestModel, FileType>();

            CreateMap<FileType, FileTypeResponseModel>();
        }
    }
}
