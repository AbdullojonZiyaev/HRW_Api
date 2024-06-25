
using AuthKeeper.DTOs.Response;
using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models;
using HRM_Project.Models.Common;
using HRM_Project.Models.Options;
using HRM_Project.Models.Types;


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

            CreateMap<Employee, EmployeeViewDto>()
           .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position.Title))
           .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Fullname))
           .ForMember(dest => dest.DivisionName, opt => opt.MapFrom(src => src.Division.Fullname))
           .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Fullname));
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();

            CreateMap<Position, PositionViewDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Fullname))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Fullname))
            .ForMember(dest => dest.DivisionName, opt => opt.MapFrom(src => src.Division.Fullname));
            CreateMap<PositionCreateDto, Position>();
            CreateMap<PositionUpdateDto, Position>();

            CreateMap<OrderCreateDto, Order>();
            CreateMap<Order, OrderViewDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => $"{src.Employee.FirstName} {src.Employee.LastName}"))
                .ForMember(dest => dest.OrderTypeName, opt => opt.MapFrom(src => src.OrderType.Name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Fullname))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Fullname))
                .ForMember(dest => dest.DivisionName, opt => opt.MapFrom(src => src.Division != null ? src.Division.Fullname : string.Empty));

            CreateMap<OrderType, OrderTypeViewDto>();
            CreateMap<OrderTypeCreateDto, OrderType>();

            CreateMap<News, NewsViewDto>();
            CreateMap<NewsCreateDto, News>();
            CreateMap<NewsUpdateDto, News>();
        }
    }
}
