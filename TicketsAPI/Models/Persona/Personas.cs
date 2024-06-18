using System.ComponentModel.DataAnnotations;

namespace TicketsAPI.Models;

public class Personas
{
    [Key]
    public int PersonaId { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? Direccion { get; set; }

}
