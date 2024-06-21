using HRM_Project.Models.Abstraction;
using HRM_Project.Models.Common;
using HRM_Project.Models.Types;

namespace HRM_Project.Models
{
    public class Reference : DbRecord
    {
        public string ReferenceNumber { get; set; }
        public int ReferenceTypeId { get; set; }
        public ReferenceType ReferenceType { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public bool ReferenceStatus { get; set; }
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public int? DivisionId { get; set; }
        public virtual Division Division { get; set; }
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
