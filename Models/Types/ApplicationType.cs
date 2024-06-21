using HRM_Project.Models.Abstraction;
using static System.Net.Mime.MediaTypeNames;

namespace HRM_Project.Models.Types
{
    public class ApplicationType : DbRecord
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Application> Application { get; set; } 

    }
}
