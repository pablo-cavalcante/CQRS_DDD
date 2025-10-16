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
    public class FrutaAPITests
    {
        #region MyRegion
        private HttpClient _client { get; set; }

        public FrutaAPITests()
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.CreateClient();
        } 
        #endregion

        [Test]
        public async Task GetAll_ReturnsSuccessAndList()
        #region MyRegion
        {
            var response = await _client.GetAsync("api/Frutas");
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
            TestContext.WriteLine("🔍 Iniciando teste de CRUD de fruta...");

            #region CreateFrutaCommand
            //Inicio da criação de nova fruta
            var command = new CreateFrutaCommand
            {
                Nome = "BasicCrudProcess",
                Qtde = 1
            };

            var response = await _client.PostAsJsonAsync("api/Frutas", command);
            var apiResponse = await response.Content.ReadFromJsonAsync<APIResponse>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(apiResponse, Is.Not.Null);

            FrutasEntity basicCrudProcessResponseData =
                JsonConvert.DeserializeObject<FrutasEntity>(apiResponse.data.ToString());

            Assert.That(basicCrudProcessResponseData.Nome, Is.EqualTo("BasicCrudProcess"));
            Assert.That(basicCrudProcessResponseData.Qtde, Is.EqualTo(1));
            //FIM 
            #endregion
            TestContext.WriteLine("➡️ Teste Create concluído com sucesso!");

            #region GetById
            //Teste do carregamento unitário
            var idOld = basicCrudProcessResponseData.FrutasEntityId;
            response = await _client.GetAsync($"api/Frutas/{basicCrudProcessResponseData.FrutasEntityId}");
            apiResponse = await response.Content.ReadFromJsonAsync<APIResponse>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(apiResponse!.message, Is.EqualTo("OK"));
            Assert.That(apiResponse.data, Is.Not.Null);

            basicCrudProcessResponseData =
                JsonConvert.DeserializeObject<FrutasEntity>(apiResponse.data.ToString());

            Assert.That(basicCrudProcessResponseData.FrutasEntityId, Is.EqualTo(idOld));
            //FIM 
            #endregion
            TestContext.WriteLine("➡️ Teste GetById concluído com sucesso!");

            #region WhenNotExists
            //Teste do carregamento unitário
            response = await _client.GetAsync($"api/Frutas/99999999");
            apiResponse = await response.Content.ReadFromJsonAsync<APIResponse>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(apiResponse!.message, Is.EqualTo("Fruta não encontrada"));
            Assert.That(apiResponse.data, Is.Null);
            //FIM 
            #endregion
            TestContext.WriteLine("➡️ Teste WhenNotExists concluído com sucesso!");

            #region UpdateFrutaCommand
            //Inicio da atualização da fruta
            var commandUpdate = new UpdateFrutaCommand
            {
                FrutasEntityId = basicCrudProcessResponseData.FrutasEntityId,
                Nome = "BasicCrudProcessUpdate",
                Qtde = 2
            };

            response = await _client.PutAsJsonAsync("api/Frutas", commandUpdate);
            apiResponse = await response.Content.ReadFromJsonAsync<APIResponse>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(apiResponse, Is.Not.Null);

            basicCrudProcessResponseData =
                JsonConvert.DeserializeObject<FrutasEntity>(apiResponse.data.ToString());

            Assert.That(basicCrudProcessResponseData.Nome, Is.EqualTo("BasicCrudProcessUpdate"));
            Assert.That(basicCrudProcessResponseData.Qtde, Is.EqualTo(2));
            //FIM 
            #endregion
            TestContext.WriteLine("➡️ Teste Update concluído com sucesso!");

            #region DeleteFrutaCommand
            //Inicio da remoção da fruta
            var commandUDelete = new DeleteFrutaCommand
            {
                FrutasEntityId = basicCrudProcessResponseData.FrutasEntityId
            };

            var request = new HttpRequestMessage(HttpMethod.Delete, "api/Frutas")
            { Content = JsonContent.Create(commandUDelete) };

            response = await _client.SendAsync(request);
            apiResponse = await response.Content.ReadFromJsonAsync<APIResponse>();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(apiResponse, Is.Not.Null);

            basicCrudProcessResponseData =
                JsonConvert.DeserializeObject<FrutasEntity>(apiResponse.data.ToString());

            Assert.That(apiResponse!.status, Is.EqualTo(0));
            //FIM 
            #endregion
            TestContext.WriteLine("➡️ Teste Delete concluído com sucesso!");

            TestContext.WriteLine("✅ Todos os testes concluídos com sucesso!");
        } 
        #endregion
    }
}