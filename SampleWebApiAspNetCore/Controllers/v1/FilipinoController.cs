using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SampleWebApiAspNetCore.Dtos;
using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Services;
using SampleWebApiAspNetCore.Repositories;
using SampleWebApiAspNetCore.Models;

namespace SampleWebApiAspNetCore.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FilipinoController : ControllerBase
    {
        private readonly IFilipinoRepository _filipinoRepository;
        private readonly IMapper _mapper;
        private readonly ILinkService<FilipinoController> _linkService;

        public FilipinoController(
            IFilipinoRepository filipinoRepository,
            IMapper mapper,
            ILinkService<FilipinoController> linkService)
        {
            _filipinoRepository = filipinoRepository;
            _mapper = mapper;
            _linkService = linkService;
        }

        [HttpGet]
        [Route("{id:int}", Name = nameof(GetSingleFilam))]
        public ActionResult GetSingleFilam(ApiVersion version, int id)
        {
            FilipinoEntity filipinoItem = _filipinoRepository.GetSingle(id);

            if (filipinoItem == null)
            {
                return NotFound();
            }
            FilipinoDto item = _mapper.Map<FilipinoDto>(filipinoItem);

            return Ok(_linkService.ExpandSingleItem(item, item.Id, version));
        }

        [HttpPost(Name = nameof(AddFilam))]
        public ActionResult<FilipinoDto> AddFilam(ApiVersion version, [FromBody] FilipinoCreateDto filipinoCreateDto)
        {
            if (filipinoCreateDto == null)
            {
                return BadRequest();
            }

            FilipinoEntity toAdd = _mapper.Map<FilipinoEntity>(filipinoCreateDto);

            _filipinoRepository.Add(toAdd);

            if (!_filipinoRepository.Save())
            {
                throw new Exception("Creating a filipinoItem failed on save.");
            }

            FilipinoEntity newFilipinoItem = _filipinoRepository.GetSingle(toAdd.Id);
            FilipinoDto filipinoDto = _mapper.Map<FilipinoDto>(newFilipinoItem);

            return CreatedAtRoute(nameof(GetSingleFilam),
                new { version = version.ToString(), id = newFilipinoItem.Id },
                _linkService.ExpandSingleItem(filipinoDto, filipinoDto.Id, version));
        }

        [HttpDelete]
        [Route("{id:int}", Name = nameof(RemoveFilam))]
        public ActionResult RemoveFilam(int id)
        {
            FilipinoEntity filipinoItem = _filipinoRepository.GetSingle(id);

            if (filipinoItem == null)
            {
                return NotFound();
            }

            _filipinoRepository.Delete(id);

            if (!_filipinoRepository.Save())
            {
                throw new Exception("Deleting a filipinoItem failed on save.");
            }

            return NoContent();
        }

        [HttpPut]
        [Route("{id:int}", Name = nameof(UpdateFilam))]
        public ActionResult<FilipinoDto> UpdateFilam(ApiVersion version, int id, [FromBody] FilipinoUpdateDto filipinoUpdateDto)
        {
            if (filipinoUpdateDto == null)
            {
                return BadRequest();
            }

            var existingFilipinoItem = _filipinoRepository.GetSingle(id);

            if (existingFilipinoItem == null)
            {
                return NotFound();
            }

            _mapper.Map(filipinoUpdateDto, existingFilipinoItem);

            _filipinoRepository.Update(id, existingFilipinoItem);

            if (!_filipinoRepository.Save())
            {
                throw new Exception("Updating a filipinoItem failed on save.");
            }
            FilipinoDto filipinoDto = _mapper.Map<FilipinoDto>(existingFilipinoItem);

            return Ok(_linkService.ExpandSingleItem(filipinoDto, filipinoDto.Id, version));
        }
    }
}