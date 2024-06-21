using HRM_Project.Models.Abstraction;

namespace HRM_Project.Models.Types
{
    public class ReferenceType : DbRecord
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Reference> Reference { get; set; } 
    }
}
