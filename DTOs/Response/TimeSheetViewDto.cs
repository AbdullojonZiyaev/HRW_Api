namespace HRM_Project.DTOs.Response
{
    public class TimeSheetViewDto
    {
        public int Id { get; set; }
        public string NumberTimeSheet { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string HoursWorked { get; set; }
        public int TimeSheetTypeId { get; set; }
        public string TimeSheetTypeName { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool IsPrimary { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
    }

}
