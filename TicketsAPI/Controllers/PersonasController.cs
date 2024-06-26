using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketsAPI.DAL;
using TicketsAPI.Models;
using TicketsAPI.Models.Ticket;

namespace TicketsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonasController(Contexto _context) : ControllerBase
{

    // GET: api/Personas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonaResponse>>> GetPersonas()
    {
        var Personas = await _context.Personas.ToListAsync();
        var PersonaResponse = Personas.Select(t => new PersonaResponse
        {
            PersonaId = t.PersonaId,
            Direccion = t.Direccion,
            Nombre = t.Nombre,
            Apellido = t.Apellido
        }).ToList();

        return Ok(PersonaResponse);
    }

    // GET: api/Personas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonaResponse>> GetPersonas(int id)
    {
        var Persona = await _context.Personas.FindAsync(id);

        if (Persona == null)
        {
            return NotFound();
        }

        var PersonaResponse = new PersonaResponse
        {
            PersonaId = Persona.PersonaId,
            Direccion = Persona.Direccion,
            Nombre = Persona.Nombre,
            Apellido = Persona.Apellido
        };

        return Ok(PersonaResponse);
    }

    // POST: api/Personas
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<PersonaResponse>> PostPersona(PersonaRequest Persona)
    {
        var persona = new Personas
        {
            PersonaId = 0,
            Direccion = Persona.Direccion,
            Nombre = Persona.Nombre,
            Apellido = Persona.Apellido
        };
        _context.Personas.Add(persona);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPersonas", new { id = persona.PersonaId }, persona);
    }

    // PUT: api/Personas/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPersona(int id,  PersonaRequest personaR)
    {
        var Personas = new Personas
        {
            PersonaId = id,
            Direccion = personaR.Direccion,
            Nombre = personaR.Nombre,
            Apellido = personaR.Apellido
        };
        if (id != Personas.PersonaId)
        {
            return BadRequest();
        }

        _context.Entry(Personas).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Personas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePersona(int id)
    {
        var Personas = await _context.Personas.FindAsync(id);
        if (Personas == null)
        {
            return NotFound();
        }

        _context.Personas.Remove(Personas);
        await _context.SaveChangesAsync();

        return NoContent();
    }
 
}