using HRM_Project.Models.Abstraction;
using HRM_Project.Models.Common;
using HRM_Project.Models.Types;

namespace HRM_Project.Models
{
    public class Order : DbRecord
    {
        public string OrderNumber { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public int OrderTypeId { get; set; }
        public virtual OrderType OrderType { get; set; }
        public string Description { get; set; }
        public DateTime ActivationDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public bool Approved { get; set; }
        public string SignedBy { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public int? DivisionId { get; set; }
        public virtual Division Division { get; set; }
    }
}
