using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models;
using SchoolAPI.DTOs;
using AutoMapper;
using SchoolAPI.Repositories;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsRepoController : ControllerBase
    {
        private readonly IUniversityRepository _repository;
        private readonly IMapper _mapper;

        public SchoolsRepoController(IUniversityRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("get-all-schools")]
        public ActionResult<IEnumerable<SchoolDto>> GetSchools()
        {
            var schools = _repository.GetSchools();
            return Ok(_mapper.Map<IEnumerable<SchoolDto>>(schools));
        }

        [HttpGet("get-school-by-id/{id}")]
        public ActionResult<SchoolDto> GetSchool(int id)
        {
            var school = _repository.GetSchoolById(id);
            if (school == null) return NotFound();

            return Ok(_mapper.Map<SchoolDto>(school));
        }

        [HttpGet("search-by-name")]
        public ActionResult<IEnumerable<SchoolDto>> SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Search term cannot be empty");

            var schools = _repository.GetSchoolsByName(name);
            return Ok(_mapper.Map<IEnumerable<SchoolDto>>(schools));
        }

        [HttpPost("create-school")]
        public ActionResult<SchoolDto> PostSchool(SchoolDto schoolDto)
        {
            var school = _mapper.Map<School>(schoolDto);
            _repository.AddSchool(school);

            var createdDto = _mapper.Map<SchoolDto>(school);
            return CreatedAtAction(nameof(GetSchool), new { id = school.Id }, createdDto);
        }

        [HttpPut("edit-school/{id}")]
        public IActionResult PutSchool(int id, SchoolDto schoolDto)
        {
            var school = _repository.GetSchoolById(id);
            if (school == null) return NotFound();

            _mapper.Map(schoolDto, school);
            _repository.UpdateSchool(school);
            return NoContent();
        }

        [HttpDelete("delete-school/{id}")]
        public IActionResult DeleteSchool(int id)
        {
            var school = _repository.GetSchoolById(id);
            if (school == null) return NotFound();

            _repository.DeleteSchool(id);
            return NoContent();
        }

        [HttpPost("add-new-school")]
        public IActionResult AddSchool(SchoolDto newSchool)
        {
            var school = _mapper.Map<School>(newSchool);
            _repository.AddSchool(school);
            return Ok();
        }

        [HttpGet("list-schools")]
        public ActionResult<IEnumerable<SchoolDto>> ListSchools()
        {
            var schools = _repository.GetSchools();
            return Ok(_mapper.Map<IEnumerable<SchoolDto>>(schools));
        }
    }
}
