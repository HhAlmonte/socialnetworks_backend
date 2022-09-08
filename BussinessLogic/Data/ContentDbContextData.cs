using Core.Entities;
using Microsoft.Extensions.Logging;

namespace BussinessLogic.Data
{
    public class ContentDbContextData
    {
        public static async Task SeedContentAsync(ContentDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                // Seed, if necessary
                if (!context.PublicationsEntities.Any())
                {
                    // Publications
                    var publication = new PublicationsEntities
                    {
                        Content = "Hola Mundo, soy una publicación",
                        ImageUrl = "imagendeprueba.png",
                        CreatedBy = "Hbalmontess373@gmail.com"
                    };

                    context.PublicationsEntities.Add(publication);
                    await context.SaveChangesAsync();
                }
                
                if (!context.CommentsEntities.Any())
                {
                    // Comments
                    var comments = new CommentsEntities
                    {
                        PublicationId = "489c0585-2208-4bf6-aad1-f6f6855a47a2",
                        Content = "Hola Mundo, soy un comentario",
                        ImageUrl = "imagendeprueba.png",
                        CreatedBy = "Hbalmontess272@gmail.com"
                    };

                    context.CommentsEntities.Add(comments);
                    await context.SaveChangesAsync();
                }

                if (!context.AnswersEntities.Any())
                {
                    // Answers
                    var answers = new AnswersEntities
                    {
                        CommentId = "3f70f2e2-7fbc-4db4-8ad5-ad4c5a508d82",
                        Content = "Hola Mundo, soy una respuesta",
                        ImageUrl = "imagendeprueba.png",
                        CreatedBy = "Hbalmontess373@gmail.com"
                    };

                    context.AnswersEntities.Add(answers);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ContentDbContextData>();
                logger.LogError(ex, "Error en el proceso de Seed");
            }
            
        }
    }
}
