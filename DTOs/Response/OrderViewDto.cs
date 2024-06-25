namespace HRM_Project.DTOs.Response
{
    public class OrderViewDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int OrderTypeId { get; set; }
        public string OrderTypeName { get; set; }
        public string Description { get; set; }
        public DateTime ActivationDate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool Approved { get; set; }
        public string SignedBy { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? DivisionId { get; set; }
        public string DivisionName { get; set; }
    }
}
