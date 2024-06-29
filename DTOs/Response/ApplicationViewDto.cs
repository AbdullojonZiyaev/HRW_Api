namespace HRM_Project.DTOs.Response
{
    public class ApplicationViewDto
    {
        public int Id { get; set; }
        public string ActNumber { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int PositionId { get; set; }
        public string PositionTitle { get; set; }
        public int ApplicationTypeId { get; set; }
        public string ApplicationTypeName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public bool Approved { get; set; }
        public string SignedBy { get; set; }
    }

}