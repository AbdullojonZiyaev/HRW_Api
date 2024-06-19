using HRM_Project.Models.Abstraction;

namespace HRM_Project.Models.Common
{
    public class Department:DbRecord
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string HeadOfDepartment { get; set; }
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Division> Divisions { get; set; } = new List<Division> ();
    }
}
