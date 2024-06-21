using HRM_Project.Models.Abstraction;
using HRM_Project.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRM_Project.Models
{
    public class Company : DbRecord
    {
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Inn { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Director { get; set; }
        public int? CityId { get; set; }
        public virtual City City { get; set; }
        [NotMapped]
        public virtual User User { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Vacancy> Vacancies { get; set; } 
    }
}
