using ALPHII.Models.Domain;
using ALPHII.Models.DTO;
using AutoMapper;

namespace ALPHII.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //CreateMap<UserDTO, UserDomain>()
            //    .ForMember(x => x.Name, opt => opt.MapFrom(x => x.FullName))
            //    .ReverseMap();

            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();

            CreateMap<DifficultyDto, Difficulty>().ReverseMap();

            CreateMap<ProjectDto, Project>().ReverseMap();
            CreateMap<VmProjectDto, VMProject>().ReverseMap();
            CreateMap<UpdateVmProjectRequestDto, Project>().ReverseMap();
            CreateMap<UpdateVmProjectRequestDto,Project>().ReverseMap();

            CreateMap<UpdateToolRequestDto, Tool>().ReverseMap();

            CreateMap<ToolDto, Tool>().ReverseMap();

            CreateMap<AddToolRequestDto, Tool>().ReverseMap();

            CreateMap<UpdateToolRequestDto, Tool>().ReverseMap();
            CreateMap<PlanDto, Plan>().ReverseMap();

        }
    }

    //public class UserDTO
    //{
    //    public string FullName { get; set; }
    //}

    //public class UserDomain
    //{
    //    public string Name { get; set; } 
    //}
}
