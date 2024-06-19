using HRM_Project.DTOs.Request;

namespace HRM_Project.DTOs.Response
{
    public class CompanyViewDto:CompanyUpdateDto
    {
        public string CityName { get; set; }
    }
}
