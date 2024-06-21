using HRM_Project.Models.Abstraction;
using HRM_Project.Models.Common;
using HRM_Project.Models.Types;

namespace HRM_Project.Models
{
    public class TimeSheet : DbRecord
    {
        public string NumberTimeSheet { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public int? DivisionId { get; set; }
        public virtual Division Division { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; } 
        public TimeSpan HoursWorked { get; set; } 
        public int TimeSheetTypeId { get; set; } 
        public virtual TimeSheetType TimeSheetType { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool IsPrimary { get; set; } // Первичный или корректирующий табель
        public int? UserId { get; set; }
        public virtual User User { get; set; } 
    }
}
