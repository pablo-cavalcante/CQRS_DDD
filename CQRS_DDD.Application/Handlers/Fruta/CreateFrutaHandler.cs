using CQRS_DDD.Application.Commands.Fruta;
using CQRS_DDD.Domain.Entities;
using CQRS_DDD.Domain.Interfaces;
using CQRS_DDD.Domain.Responses;
using MediatR;
using RabbitMQ.Client;
using System.Text;

#pragma warning disable
namespace CQRS_DDD.Application.Handlers.Fruta
{
    public class CreateFrutaHandler : IRequestHandler<CreateFrutaCommand, APIResponse>
    {
        private readonly IFrutaRepository _context;

        public CreateFrutaHandler(IFrutaRepository context)
        #region MyRegion
        {
            _context = context;
        }
        #endregion

        public async Task<APIResponse> Handle(CreateFrutaCommand request, CancellationToken cancellationToken)
        #region MyRegion
        {
            try
            {
                var fruta = new FrutasEntity
                {
                    Nome = request.Nome,
                    Qtde = request.Qtde,
                    Ativa = request.Ativa
                };

                _context.BeginTransaction();
                await _context.Add(fruta, cancellationToken);
                _context.CommitTransaction();

                APIResponse response = new();
                response.setSuccessResponse("Fruta criada com sucesso!", fruta);

                this.SendComunicacaoAsync(request, cancellationToken);
                return response;
            }
            catch (Exception ex)
            {
                _context.RollbackTransaction();
                throw new ApplicationException($"Erro ao criar fruta '{request.Nome}': {ex.Message}", ex);
            }
        }
        #endregion

        protected async Task SendComunicacaoAsync(CreateFrutaCommand request, CancellationToken stoppingToken)
        #region MyRegion
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "admin"
            };

            // Cria conexão e canal de forma assíncrona
            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            // Declara a fila (caso ainda não exista)
            await channel.QueueDeclareAsync(
                queue: "frutas.criada",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            // Converte a mensagem em bytes
            var body = Encoding.UTF8.GetBytes($"Fruta criada: {request.Nome}");

            // Publica de forma assíncrona
            await channel.BasicPublishAsync(
                exchange: string.Empty,      // exchange padrão
                routingKey: "frutas.criada",    // nome da fila
                body: body);
        } 
        #endregion
    }
}