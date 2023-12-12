using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketsAPI.Core.Context;
using TicketsAPI.Core.DTO;
using TicketsAPI.Core.Entities;

namespace TicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TicketsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //CRUD
        //Create
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateTicket([FromBody]CreateTicketDTO createTicketDTO)
        {
            var newTicket = new Ticket();
            _mapper.Map(createTicketDTO, newTicket);
            await _context.Tickets.AddAsync(newTicket);
            await _context.SaveChangesAsync();
            return Ok("Ticket saved successfully");
        }
        //Read all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTicketDTO>>> GetTickets(string? q)
        {
            IQueryable<Ticket> query = _context.Tickets;
            if(q is not null)
            {
                query = query.Where(t => t.PassengerName.Contains(q));
            }
            //var tickets = await _context.Tickets.ToListAsync();
            var tickets = await query.ToListAsync();
            var convertedTickets = _mapper.Map<IEnumerable<GetTicketDTO>>(tickets);
            return Ok(convertedTickets);
        }
        //Read by id
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GetTicketDTO>> GetTicketById([FromRoute] long id)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
            if (ticket == null)
            {
                return NotFound("Ticket Not Found");
            }
            var convertedTickets = _mapper.Map<GetTicketDTO>(ticket);
            return Ok(convertedTickets);
        }
        //Update
        [HttpPut]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditTicket([FromRoute] long id, [FromBody]UpdateTicketDTO updateTicketDTO)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
            if (ticket == null)
            {
                return NotFound("Ticket Not Found");
            }
            _mapper.Map(updateTicketDTO, ticket);
            ticket.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();  
            return Ok("Ticket Updated Successfully");
        }
        //Delete
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> DeleteTask([FromRoute] long id)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
            if (ticket == null)
            {
                return NotFound("Ticket Not Found");
            }
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return Ok("Ticket Deleted Successfully");
        }

    }
}
