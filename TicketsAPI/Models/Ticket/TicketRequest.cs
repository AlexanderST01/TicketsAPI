namespace TicketsAPI.Models.Ticket;

public class TicketRequest
{
    public string? Descripcion { get; set; }
    public string? Fecha { get; set; }
    public double Precio { get; set; }
}
