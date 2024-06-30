namespace HRM_Project.DTOs.Request
{
    public class ReferenceUpdateDto
    {
        public int Id { get; set; }
        public string ReferenceNumber { get; set; }
        public int ReferenceTypeId { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public bool ReferenceStatus { get; set; }
        public int? CompanyId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DivisionId { get; set; }
        public int? EmployeeId { get; set; }
    }
    
}
