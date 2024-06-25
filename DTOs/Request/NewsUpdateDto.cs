namespace HRM_Project.DTOs.Request
{
    public class NewsUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; }
        public string Tags { get; set; }
    }
}
