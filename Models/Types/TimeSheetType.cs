using HRM_Project.Models.Abstraction;

namespace HRM_Project.Models.Types
{
    public class TimeSheetType : DbRecord
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<TimeSheet> TimeSheets { get; set; }

    }
}
