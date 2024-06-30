namespace HRM_Project.DTOs.Response
{
    public class ReferenceViewDto
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

        // Including related entities' names
        public string ReferenceTypeName { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public string DepartmentName { get; set; }
        public string DivisionName { get; set; }
        public string EmployeeName { get; set; }
    }

}
