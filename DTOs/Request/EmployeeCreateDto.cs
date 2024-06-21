using HRM_Project.Models.Enums;

namespace HRM_Project.DTOs.Request
{
    public class EmployeeCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfHire { get; set; }
        public decimal Salary { get; set; }
        public Gender Gender { get; set; }
        public string Nationality { get; set; }
        public string Education { get; set; }
        public string DegreeName { get; set; }
        public string PersonalSkills { get; set; }
        public string ComputerSkills { get; set; }
        public string Qualifications { get; set; }
        public string LanguageKnowledge { get; set; }
        public string Party { get; set; } // Партийность
        public string PreviousJob { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public MilitaryStatus MilitaryStatus { get; set; }
        public DriverLicense DriverLicense { get; set; }
        public int NumberOfChildren { get; set; }
        public string Specialization { get; set; }
        public string InsuranceStatus { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public int? DivisionId { get; set; }
        public int? PositionId { get; set; }
        public bool IsInReserve { get; set; } = true;
    }

}
