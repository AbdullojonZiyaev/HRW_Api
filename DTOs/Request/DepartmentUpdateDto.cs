namespace HRM_Project.DTOs.Request
{
    public class DepartmentUpdateDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string HeadOfDepartment { get; set; }
        public int? CompanyId { get; set; }
    }
}
