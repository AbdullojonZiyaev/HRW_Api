namespace HRM_Project.DTOs.Request
{
    public class ApplicationUpdateDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public int? DivisionId { get; set; }
        public int PositionId { get; set; }
        public int ApplicationTypeId { get; set; }
        public int EmployeeId { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public bool Approved { get; set; }
        public string SignedBy { get; set; }
    }

}