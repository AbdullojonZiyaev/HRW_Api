using HRM_Project.Models.Abstraction;

namespace HRM_Project.Models.Types
{
    public class OrderType : DbRecord
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string OrderCategory { get; set; }
        public virtual ICollection<Order> Order { get; set; } 
    }
}
