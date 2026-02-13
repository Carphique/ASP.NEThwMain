using ASP.NEThwMain.Data;
using ASP.NEThwMain.DTO;
using ASP.NEThwMain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly Context _db;
    private readonly IMapper _mapper;

    public OrderController(Context db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderReadDTO>>> GetAll()
    {
        var orders = await _db.Orders.ToListAsync();
        return Ok(_mapper.Map<List<OrderReadDTO>>(orders));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderReadDTO>> GetById(Guid id)
    {
        var order = await _db.Orders.FindAsync(id);
        if (order == null) return NotFound();

        return Ok(_mapper.Map<OrderReadDTO>(order));
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderCreateDTO dto)
    {
        var entity = _mapper.Map<Order>(dto);
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;

        _db.Orders.Add(entity);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = entity.Id },
            _mapper.Map<OrderReadDTO>(entity));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, OrderUpdateDTO dto)
    {
        var entity = await _db.Orders.FindAsync(id);
        if (entity == null) return NotFound();

        _mapper.Map(dto, entity);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var entity = await _db.Orders.FindAsync(id);
        if (entity == null) return NotFound();

        _db.Orders.Remove(entity);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
