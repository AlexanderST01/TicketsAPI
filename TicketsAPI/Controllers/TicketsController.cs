using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketsAPI.DAL;
using TicketsAPI.Models.Ticket;

namespace TicketsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController(Contexto _context) : ControllerBase
{

    // GET: api/Tickets
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketResponse>>> GetTickets()
    {
        var Tickets = await _context.Tickets.ToListAsync();
        var TicketResponse = Tickets.Select(t => new TicketResponse
        {
            TicketId = t.TicketId,
            Descripcion = t.Descripcion,
            Fecha = t.Fecha,
            Precio = t.Precio
        }).ToList();

        return Ok(TicketResponse);
    }

    // GET: api/Tickets/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TicketResponse>> GetTickets(int id)
    {
        var Tickets = await _context.Tickets.FindAsync(id);

        if (Tickets == null)
        {
            return NotFound();
        }

        var TicketResponse = new TicketResponse
        {
            TicketId = Tickets.TicketId,
            Descripcion = Tickets.Descripcion,
            Fecha = Tickets.Fecha,
            Precio = Tickets.Precio
        };

        return Ok(TicketResponse);
    }

    // POST: api/Tickets
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TicketResponse>> PostTicket(TicketRequest Tickets)
    {
        var articulo = new Tickets
        {
            TicketId = 0,
            Descripcion = Tickets.Descripcion,
            Fecha = Tickets.Fecha,
            Precio = Tickets.Precio
        };
        _context.Tickets.Add(articulo);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTickets", new { id = articulo.TicketId }, articulo);

        //if (TicketDescripcionExiste(Tickets.Descripcion!))
        //{
        //    var ticketAux = await _context.Tickets
        //    .FirstOrDefaultAsync(a => a.Descripcion == Tickets.Descripcion);
        //    if (ticketAux == null)
        //    {
        //        return NotFound();
        //    }

        //    ticketAux.Descripcion = Tickets.Descripcion;
        //    ticketAux.Precio = Tickets.Precio;
        //    ticketAux.Fecha = Tickets.Fecha;

        //    _context.Entry(ticketAux).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return Ok(ticketAux);
        //}
        //else
        //{
        //    var ticket = new Tickets
        //    {
        //        TicketId = 0,
        //        Descripcion = Tickets.Descripcion,
        //        Precio = Tickets.Precio,
        //        Fecha = Tickets.Fecha
        //    };

        //    _context.Tickets.Add(ticket);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTikets", new { id = ticket.TicketId }, ticket);
        //}
    }

    // PUT: api/Tickets/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTicket(int id, TicketRequest ticketR)
    {
        var Tickets = new Tickets
        {
            TicketId = id,
            Descripcion = ticketR.Descripcion,
            Fecha = ticketR.Fecha,
            Precio = ticketR.Precio
        };

        if (id != Tickets.TicketId)
        {
            return BadRequest();
        }

        _context.Entry(Tickets).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Tickets/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTIckets(int id)
    {
        var Tickets = await _context.Tickets.FindAsync(id);
        if (Tickets == null)
        {
            return NotFound();
        }

        _context.Tickets.Remove(Tickets);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TicketExiste(int id)
    {
        return _context.Tickets.Any(e => e.TicketId == id);
    }
    private bool TicketDescripcionExiste(string descripcion)
    {
        return _context.Tickets.Any(e => e.Descripcion.ToLower() == descripcion.ToLower());
    }
   
}
