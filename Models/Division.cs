using HRM_Project.Models.Abstraction;

namespace HRM_Project.Models
{
    public class Division : DbRecord
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string HeadOfDivision { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = [];
        public virtual ICollection<Vacancy> Vacancies { get; set; }
    }
}
