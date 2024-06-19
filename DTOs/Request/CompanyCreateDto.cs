using HRM_Project.Models.Common;

namespace HRM_Project.DTOs.Request
{
    public class CompanyCreateDto
    {
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Inn { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Director { get; set; }
        public int? CityId { get; set; }
    }
}
