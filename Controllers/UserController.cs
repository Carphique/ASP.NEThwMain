using ASP.NEThwMain.Data;
using ASP.NEThwMain.DTO;
using ASP.NEThwMain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ASP.NEThwMain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DBContext _db;
        private readonly IMapper _mapper;

        public UserController(DBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserReadDTO>>> GetAll()
        {
            var users = await _db.Users.ToListAsync();
            return Ok(_mapper.Map<List<UserReadDTO>>(users));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserReadDTO>> GetById(Guid id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();

            return Ok(_mapper.Map<UserReadDTO>(user));
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserCreateDTO dto)
        {
            var entity = _mapper.Map<User>(dto);
            entity.Id = Guid.NewGuid();

            _db.Users.Add(entity);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.Map<UserReadDTO>(entity));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UserUpdateDTO dto)
        {
            var entity = await _db.Users.FindAsync(id);
            if (entity == null) return NotFound();

            _mapper.Map(dto, entity);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _db.Users.FindAsync(id);
            if (entity == null) return NotFound();

            _db.Users.Remove(entity);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
