using eHandbook.BlazorWebApp.Shared.Domain.Entities;
using eHandbook.BlazorWebApp.Shared.Services;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;

namespace eHandbook.BlazorWebApp.SharedTest
{
    public class EHandBookApiServiceTests
    {
        private readonly string baseApiUrl = "https://localhost:7001/";

        //Fact() is an attribute that tells the test runner that the method is a test method.
        [Fact]
        public async Task GetItemAsync_ReturnsManual_WhenApiCallIsSuccessful()
        {
            //1.Arrange(Setup): is the part of the test where you set up everything you need to run the test.

            var manualId = "65B927ED-B563-4DB4-9594-002FD5379178";
            var expectedUri = $"api/V2/manuals/{manualId}";
            var manual = new Manual(
                Guid.Parse(manualId),
                "open_source_solutions_vermont.zirz",
                "/var/mail/roi.aep",
                new AuditableDetails(CreatedBy: "335286",
                                    CreatedOn: DateTime.Parse("2024-03-08T19:04:18.6591281"),
                                    UpdatedBy: null,
                                    UpdatedOn: null,
                                    IsUpdated: false,
                                    DeletedOn: null,
                                    DeletedBy: null,
                                    IsDeleted: false));

            var apiResponse = new ApiResponsService<Manual>
            {
                Data = manual
            };
            //ApiJsonResponse is the response that we are going to return when the API is called.
            var ApiJsonResponse = JsonSerializer.Serialize(apiResponse);

            //Mocks the HttpMessageHandler: to intercept calls made by HttpClient and returns a predefined response.
            Mock<HttpMessageHandler> handlerMock = HttpMessageHandlerMock(expectedUri, ApiJsonResponse);

            //httpClient is the client that we are going to use to make the request to the API
            var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri(baseApiUrl) };

            //service is the instance of the class that we are going to test and we pass the httpClient as a parameter to the constructor
            var service = new EHandBookApiService(httpClient);

            //2.Act(Exercise):
            //is the part of the test where you actually run the method you are testing.

            var result = await service.GetItemAsync(manualId);

            //3.Assert(Verify):
            //is the part of the test where you verify that the method you are testing behaves as expected.

            Assert.NotNull(result);
            Assert.Equal(manual, result.Data);

            //handlerMock.Protected().Verify is used to verify that the method SendAsync was called once with the expected parameters and the expected response.
            handlerMock.Protected().Verify(
            "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().EndsWith(expectedUri)),
                ItExpr.IsAny<CancellationToken>()
            );



        }

        [Fact]
        public async Task GetItemsAsync_ReturnsManualList_WhenApiCallIsSuccessfull()
        {
            // Arrange
            var expectedUri = "api/V2/manuals";
            var manuals = new List<Manual> {
            new Manual(
                Guid.Parse("65B927ED-B563-4DB4-9594-002FD5379178"),
                "open_source_solutions_vermont.zirz",
                "/var/mail/roi.aep",
                new AuditableDetails(CreatedBy: "335286",
                                    CreatedOn: DateTime.Parse("2024-03-08T19:04:18.6591281"),
                                    UpdatedBy: null,
                                    UpdatedOn: null,
                                    IsUpdated: false,
                                    DeletedOn: null,
                                    DeletedBy: null,
                                    IsDeleted: false))
            };
            var apiResponse = new ApiResponsService<IEnumerable<Manual>> { Data = manuals };
            var ApiJsonResponse = JsonSerializer.Serialize(apiResponse);

            //Mocks the HttpMessageHandler: to intercept calls made by HttpClient and returns a predefined response.
            Mock<HttpMessageHandler> handlerMock2 = HttpMessageHandlerMock(expectedUri, ApiJsonResponse);

            var httpClient = new HttpClient(handlerMock2.Object) { BaseAddress = new Uri(baseApiUrl) };
            var service = new EHandBookApiService(httpClient);

            // Act
            var result = await service.GetItemsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(manuals.Count >= 1, result.Data.Count() >= 1); // Assuming your ApiResponsService has a Data property
            handlerMock2.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().EndsWith(expectedUri)),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async Task SoftDeleteItemAsync_ReturnMessageSussess_WhenApiCallIsSuccessful()
        {
            // Arrange
            var manualId = "65b927ed-b563-4db4-9594-002fd5379178";
            var expectedUri = $"api/V2/manuals/delete/{manualId}";
            var apiResponse = new ApiResponsService<Manual> { Metadata = new() { Message = "Manual deleted successfully" } };
            var ApiJsonResponse = JsonSerializer.Serialize(apiResponse);

            //Mocks the HttpMessageHandler: to intercept calls made by HttpClient and returns a predefined response.
            Mock<HttpMessageHandler> handlerMock = HttpMessageHandlerMock(expectedUri, ApiJsonResponse);

            var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri(baseApiUrl) };
            var service = new EHandBookApiService(httpClient);

            // Act
            var result = await service.SoftDeleteItemAsync(manualId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(apiResponse.Metadata.Message, result.Metadata.Message);
            handlerMock.Protected().Verify(
            "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Put && req.RequestUri.ToString().EndsWith(expectedUri)),
                ItExpr.IsAny<CancellationToken>()
            );  
        }
        //HttpMessageHandlerMock is a method that  
        static Mock<HttpMessageHandler> HttpMessageHandlerMock(string expectedUri, string ApiJsonResponse)
        {
            //here handlerMock is the mock object that we are going to use to intercept the call to the API, we are going to use it to return a predefined response.
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Put || req.Method == HttpMethod.Get && req.RequestUri.ToString().EndsWith(expectedUri)),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(ApiJsonResponse),
                })
                .Verifiable();
            return handlerMock;
        }
    }
}