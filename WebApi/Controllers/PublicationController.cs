using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.DTOs;
using WebApi.Errors;

namespace WebApi.Controllers
{
    public class PublicationController : BaseApiController
    {
        private readonly IGenericRepository<PublicationsEntities> _publicationRepository;
        
        public PublicationController(IGenericRepository<PublicationsEntities> publicationRepository)
        {
            _publicationRepository = publicationRepository;
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<ActionResult<ResponsePublicationDto>> CreatePublication([FromForm]PublicationDto publicationDto)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            
            var publication = new PublicationsEntities
            {
                Content = publicationDto.Content,
                ImageUrl = publicationDto.ImageUrl,
                VideoUrl = publicationDto.VideoUrl,
                CreatedBy = email,
            };
            var result = await _publicationRepository.Add(publication);
            
            if(result == 0)
            {
                return BadRequest(new CodeErrorResponse(400, "No se pudo crear la publicación"));
            }

            return new ResponsePublicationDto
            {
                Id = publication.Id,
                Content = publication.Content,
                ImageUrl = publication.ImageUrl,
                VideoUrl = publication.VideoUrl,
                CreatedBy = publication.CreatedBy,
                CreatedDate = publication.CreatedDate,
                ModifiedBy = publication.ModifiedBy ?? "no se han realizado modificaciones",
                ModifiedDate = publication.ModifiedDate,
                Status = publication.Status,
            };
        }

        [Authorize]
        [HttpPost("update/{id}")]
        public async Task<ActionResult<ResponsePublicationDto>> UpdatePublication(string id, [FromForm]PublicationDto publicationDto)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            
            var publication = new PublicationsEntities
            {
                Id = id,
                Content = publicationDto.Content,
                ImageUrl = publicationDto.ImageUrl,
                VideoUrl = publicationDto.VideoUrl,
                CreatedBy = email,
                ModifiedBy = email,
                ModifiedDate = DateTime.Now
            };

            var result = await _publicationRepository.Update(publication);
            
            if(result == 0)
            {
                return BadRequest(new CodeErrorResponse(400, "No se pudo actualizar la publicación"));
            }

            return new ResponsePublicationDto
            {
                Id = publication.Id,
                Content = publication.Content,
                ImageUrl = publication.ImageUrl,
                VideoUrl = publication.VideoUrl,
                CreatedBy = publication.CreatedBy,
                CreatedDate = publication.CreatedDate,
                ModifiedBy = publication.ModifiedBy,
                ModifiedDate = publication.ModifiedDate,
                Status = publication.Status,
            };
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<string>> DeletePublication(string id)
        {
            var publication = await _publicationRepository.GetById(id);
            
            if (publication == null)
            {
                return BadRequest(new CodeErrorResponse(404));
            }

            var result = await _publicationRepository.Delete(publication);

            if (result == 0)
            {
                return BadRequest(new CodeErrorResponse(400, "No se pudo eliminar la publicación"));
            }

            return "La publicación se eliminó correctamente";
        }

        [Authorize]
        [HttpGet("get/{id}")]
        public async Task<ActionResult<ResponsePublicationDto>> GetPublicationById(string id)
        {
            var publication = await _publicationRepository.GetById(id);
            
            if (publication == null)
            {
                return BadRequest(new CodeErrorResponse(404));
            }
            
            return new ResponsePublicationDto
            {
                Id = publication.Id,
                Content = publication.Content,
                ImageUrl = publication.ImageUrl,
                VideoUrl = publication.VideoUrl,
                CreatedBy = publication.CreatedBy,
                CreatedDate = publication.CreatedDate,
                ModifiedBy = publication.ModifiedBy ?? "no se han realizado modificaciones",
                ModifiedDate = publication.ModifiedDate,
                Status = publication.Status,
            };
        }
    }
}
