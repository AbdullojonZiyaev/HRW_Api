
using AuthKeeper.DTOs.Response;
using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models.Common;
using HRM_Project.Models.Options;


namespace AuthKeeper.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreateDto, User> ();
            CreateMap<UserUpdateDto, User> ();
            CreateMap<RoleCreateDto, Role> ();

            CreateMap<User, UserViewDto> ()
                   .ForMember (x => x.RoleName, y => y.MapFrom (c => c.Role.Name));

            CreateMap<User, UserInfoDto> ()
                    .ForMember (x => x.RoleName, y => y.MapFrom (c => c.Role.Name))
                    .ForMember (x => x.Functionals, y => y.MapFrom (c => c.Role.Functionals))
                    .ForMember (x => x.CompanyName, y => y.MapFrom (c => c.Company.Fullname)); 

            CreateMap<User, BaseUser> ();
            CreateMap<Role, RoleViewDto> ();

            CreateMap<CompanyCreateDto, Company> ();
            CreateMap<CompanyUpdateDto, Company> ();
            CreateMap<Company, CompanyViewDto> ().ForMember (a => a.CityName, b => b.MapFrom (c => c.City.Name));

            CreateMap<CityCreateDto, City> ();
            CreateMap<CityUpdateDto, City> ();
            CreateMap<City, CityViewDto> ();

            CreateMap<Department, DepartmentViewDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Fullname));
            CreateMap<DepartmentCreateDto, Department>();
            CreateMap<DepartmentUpdateDto, Department>();

            CreateMap<Division, DivisionViewDto>()
           .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Fullname));
            CreateMap<DivisionCreateDto, Division>();
            CreateMap<DivisionUpdateDto, Division>();
        }
    }
}
