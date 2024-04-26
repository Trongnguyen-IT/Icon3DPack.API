using AutoMapper;
using Icon3DPack.API.Application.Models.FileEntity;
using Icon3DPack.API.Core.Entities;

namespace Icon3DPack.API.Application.MappingProfiles
{
    public class FileEntityProfile : Profile
    {
        public FileEntityProfile()
        {
            CreateMap<FileEntityRequestModel, FileEntity>();
            CreateMap<FileEntity, FileEntityResponseModel>();
        }
    }
}