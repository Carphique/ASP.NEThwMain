using ASP.NEThwMain.Data;
using ASP.NEThwMain.DTO;
using ASP.NEThwMain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class OrderItemController : ControllerBase
{
    private readonly Context _db;
    private readonly IMapper _mapper;

    public OrderItemController(Context db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<PageResult<OrderItemReadDTO>>> Get(int page = 1, int pageSize = 10)
    {
        var query = _db.OrderItems.AsNoTracking();

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new PageResult<OrderItemReadDTO>
        {
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            Items = _mapper.Map<List<OrderItemReadDTO>>(items)
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderItemCreateDTO dto)
    {
        var entity = _mapper.Map<OrderItem>(dto);
        entity.Id = Guid.NewGuid();

        _db.OrderItems.Add(entity);
        await _db.SaveChangesAsync();

        return Ok(_mapper.Map<OrderItemReadDTO>(entity));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var entity = await _db.OrderItems.FindAsync(id);
        if (entity == null) return NotFound();

        _db.OrderItems.Remove(entity);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
