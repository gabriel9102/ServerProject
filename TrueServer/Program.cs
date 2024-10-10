using Core.Application.UseCases;
using Core.Domain.Repository;
using Infra.Repository;
using Infra.WebSocket;
using Microsoft.AspNetCore.WebSockets;

namespace TrueServer
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        // Adicionar os serviços ao contêiner de injeção de dependências
                        services.AddRouting(); // Necessário para roteamento de API
                        services.AddControllers(); // Necessário para habilitar os controladores de API

                        // Repositório e Casos de Uso
                        services.AddSingleton<ITaskRepository, TaskRepository>();
                        services.AddSingleton<AddTask>();
                        services.AddSingleton<DeleteTask>();
                        services.AddSingleton<GetTask>();
                        //services.AddSingleton<WebSocketHandler>();
                    });

                    webBuilder.ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole(); // Adiciona logging no console
                        logging.SetMinimumLevel(LogLevel.Debug); // Define o nível de logging
                    });

                    webBuilder.Configure(app =>
                    {
                        app.UseDeveloperExceptionPage();
                        app.UseRouting();

                        // Habilitar WebSocketManagement
                        app.UseWebSockets();

                        // Usar o WebSocketMiddleware para lidar com as requisições WebSocket
                        app.UseMiddleware<WebSocketMiddleware>();

                        // Configurar os endpoints para mapear as rotas dos controladores.
                        app.UseEndpoints(endpoints =>
                        {
                            var logger = app.ApplicationServices.GetRequiredService<ILogger<Program>>();
                            logger.LogInformation("Mapeando as rotas...");
                            endpoints.MapControllers(); // Isso mapeia todos os controladores automaticamente.

                            foreach (var dataSource in endpoints.DataSources)
                            {
                                foreach (var endpoint in dataSource.Endpoints)
                                {
                                    logger.LogInformation($"Rota mapeada: {endpoint.DisplayName}");
                                }
                            }
                        });
                    });
                })
                .Build();

            return host.RunAsync();
        }
    }
}
