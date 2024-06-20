namespace HRM_Project.DTOs.Request
{
    public class DivisionUpdateDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string HeadOfDivision { get; set; }
        public int? DepartmentId { get; set; }
    }
}
