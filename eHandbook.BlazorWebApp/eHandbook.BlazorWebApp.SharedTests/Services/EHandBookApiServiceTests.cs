using Moq;
using Moq.Protected;
using System.Net;
using Xunit;
using eHandbook.BlazorWebApp.Shared.Services;
using eHandbook.BlazorWebApp.Shared.Domain.Entities;
using System.Text.Json;

namespace eHandbook.BlazorWebApp.SharedTests.Services;

public class EHandBookApiServiceTests
{
    [Fact]
    public async Task GetItemAsync_ReturnsManual_WhenApiCallIsSuccessful()
    {
        // Arrange
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
            Data = manual,
            Metadata = {
            Message = "Manual Found by ID.Reponse OK",
            MyCustomErrorMessage = null,
            Succeeded = true
        }
        };

        var json = JsonSerializer.Serialize(apiResponse);
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().EndsWith(expectedUri)),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json),
            })
            .Verifiable();

        var httpClient = new HttpClient(handlerMock.Object);
        var service = new EHandBookApiService(httpClient);

        // Act
        var result = await service.GetItemAsync(manualId);

        // Assert
        Assert.IsNotNull(result);
        Assert.Equals(/* expected values */manual, /* actual values from result */result);
        handlerMock.Protected().Verify(
        "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().EndsWith(expectedUri)),
            ItExpr.IsAny<CancellationToken>()
        );
    }
}