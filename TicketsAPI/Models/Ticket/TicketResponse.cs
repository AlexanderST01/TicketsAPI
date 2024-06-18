namespace TicketsAPI.Models.Ticket;

public class TicketResponse
{
    public int TicketId { get; set; }
    public string? Descripcion { get; set; }
    public string? Fecha { get; set; }
    public double Precio { get; set; }

}
