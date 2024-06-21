using HRM_Project.Models.Abstraction;

namespace HRM_Project.Models
{
    public class Vacancy : DbRecord
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Responsibilities { get; set; }
        public string Requirements { get; set; }
        public string Qualifications { get; set; }
        public double Salary { get; set; } // Зарплата
        public DateTime DatePosted { get; set; } = new DateTime();
        public DateTime? ClosingDate { get; set; }
        public string EmploymentType { get; set; } // Тип занятости (например, полный рабочий день, частичная занятость)
        public string EducationLevel { get; set; }
        public int ExperienceYears { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public int DivisionId { get; set; }
        public virtual Division Division { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsRemote { get; set; } = false;
    }
}
