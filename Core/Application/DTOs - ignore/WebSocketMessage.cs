using Core.Domain.Entities;

namespace Core.Application.DTOs
{
    public class WebSocketMessage
    {
        public string Action { get; set; }
        public AppTask Task { get; set; } // Usando a entidade AppTask
    }
}
