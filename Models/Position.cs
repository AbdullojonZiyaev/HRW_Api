using HRM_Project.Models.Abstraction;

namespace HRM_Project.Models
{
    public class Position : DbRecord
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Salary { get; set; }
        public string EmploymentType { get; set; }
        public string Responsibilities { get; set; }
        public string Requirements { get; set; }
        public string Qualifications { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public int DivisionId { get; set; }
        public virtual Division Division { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
