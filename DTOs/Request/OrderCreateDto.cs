namespace HRM_Project.DTOs.Request
{
    public class OrderCreateDto
    {
        public string OrderNumber { get; set; }
        public int EmployeeId { get; set; }
        public int OrderTypeId { get; set; }
        public string Description { get; set; }
        public DateTime ActivationDate { get; set; }
        public int UserId { get; set; }
        public bool Approved { get; set; }
        public string SignedBy { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public int? DivisionId { get; set; }
    }

}
