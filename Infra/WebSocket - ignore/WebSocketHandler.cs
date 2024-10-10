using Core.Application.UseCases;
using Core.Application.DTOs;
using WebSocketNet = System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Infra.WebSocket
{
    public class WebSocketHandler
    {
        private readonly AddTask _addTask;
        private readonly DeleteTask _deleteTask;
        private readonly GetTask _getTask;

        // Dicionário para mapear ações para funções
        private readonly Dictionary<string, Func<WebSocketMessage, Task>> _actions;

        public WebSocketHandler(AddTask addTask, DeleteTask deleteTask, GetTask getTask)
        {
            _addTask = addTask;
            _deleteTask = deleteTask;
            _getTask = getTask;

            // Inicializando o dicionário com as ações
            _actions = new Dictionary<string, Func<WebSocketMessage, Task>>
            {
                { "add-task", async (message) => await _addTask.Execute(message.Task) },
                { "delete-task", async (message) => await _deleteTask.Execute(message.Task.Id) },
                { "get-task", async (message) => await _getTask.Execute() }
            };
        }

        // Método principal para lidar com a conexão WebSocket
        public async Task HandleAsync(HttpContext context, WebSocketNet.WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketNet.WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                var receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"Mensagem recebida: {receivedMessage}");

                // Parse a mensagem recebida
                var parsedMessage = JsonSerializer.Deserialize<WebSocketMessage>(receivedMessage);

                if (parsedMessage != null && _actions.ContainsKey(parsedMessage.Action))
                {
                    // Executa a ação correspondente no dicionário
                    await _actions[parsedMessage.Action](parsedMessage);
                }
                else
                {
                    // Ação não reconhecida
                    var unknownMessage = Encoding.UTF8.GetBytes("Ação não reconhecida");
                    await webSocket.SendAsync(new ArraySegment<byte>(unknownMessage), WebSocketNet.WebSocketMessageType.Text, true, CancellationToken.None);
                }

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}
