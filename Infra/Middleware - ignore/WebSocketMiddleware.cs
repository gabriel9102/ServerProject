using Infra.WebSocket;  // Certifique-se de que o namespace está correto
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets; // Importando o namespace correto de WebSockets do .NET
using System.Threading.Tasks;

namespace Infra.Middleware
{
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly WebSocketHandler _webSocketHandler;

        public WebSocketMiddleware(RequestDelegate next, WebSocketHandler webSocketHandler)
        {
            _next = next;
            _webSocketHandler = webSocketHandler;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                // Usando o nome completo do tipo WebSocket para evitar colisão de nomes
                System.Net.WebSockets.WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await _webSocketHandler.HandleAsync(context, webSocket);
            }
            else
            {
                await _next(context); // Continua para o próximo middleware se não for WebSocket
            }
        }
    }
}
