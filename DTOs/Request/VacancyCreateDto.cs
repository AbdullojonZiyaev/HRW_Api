﻿namespace HRM_Project.DTOs.Request
{
    public class VacancyCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Responsibilities { get; set; }
        public string Requirements { get; set; }
        public string Qualifications { get; set; }
        public double Salary { get; set; }
        public DateTime DatePosted { get; set; } = DateTime.Now;
        public DateTime? ClosingDate { get; set; }
        public string EmploymentType { get; set; }
        public string EducationLevel { get; set; }
        public int ExperienceYears { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public int DivisionId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsRemote { get; set; } = false;
    }

}
