namespace Core.Entities
{
    public class AnswersEntities : CommonProperties
    {
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }

        public string CommentId { get; set; }
        public CommentsEntities Comment { get; set; }
    }
}
