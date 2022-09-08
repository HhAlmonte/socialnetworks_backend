using Core.Entities;
using Core.Interface;

namespace WebApi.Controllers
{
    public class AnswerController : BaseApiController
    {
        private readonly IGenericRepository<AnswersEntities> _answerRepository;
    }
}
