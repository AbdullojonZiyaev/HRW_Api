namespace HRM_Project.DTOs.Request
{
    public class TimeSheetUpdateDto
    {
        public int Id { get; set; }
        public string NumberTimeSheet { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public int? DivisionId { get; set; }
        public int EmployeeId { get; set; }
        public string HoursWorked { get; set; }
        public int TimeSheetTypeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool IsPrimary { get; set; }
        public int? UserId { get; set; }
    }

}
