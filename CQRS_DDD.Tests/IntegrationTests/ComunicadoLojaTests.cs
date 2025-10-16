using System.Net;
using System.Net.Http.Json;
using CQRS_DDD.Application.Commands.Fruta;
using CQRS_DDD.Domain.Entities;
using CQRS_DDD.Domain.Responses;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;

#pragma warning disable
namespace CQRS_DDD.Tests.IntegrationTests
{
    [TestFixture]
    public class ComunicadoLojaTests
    {
        #region MyRegion
        private HttpClient _client { get; set; }

        public ComunicadoLojaTests()
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.CreateClient();
        }
        #endregion

        [Test]
        public async Task GetAll_ReturnsSuccessAndList()
        #region MyRegion
        {
            var response = await _client.GetAsync("api/ComunicadoLoja");
            var apiResponse = await response.Content.ReadFromJsonAsync<APIResponse>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse!.message, Is.EqualTo("OK"));
            Assert.That(apiResponse.data, Is.Not.Null);
        }
        #endregion

        [Test]
        public async Task BasicCrudProcess()
        #region MyRegion
        {
            TestContext.WriteLine("🔍 Iniciando teste de CRUD do comunicado da loja...");

            #region CreateFrutaCommand
            //Inicio da criação de nova fruta
            var command = new CreateComunicadoLojaCommand
            {
                CiMsg = "BasicCrudProcess",
                LojaEntityId = 1,
                Ativa = true
            };

            var response = await _client.PostAsJsonAsync("api/ComunicadoLoja", command);
            var apiResponse = await response.Content.ReadFromJsonAsync<APIResponse>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(apiResponse, Is.Not.Null);

            ComunicadoLojaEntity basicCrudProcessResponseData =
                JsonConvert.DeserializeObject<ComunicadoLojaEntity>(apiResponse.data.ToString());

            Assert.That(basicCrudProcessResponseData.CiMsg, Is.EqualTo("BasicCrudProcess"));
            Assert.That(basicCrudProcessResponseData.LojaEntityId, Is.EqualTo(1));
            //FIM 
            #endregion
            TestContext.WriteLine("➡️ Teste Create concluído com sucesso!");

            #region GetById
            //Teste do carregamento unitário
            var idOld = basicCrudProcessResponseData.CiLojaEntityId;
            response = await _client.GetAsync($"api/ComunicadoLoja/{basicCrudProcessResponseData.CiLojaEntityId}");
            apiResponse = await response.Content.ReadFromJsonAsync<APIResponse>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(apiResponse!.message, Is.EqualTo("OK"));
            Assert.That(apiResponse.data, Is.Not.Null);

            basicCrudProcessResponseData =
                JsonConvert.DeserializeObject<ComunicadoLojaEntity>(apiResponse.data.ToString());

            Assert.That(basicCrudProcessResponseData.CiLojaEntityId, Is.EqualTo(idOld));
            //FIM 
            #endregion
            TestContext.WriteLine("➡️ Teste GetById concluído com sucesso!");

            #region WhenNotExists
            //Teste do carregamento unitário
            response = await _client.GetAsync($"api/ComunicadoLoja/99999999");
            apiResponse = await response.Content.ReadFromJsonAsync<APIResponse>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(apiResponse!.message, Is.EqualTo("Comunicado não encontrado"));
            Assert.That(apiResponse.data, Is.Null);
            //FIM 
            #endregion
            TestContext.WriteLine("➡️ Teste WhenNotExists concluído com sucesso!");

            #region UpdateFrutaCommand
            //Inicio da atualização da fruta
            var commandUpdate = new UpdateComunicadoLojaCommand
            {
                CiLojaEntityId = basicCrudProcessResponseData.CiLojaEntityId,
                CiMsg = "BasicCrudProcessUpdate",
                LojaEntityId = 2
            };

            response = await _client.PutAsJsonAsync("api/ComunicadoLoja", commandUpdate);
            apiResponse = await response.Content.ReadFromJsonAsync<APIResponse>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(apiResponse, Is.Not.Null);

            basicCrudProcessResponseData =
                JsonConvert.DeserializeObject<ComunicadoLojaEntity>(apiResponse.data.ToString());

            Assert.That(basicCrudProcessResponseData.CiMsg, Is.EqualTo("BasicCrudProcessUpdate"));
            Assert.That(basicCrudProcessResponseData.LojaEntityId, Is.EqualTo(2));
            //FIM 
            #endregion
            TestContext.WriteLine("➡️ Teste Update concluído com sucesso!");

            #region DeleteFrutaCommand
            //Inicio da remoção da fruta
            var commandUDelete = new DeleteComunicadoLojaCommand
            {
                CiLojaEntityId = basicCrudProcessResponseData.CiLojaEntityId
            };

            var request = new HttpRequestMessage(HttpMethod.Delete, "api/ComunicadoLoja")
            { Content = JsonContent.Create(commandUDelete) };

            response = await _client.SendAsync(request);
            apiResponse = await response.Content.ReadFromJsonAsync<APIResponse>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(apiResponse, Is.Not.Null);

            basicCrudProcessResponseData =
                JsonConvert.DeserializeObject<ComunicadoLojaEntity>(apiResponse.data.ToString());

            Assert.That(apiResponse!.status, Is.EqualTo(0));
            //FIM 
            #endregion
            TestContext.WriteLine("➡️ Teste Delete concluído com sucesso!");

            TestContext.WriteLine("✅ Todos os testes concluídos com sucesso!");
        }
        #endregion
    }
}
