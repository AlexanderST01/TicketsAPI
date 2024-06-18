using System.ComponentModel.DataAnnotations;

namespace TicketsAPI.Models.Ticket;

public class Tickets
{
    [Key]
    public int TicketId { get; set; }
    public string? Descripcion { get; set; }
    public string? Fecha { get; set; }
    public double Precio { get; set; }

}
