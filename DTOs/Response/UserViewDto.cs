using HRM_Project.Models.Common;

namespace HRM_Project.DTOs.Response
{
    public class UserViewDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public bool isActived {  get; set; }
    }
}
