using HRM_Project.Models.Abstraction;

namespace HRM_Project.Models
{
    public class News : DbRecord
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; } = true;
        public string Tags { get; set; }
    }
}
