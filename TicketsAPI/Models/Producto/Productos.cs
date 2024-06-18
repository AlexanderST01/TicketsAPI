using System.ComponentModel.DataAnnotations;

namespace TicketsAPI.Models;

public class Productos
{
    [Key]
    public int ProductoId { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public double Precio { get; set; }

}
