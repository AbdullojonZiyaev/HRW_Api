using HRM_Project.Models;

namespace HRM_Project.DTOs.Response
{
    public class MinimalEmployeeViewDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string PositionTitle { get; set; }
        public int divisionId { get; set; }
        public string DivisionName { get; set; }
        public int PositionId { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public string CompanyName { get; set; }
        public string DepartmentName { get; set; }

        public bool isInReserve { get; set; }
    }
}
