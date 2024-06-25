namespace HRM_Project.DTOs.Request
{
    public class NewsCreateDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; }
        public string Tags { get; set; }
    }
}
