using AutoMapper;
using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.DTOs.PublicationDtos;
using WebApi.Errors;

namespace WebApi.Controllers
{
    public class PublicationController : BaseApiController
    {
        private readonly string? _email;
        private readonly IGenericRepository<PublicationsEntities> _publicationRepository;
        private readonly IMapper _mapper;

        public PublicationController(IGenericRepository<PublicationsEntities> publicationRepository,
                                     IMapper mapper)
        {
            _email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            _mapper = mapper;
            _publicationRepository = publicationRepository;
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<ActionResult<ResponsePublicationDto>> CreatePublication([FromForm]PublicationDto publicationDto)
        {
            var publication = _mapper.Map<PublicationsEntities>(publicationDto); publication.CreatedBy = _email;
            var result = await _publicationRepository.Add(publication);
            
            if(result == 0) return BadRequest(new CodeErrorResponse(400, "No se pudo crear la publicación"));
            
            var responsePublicationDto = _mapper.Map<ResponsePublicationDto>(publication);
            return Ok(responsePublicationDto);
        }

        [Authorize]
        [HttpPost("update/{id}")]
        public async Task<ActionResult<ResponsePublicationDto>> UpdatePublication(string id, [FromForm]PublicationDto publicationDto)
        {
            var publication = _mapper.Map<PublicationsEntities>(publicationDto);
            publication.Id = id; publication.ModifiedBy = _email;
            var result = await _publicationRepository.Update(publication);
            
            if(result == 0) return BadRequest(new CodeErrorResponse(400, "Error al actualizar la publicación"));

            var responsePublicationDto = _mapper.Map<ResponsePublicationDto>(publication);
            return responsePublicationDto;
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<string>> DeletePublication(string id)
        {
            var publication = await _publicationRepository.GetById(id);
            if (publication == null) return NotFound(new CodeErrorResponse(404, "No se encontró la publicación"));
            var result = await _publicationRepository.Delete(publication);
            if (result == 0) return BadRequest(new CodeErrorResponse(400, "No se pudo eliminar la publicación"));
            
            return "La publicación se eliminó correctamente";
        }

        [Authorize]
        [HttpGet("get/{id}")]
        public async Task<ActionResult<ResponsePublicationDto>> GetPublicationById(string id)
        {
            var publication = await _publicationRepository.GetById(id);
            if (publication == null) return NotFound(new CodeErrorResponse(404, "No se encontró la publicación"));
            var responsePublicationDto = _mapper.Map<ResponsePublicationDto>(publication);

            return responsePublicationDto;
        }
    }
}
