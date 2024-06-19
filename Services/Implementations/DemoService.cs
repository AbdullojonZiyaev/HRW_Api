using HRM_Project.DTOs.Request;
using HRM_Project.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace HRM_Project.Services.Implementations
{
    public class DemoService : IDemoService
    {
        readonly ApplicationDbContext context;
        readonly IUserService userService;
        readonly IRoleService roleService;
        readonly ICityService cityService;
        readonly ICompanyService companyService;

        public DemoService(
           ApplicationDbContext context,
            IUserService userService,
            IRoleService roleService,
            ICityService cityService,
            ICompanyService companyService
            )
        {
            this.context = context;
            this.userService = userService;
            this.roleService = roleService;
            this.cityService = cityService;
            this.companyService = companyService;
        }
        public async Task CreateDemoAsync()
        {
            if (await context.Users.AnyAsync())
                return;

            // add city
            await cityService.AddAsync (new CityCreateDto ()
            {
                Name = "Dushanbe"
            });
            //add company
            await companyService.AddAsync (new CompanyCreateDto ()
            {
                Fullname = "Main Company",
                Address = "Dushanbe city, Rudari street",
                Inn = "INN0001",
                Email = "MainCom@gmail.com",
                Phone = "+9920303030303",
                Director = "Director of Main Company",
                CityId = 1
            });

            // admin role
            await roleService.AddAsync(new RoleCreateDto()
            {
                Name = "Admin",
                Functionals = new string[] { "allFunctionals" }
            });
            // admin user 
            await userService.AddAsync(new UserCreateDto()
            {
                FirstName = "Administrator",
                SecondName = "Administrator",
                Surname = "Administrator",
                Username = "Admin",
                Password = "Admin123",
                Phone = "+99200000000",
                Address = "Dushanbe",
                Position = "Manager",
                CompanyId = 1,
                IsActived = true,
                RoleId = 1
            });

        }
    }
}
