namespace HRM_Project.DTOs.Request
{
    public class DepartmentCreateDto
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string HeadOfDepartment { get; set; }
        public int? CompanyId { get; set; }
    }
}
