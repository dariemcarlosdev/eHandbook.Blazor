using Moq;
using Moq.Protected;
using System.Net;

namespace eHandbook.BlazorWebApp.UnitTest.Helpers
{
    internal static class Helpers
    {

        //HttpMessageHandlerMock is a method that returns a mock object of HttpMessageHandler, this method is used to intercept the call to the API and return a predefined response.
        internal static Mock<HttpMessageHandler> HttpMessageHandlerMock(string expectedUri, string ApiJsonResponse)
        {
            //here handlerMock is the mock object that we are going to use to intercept the call to the API, we are going to use it to return a predefined response.
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(
                        req => // We are intercepting the call to the API and returning a predefined response
                        req.Method == HttpMethod.Put || //HttpMethod.Put is used to update the manual
                        req.Method == HttpMethod.Delete || //HttpMethod.Delete is used to delete the manual
                        req.Method == HttpMethod.Get || //HttpMethod.Get is used to get the manual
                        req.Method == HttpMethod.Post && //HttpMethod.Post is used to create the manual
                        req.RequestUri.ToString().EndsWith(expectedUri)),
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
