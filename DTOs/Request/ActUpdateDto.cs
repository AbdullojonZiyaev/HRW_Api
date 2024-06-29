namespace HRM_Project.DTOs.Request
{
    public class ActUpdateDto
    {
        public int Id { get; set; }
        public string ActNumber { get; set; }
        public int ActTypeId { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public bool ActStatus { get; set; }
        public int? CompanyId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DivisionId { get; set; }
        public int? EmployeeId { get; set; }
    }
}
