using CQRS_DDD.Application.Commands.Fruta;
using CQRS_DDD.Domain.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Http.Json;
using System.Text;

#pragma warning disable
namespace CQRS_DDD.Application.Services
{
    public class RabbitMqConsumerService : BackgroundService
    {
        private readonly ILogger<RabbitMqConsumerService> _logger;
        private IConnection? _connection;
        private IChannel? _channel;
        private readonly ConnectionFactory _factory;
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _comunicadoLojaEndpoint;

        private const string QueueName = "frutas.criada";

        public RabbitMqConsumerService(ILogger<RabbitMqConsumerService> logger, HttpClient httpClient, IConfiguration configuration)
        #region MyRegion
        {
            _logger = logger;
            _httpClient = httpClient;

            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "admin", // ajuste conforme seu ambiente
                Password = "admin"
            };

            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _comunicadoLojaEndpoint = configuration["ApiSettings:ComunicadoLojaEndpoint"];
        }
        #endregion

        public override async Task StartAsync(CancellationToken cancellationToken)
        #region MyRegion
        {
            _logger.LogInformation("🔌 Iniciando consumidor RabbitMQ...");

            _connection = await _factory.CreateConnectionAsync(cancellationToken);
            _channel = await _connection.CreateChannelAsync();

            // Garante que a fila exista
            await _channel.QueueDeclareAsync(
                queue: QueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: cancellationToken);

            _logger.LogInformation("✅ Fila '{Queue}' pronta para consumo.", QueueName);

            await base.StartAsync(cancellationToken);
        }
        #endregion

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        #region MyRegion
        {
            if (_channel == null)
            {
                _logger.LogError("❌ Canal RabbitMQ não foi inicializado.");
                return;
            }

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (sender, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    _logger.LogInformation("📩 Mensagem recebida da fila '{Queue}': {Message}", QueueName, message);

                    // Aqui você pode processar a mensagem:
                    // Ex: chamar um serviço de domínio, CQRS handler, etc.

                    #region CreateComunicadoLojaCommand

                    var command = new CreateComunicadoLojaCommand
                            {
                                CiMsg = message,
                                LojaEntityId = 1,
                                Ativa = true
                            };

                    var response = await _httpClient.PostAsJsonAsync(_baseUrl + _comunicadoLojaEndpoint, command);
                    var apiResponse = await response.Content.ReadFromJsonAsync<APIResponse>();

                    if (apiResponse.isErrorResponse())
                    {
                        throw new Exception(apiResponse.message);
                    }

                    #endregion

                    await Task.CompletedTask;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao processar mensagem RabbitMQ.");
                }
            };

            // Inicia consumo assíncrono
            await _channel.BasicConsumeAsync(
                queue: QueueName,
                autoAck: true,
                consumer: consumer,
                cancellationToken: stoppingToken);

            _logger.LogInformation("👂 Ouvindo mensagens da fila '{Queue}'...", QueueName);

            // Mantém o serviço ativo enquanto a API estiver rodando
            await Task.Delay(Timeout.Infinite, stoppingToken);
        } 
        #endregion

        public override async Task StopAsync(CancellationToken cancellationToken)
        #region MyRegion
        {
            _logger.LogInformation("🛑 Encerrando consumidor RabbitMQ...");

            if (_channel != null)
                await _channel.CloseAsync(cancellationToken);

            _connection?.CloseAsync();
            await base.StopAsync(cancellationToken);
        } 
        #endregion
    }
}
