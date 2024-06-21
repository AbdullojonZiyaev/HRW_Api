using HRM_Project.Models.Abstraction;

namespace HRM_Project.Models.Types
{
    public class ActType : DbRecord
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Act> Act { get; set; } 
    }
}
