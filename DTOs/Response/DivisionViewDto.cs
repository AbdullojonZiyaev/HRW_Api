namespace HRM_Project.DTOs.Response
{
    public class DivisionViewDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string HeadOfDivision { get; set; }
        public string DepartmentName { get; set; }
        public ICollection<EmployeeViewDto> Employees { get; set; }
    }
}
