namespace HRM_Project.DTOs.Response
{
    public class NewsViewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; }
        public string Tags { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
