namespace HRM_Project.DTOs.Response
{
    public class DepartmentViewDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string HeadOfDepartment { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }  // Example of an additional property
    }
}
