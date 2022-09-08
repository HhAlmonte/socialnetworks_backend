namespace WebApi.DTOs
{
    public class ResponsePublicationDto
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int Status { get; set; }
    }
}
