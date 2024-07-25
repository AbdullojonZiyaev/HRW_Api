using HRM_Project.Models.Abstraction;

namespace HRM_Project.Models.Common
{
        public class User : DbRecord
        {
            public string FirstName { get; set; }
            public string SecondName { get; set; }
            public string Surname { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string Position { get; set; }
            public int? RoleId { get; set; }
            public virtual Role Role { get; set; }
            public int? CompanyId { get; set; }
            public Company Company { get; set; }
            public bool IsActived { get; set; } = true;
        }
}
