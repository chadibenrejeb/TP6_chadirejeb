using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models;
using SchoolAPI.DTOs;
using AutoMapper;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        private readonly IMapper _mapper;

        public SchoolsController(SchoolDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("get-all-schools")]
        public async Task<ActionResult<IEnumerable<SchoolDto>>> GetSchools()
        {
            var schools = await _context.Schools.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<SchoolDto>>(schools));
        }

        [HttpGet("get-school-by-id/{id}")]
        public async Task<ActionResult<SchoolDto>> GetSchool(int id)
        {
            var school = await _context.Schools.FindAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SchoolDto>(school));
        }

        [HttpPut("edit-school/{id}")]
        public async Task<IActionResult> PutSchool(int id, SchoolDto schoolDto)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }

            _mapper.Map(schoolDto, school);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost("create-school")]
        public async Task<ActionResult<SchoolDto>> PostSchool(SchoolDto schoolDto)
        {
            var school = _mapper.Map<School>(schoolDto);
            _context.Schools.Add(school);
            await _context.SaveChangesAsync();

            var createdDto = _mapper.Map<SchoolDto>(school);
            return CreatedAtAction(nameof(GetSchool), new { id = school.Id }, createdDto);
        }


        [HttpDelete("delete-school/{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }

            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("search-by-name")]
        public async Task<ActionResult<IEnumerable<SchoolDto>>> SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Search term cannot be empty");
            }

            var schools = await _context.Schools
                .Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<SchoolDto>>(schools));
        }

        private bool SchoolExists(int id)
        {
            return _context.Schools.Any(e => e.Id == id);
        }


        [HttpGet("list-schools")]
        public ActionResult<IEnumerable<SchoolDto>> ListSchools()
        {
            return Ok(_context.Schools.Select(school => _mapper.Map<SchoolDto>(school)));
        }



        [HttpPost("add-new-school")]
        public async Task<ActionResult> AddSchool(SchoolDto newschool)
        {
            var school = _mapper.Map<School>(newschool);
            _context.Schools.Add(school);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}