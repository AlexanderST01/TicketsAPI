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
public class ProductosController(Contexto _context) : ControllerBase
{

    // GET: api/Productos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductoResponse>>> GetProductoss()
    {
        var Productos = await _context.Productos.ToListAsync();
        var PersonaResponse = Productos.Select(p => new ProductoResponse
        {
            ProductoId = p.ProductoId,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            Precio = p.Precio
        }).ToList();

        return Ok(PersonaResponse);
    }

    // GET: api/Productos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductoResponse>> GetProductos(int id)
    {
        var Producto = await _context.Productos.FindAsync(id);

        if (Producto == null)
        {
            return NotFound();
        }

        var ProductoResponse = new ProductoResponse
        {
            ProductoId = Producto.ProductoId,
            Nombre = Producto.Nombre,
            Descripcion = Producto.Descripcion,
            Precio = Producto.Precio
        };

        return Ok(ProductoResponse);
    }

    // POST: api/Productos
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ProductoResponse>> PostProducto(ProductoRequest Producto)
    {
        var producto = new Productos
        {
            ProductoId = 0,
            Nombre = Producto.Nombre,
            Descripcion = Producto.Descripcion,
            Precio = Producto.Precio
        };
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProductos", new { id = producto.ProductoId }, producto);
    }

    // PUT: api/Productos/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProducto(int id, ProductoRequest productoR)
    {
        var producto = new Productos
        {
            ProductoId = id,
            Nombre = productoR.Nombre,
            Descripcion = productoR.Descripcion,
            Precio = productoR.Precio
        };
        if (id != producto.ProductoId)
        {
            return BadRequest();
        }

        _context.Entry(producto).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Productos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProducto(int id)
    {
        var Producto = await _context.Productos.FindAsync(id);
        if (Producto == null)
        {
            return NotFound();
        }

        _context.Productos.Remove(Producto);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
