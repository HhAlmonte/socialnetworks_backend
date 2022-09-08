namespace Core.Entities
{
    public class CommentsEntities : CommonProperties
    {
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        
        public string PublicationId { get; set; }
        public PublicationsEntities Publication { get; set; }

        public List<AnswersEntities> Answers { get; set; }
    }
}
