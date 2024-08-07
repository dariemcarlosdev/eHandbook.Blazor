using eHandbook.BlazorWebApp.Shared.Domain.EntitiesModels;
using eHandbook.BlazorWebApp.Shared.Services;
using Moq;
using Moq.Protected;
using System.Text.Json;
using static eHandbook.BlazorWebApp.UnitTest.Helpers.Helpers;
using eHandbook.BlazorWebApp.Shared.Domain.DTOs.Manual;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.Intrinsics.X86;

namespace eHandbook.BlazorWebApp.UnitTest
{
    public class EHandBookApiServiceUnitTest
    {
        private readonly string baseApiUrl = "https://localhost:7001/";


        //Fact() is an attribute that tells the test runner that the method is a test method.
        [Fact]
        /*
            Endpoint: /api/V2/manuals/create
            Http Method: Post.
            Parameters: N/A
            Request Body *: 
            ManualToCreateDto {
            description*:  string minLength: 1
            path*:  string minLength: 1
        */
        //Implementation Status: Completed - Best xUnit Test Method implement. for testing the CreateItemAsync method.
        public async Task AddItemAsync_ReturnMessageSuccess_WhenApiCallIsSuccessFul()
        {
            // Arrange: In this part of the test, you set up everything you need to run the test.
            //In arrange section you should set up the expectedUri, the manualToCreate and the apiResponse that we are going to return when the API is called.
            //1.Test Data: Any data that your test will use.
            //2.Expected Results: Any expected results that you will compare against the actual results.
            //3.Mocks and Stubs: Mock objects or stubs to simulate dependencies.
            //4.System Under Test(SUT):he instance of the class or method you are testing.

            //1.Test Data
            var manualToCreateTest= new ManualToCreateDto //Test Data
            (
                Description: "open_source_solutions_vermont.zirz",
                Path: "/var/mail/roi.aep"
            );
            var expectedUri = "api/V2/manuals/create"; //Expected URI

            //2.Expected Result
            //Any expected results that you will compare against the actual results.
            //apiResponse simulate the response that we are going to return when the API is called.
            var apiExpectedResponse = new ApiResponseService<ManualDto>
            {
                Data = manualToCreateTest,
                Metadata = new()
                {
                    Message = "Reponse OK!. Manual Created successfuly.",
                    Succeeded = true
                }
            };

            //3.Mocks and Stubs

            //Mocked API Response: ApiJsonResponse is the response that we are going to return when the API is called.
            var ApiJsonResponse = JsonSerializer.Serialize(apiExpectedResponse);

            //Moking Dependencies: Mock objects or stubs to simulate dependencies.
            //Mocks the HttpMessageHandler: to intercept calls made by HttpClient and returns a predefined response.
            Mock<HttpMessageHandler> handlerMock = HttpMessageHandlerMock(expectedUri, ApiJsonResponse);
            // HttpClient with mocked handler
            var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri(baseApiUrl) };


            //4.System Under Test (SUT)
            var service = new EHandBookApiService(httpClient);

            // Act:
            // you execute the method or functionality that you are testing. This is where you perform the action that you want to test. The Act section is typically straightforward and involves calling the method on the System Under Test (SUT) with the necessary parameters.
            // The result of the method call is captured in the result variable. This result will be used in the Assert section to verify the behavior of the method.
            var actualResult = await service.CreateItemAsync(manualToCreateTest);

            // Assert
            //The Assert section is where you verify that the method you are testing behaves as expected. You compare the actual result of the method with the expected result to determine if the method is working correctly.
            Assert.NotNull(actualResult); //assert result is not null which means the result is not null thereby response was successful.
            Assert.Equal(apiExpectedResponse.Metadata.Message, actualResult.Metadata.Message); //the results's message is equal to the expected message
            handlerMock.Protected().Verify( //The Http request was sent once with the expected parameters, method and uri.
            "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri.ToString().EndsWith(expectedUri)),
                ItExpr.IsAny<CancellationToken>()
            );

        }


        [Fact]
        /*
            Endpoint: /api/V2/manuals/update
            Http Method: Put
            Parameters: N/A
            Request Body*:
            ManualToUpdateDto {
            id*:	string($uuid)
            description*: string minLength: 1
            path*: string minLength: 1
            }
        */
        //Implementation Status: Completed -- Best xUnit Test Method implement. for testing the CreateItemAsync method.
        public async Task UpdateItemAsync_ReturnMessageSuccess_WhenApiCallIsSuccessFul()
        {
            // Arrange: In this part of the test, you set up everything you need to run the test.
            //In arrange section you should set up the expectedUri, the manualToCreate and the apiResponse that we are going to return when the API is called.
            //1.Test Data: Any data that your test will use.
            //2.Expected Results: Any expected results that you will compare against the actual results.
            //3.Mocks and Stubs: Mock objects or stubs to simulate dependencies.
            //4.System Under Test(SUT):he instance of the class or method you are testing.

            //1.Test Data
            var manualId = "65b927ed-b563-4db4-9594-002fd5379178";
            var expectedUri = $"api/V2/manuals/update"; //Expected URI

            var manualToUpdateTest = new ManualToUpdateDto(
                Guid.Parse(manualId),
                "new description",
                "new description");

            //2.Expected Result
            //Any expected results that you will compare against the actual results.
            //apiResponse simulate the response that we are going to return when the API is called.
            var apiExpectedResponse = new ApiResponseService<ManualDto>
            {
                Data = manualToUpdateTest,
                Metadata = new()
                {
                    Message = "Reponse OK!. Manual Updated successfuly.",
                    Succeeded = true
                }
            };

            //3.Mocks and Stubs

            //Mocked API Response: ApiJsonResponse is the response that we are going to return when the API is called.
            var ApiJsonResponse = JsonSerializer.Serialize(apiExpectedResponse);

            //Moking Dependencies: Mock objects or stubs to simulate dependencies.
            //Mocks the HttpMessageHandler: to intercept calls made by HttpClient and returns a predefined response.
            //handlerMock is the mock object that we are going to use to intercept the call to the API, we are going to use it to return a predefined response.
            Mock<HttpMessageHandler> handlerMock = HttpMessageHandlerMock(expectedUri, ApiJsonResponse);

            // HttpClient with mocked handler
            // httpClient is a new instance of HttpClient that we are going to use to make the request to the API
            var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri(baseApiUrl) };

            //4.System Under Test (SUT)
            // service is the instance of the class that we are going to test and we pass the httpClient as a parameter to the constructor
            var service = new EHandBookApiService(httpClient);

            // Act:
            // you execute the method or functionality that you are testing. This is where you perform the action that you want to test. The Act section is typically straightforward and involves calling the method on the System Under Test (SUT) with the necessary parameters.
            // The result of the method call is captured in the result variable. This result will be used in the Assert section to verify the behavior of the method.
            var actualResult = await service.UpdateManualAsync(manualToUpdateTest);

            // Assert
            Assert.NotNull(actualResult); //assert result is not null which means the result is not null thereby response was successful.
            //Assert.Equal(manualToUpdate, actualResult.Data); // assert the result is equal to the expected result
            Assert.Equal(apiExpectedResponse.Metadata.Message, actualResult.Metadata.Message); //the results's message is equal to the expected message
            handlerMock.Protected().Verify(//The Http request was sent once with the expected parameters, method and uri.
            "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Put && req.RequestUri.ToString().EndsWith(expectedUri)),
                ItExpr.IsAny<CancellationToken>()
            );

        }


        [Fact]
        //Implementation Status: Completed -- Best xUnit Test Method implement. for testing the CreateItemAsync method.
        /*
            Endpoint: /api/V2/manuals/{Id}
            Http Method: Get.
            Parameters: Id* string($uuid)
            Request Body: N/A
        */
        public async Task GetItemAsync_ReturnsManual_WhenApiCallIsSuccessful()
        {
            // Arrange: In this part of the test, you set up everything you need to run the test.
            //In arrange section you should set up the expectedUri, the manualToCreate and the apiResponse that we are going to return when the API is called.
            //1.Test Data: Any data that your test will use.
            //2.Expected Results: Any expected results that you will compare against the actual results.
            //3.Mocks and Stubs: Mock objects or stubs to simulate dependencies.
            //4.System Under Test(SUT):he instance of the class or method you are testing.

            //1.Test Data
            var manualId = "65B927ED-B563-4DB4-9594-002FD5379178";
            var expectedUri = $"api/V2/manuals/{manualId}"; //Expected URI
            
            // Create new instance of ManualDto record using positional parameters
            var manualToTest = new ManualDto(
                Id: Guid.Parse(manualId),
                Description: "open_source_solutions_vermont.zirz",
                Path: "/var/mail/roi.aep",
                AuditableDetails: new AuditableDetailsDto(
                    CreatedBy: "335286",
                    CreatedOn: DateTime.Parse("2024-03-08T19:04:18.6591281"),
                    UpdatedBy: null,
                    UpdatedOn: null,
                    IsUpdated: false,
                    DeletedOn: null,
                    DeletedBy: null,
                    IsDeleted: false
                )
            );

            //2.Expected Result
            //Any expected results that you will compare against the actual results.
            //apiResponse simulate the response that we are going to return when the API is called.
            var apiExpectedResponse = new ApiResponseService<ManualDto>
            {
                Data = manualToTest,
                Metadata = new()
                {
                    Message = "Reponse OK!. Manual retrieved successfuly.",
                    Succeeded = true
                }
            };

            //3.Mocks and Stubs
            //ApiJsonResponse is the response that we are going to return when the API is called.
            var ApiJsonResponse = JsonSerializer.Serialize(apiExpectedResponse);

            //Moking Dependencies: Mock objects or stubs to simulate dependencies.
            //Mocks the HttpMessageHandler: to intercept calls made by HttpClient and returns a predefined response.
            //handlerMock is the mock object that we are going to use to intercept the call to the API, we are going to use it to return a predefined response.
            Mock<HttpMessageHandler> handlerMock = HttpMessageHandlerMock(expectedUri, ApiJsonResponse);

            // HttpClient with mocked handler
            // httpClient is a new instance of HttpClient that we are going to use to make the request to the API
            var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri(baseApiUrl) };

            //4.System Under Test (SUT)
            //service is the instance of the class that we are going to test and we pass the httpClient as a parameter to the constructor
            var service = new EHandBookApiService(httpClient);

            //2.Act(Exercise):
            // you execute the method or functionality that you are testing. This is where you perform the action that you want to test. The Act section is typically straightforward and involves calling the method on the System Under Test (SUT) with the necessary parameters.
            // The result of the method call is captured in the result variable. This result will be used in the Assert section to verify the behavior of the method.

            var actualResult = await service.GetItemAsync(manualId);

            //3.Assert(Verify):
            //is the part of the test where you verify that the method you are testing behaves as expected.

            Assert.NotNull(actualResult); //assert result is not null which means the result is not null thereby response was successful.
            Assert.Equal(manualToTest, actualResult.Data);
            Assert.Equal(apiExpectedResponse.Metadata.Message, actualResult.Metadata.Message); //the results's message is equal to the expected message
            handlerMock.Protected().Verify( //Verify is used to verify that the method SendAsync was called once with the expected parameters and the expected response.
            "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().EndsWith(expectedUri)),
                ItExpr.IsAny<CancellationToken>()
            );
        }


        [Fact]
        /*
           Endpoint: /api/V2/manuals/
           Http Method: Get.
           Parameters: 
           Query String: Filters(string), Sort(string), Page($int32), PageSize($int32)
           Request Body: N/A
       */
        //Implementation Status: Completed -- Best xUnit Test Method implement. for testing the CreateItemAsync method.
        public async Task GetItemsAsync_ReturnsManualList_WhenApiCallIsSuccessfull()
        {
            // Arrange: In this part of the test, you set up everything you need to run the test.
            //In arrange section you should set up the expectedUri, the manualToCreate and the apiResponse that we are going to return when the API is called.
            //1.Test Data: Any data that your test will use.
            //2.Expected Results: Any expected results that you will compare against the actual results.
            //3.Mocks and Stubs: Mock objects or stubs to simulate dependencies.
            //4.System Under Test(SUT):he instance of the class or method you are testing.

            //1.Test Data
            var expectedUri = "api/V2/manuals"; //Expected URI

            var manualsCollectionToTest = new List<ManualDto> {
            new ManualDto(
                Guid.Parse("65B927ED-B563-4DB4-9594-002FD5379178"),
                "open_source_solutions_vermont.zirz",
                "/var/mail/roi.aep",
                new AuditableDetailsDto(CreatedBy: "335286",
                                    CreatedOn: DateTime.Parse("2024-03-08T19:04:18.6591281"),
                                    UpdatedBy: null,
                                    UpdatedOn: null,
                                    IsUpdated: false,
                                    DeletedOn: null,
                                    DeletedBy: null,
                                    IsDeleted: false))
            };


            //2.Expected Result
            //Any expected results that you will compare against the actual results.
            //apiResponse simulate the response that we are going to return when the API is called.
            var apiExpectedResponse = new ApiResponseService<IEnumerable<ManualDto>> { 
                Data = manualsCollectionToTest,
                Metadata = new()
                {
                    Message = "Reponse OK!. Manuals retrieved successfuly.",
                    Succeeded = true
                }
            };

            //3.Mocks and Stubs

            //Mocked API Response: ApiJsonResponse is the response that we are going to return when the API is called.
            var ApiJsonResponse = JsonSerializer.Serialize(apiExpectedResponse);

            //Moking Dependencies: Mock objects or stubs to simulate dependencies.
            //Mocks the HttpMessageHandler: to intercept calls made by HttpClient and returns a predefined response.
            //handlerMock is the mock object that we are going to use to intercept the call to the API, we are going to use it to return a predefined response.
            Mock<HttpMessageHandler> handlerMock = HttpMessageHandlerMock(expectedUri, ApiJsonResponse);


            // HttpClient with mocked handler
            // httpClient is a new instance of HttpClient that we are going to use to make the request to the API
            var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri(baseApiUrl) };

            //4.System Under Test (SUT)
            // service is the instance of the class that we are going to test and we pass the httpClient as a parameter to the constructor
            var service = new EHandBookApiService(httpClient);

            // you execute the method or functionality that you are testing. This is where you perform the action that you want to test. The Act section is typically straightforward and involves calling the method on the System Under Test (SUT) with the necessary parameters.
            // The result of the method call is captured in the result variable. This result will be used in the Assert section to verify the behavior of the method.
            var actualResult = await service.GetManualsAsync();

            // Assert
            Assert.NotNull(actualResult); //assert result is not null which means the result is not null thereby response was successful.
            Assert.Equal(manualsCollectionToTest.Count(), actualResult.Data.Count()); // Assuming your ApiResponsService has a Data property
            Assert.Equal(apiExpectedResponse.Metadata.Message, actualResult.Metadata.Message); //the results's message is equal to the expected message
            handlerMock.Protected().Verify(//The Http request was sent once with the expected parameters, method and uri.
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().EndsWith(expectedUri)),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        /*
        Endpoint: /api/V2/manuals/delete/{Id}
        Http Method: Put
        Parameters: Id* string($uuid)
        Request Body*: N/A
        */
        //Implementation Status: Completed -- Best xUnit Test Method implement. for testing the CreateItemAsync method.
        public async Task SoftDeleteItemAsync_ReturnMessageSussess_WhenApiCallIsSuccessful()
        {
            // Arrange: In this part of the test, you set up everything you need to run the test.
            //In arrange section you should set up the expectedUri, the manualToCreate and the apiResponse that we are going to return when the API is called.
            //1.Test Data: Any data that your test will use.
            //2.Expected Results: Any expected results that you will compare against the actual results.
            //3.Mocks and Stubs: Mock objects or stubs to simulate dependencies.
            //4.System Under Test(SUT):he instance of the class or method you are testing.

            //1.Test Data
            var manualId = "65b927ed-b563-4db4-9594-002fd5379178";
            var expectedUri = $"api/V2/manuals/delete/{manualId}"; //Expected URI

            //2.Expected Result
            //Any expected results that you will compare against the actual results.
            //apiResponse simulate the response that we are going to return when the API is called.
            var apiExpectedResponse = new ApiResponseService<ManualDto> { 
                Metadata = new() { 
                    Message = "Manual deleted successfully" 
                } 
            };

            //3.Mocks and Stubs
            //Mocked API Response: ApiJsonResponse is the response that we are going to return when the API is called.
            var ApiJsonResponse = JsonSerializer.Serialize(apiExpectedResponse);

            //Moking Dependencies: Mock objects or stubs to simulate dependencies.
            //Mocks the HttpMessageHandler: to intercept calls made by HttpClient and returns a predefined response.
            //handlerMock is the mock object that we are going to use to intercept the call to the API, we are going to use it to return a predefined response.
            Mock<HttpMessageHandler> handlerMock = HttpMessageHandlerMock(expectedUri, ApiJsonResponse);


            // HttpClient with mocked handler
            // httpClient is a new instance of HttpClient that we are going to use to make the request to the API and test.
            var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri(baseApiUrl) };


            //4.System Under Test (SUT)
            // service is the instance of the class that we are going to test and we pass the httpClient as a parameter to the constructor
            var service = new EHandBookApiService(httpClient);

            // Act: Is the part of the test where you execute the method or functionality that you are testing. This is where you perform the action that you want to test. The Act section is typically straightforward and involves calling the method on the System Under Test (SUT) with the necessary parameters.

            // you execute the method or functionality that you are testing. This is where you perform the action that you want to test. The Act section is typically straightforward and involves calling the method on the System Under Test (SUT) with the necessary parameters.
            // The result of the method call is captured in the result variable. This result will be used in the Assert section to verify the behavior of the method.
            var actualResult = await service.SoftDeleteItemAsync(manualId);

            // Assert: is the part of the test where you verify that the method you are testing behaves as expected.
            
            Assert.NotNull(actualResult); //assert result is not null which means the result is not null thereby response was successful
            Assert.Equal(apiExpectedResponse.Metadata.Message, actualResult.Metadata.Message); //the results's message is equal to the expected message
            handlerMock.Protected().Verify(  //The Http request was sent once with the expected parameters, method and uri.
            "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Put && req.RequestUri.ToString().EndsWith(expectedUri)),
                ItExpr.IsAny<CancellationToken>()
            );
        }


        [Fact]
        /* 
        Endpoint: /api/V2/manuals/delete
        Http Method: Delete
        Parameters: N/A
        Request Body*:
        ManualToDeleteDto {
        id*:	string ($uuid)
        description*: string minLength: 1
        path*: string minLength: 1
        }
        */
        //Implementation Status: Completed -- Best xUnit Test Method implement. for testing the CreateItemAsync method.
        public async Task HardDeleteItemAsync_ReturnMessageSussess_WhenApiCallIsSuccessful()
        {
            // Arrange: In this part of the test, you set up everything you need to run the test.
            //In arrange section you should set up the expectedUri, the manualToCreate and the apiResponse that we are going to return when the API is called.
            //1.Test Data: Any data that your test will use.
            //2.Expected Results: Any expected results that you will compare against the actual results.
            //3.Mocks and Stubs: Mock objects or stubs to simulate dependencies.
            //4.System Under Test(SUT):he instance of the class or method you are testing.

            //1.Test Data
            var expectedUri = "api/V2/manuals/delete"; //Expected URI
            var manualToDeleteTest = new ManualToDeleteDto(
                Guid.Parse("65b927ed-b563-4db4-9594-002fd5379178"),
                "open_source_solutions_vermont.zirz",
                "/var/mail/roi.aep"
            );

            //2.Expected Result
            //Any expected results that you will compare against the actual results.
            //apiResponse simulate the response that we are going to return when the API is called.

            var apiExpectedResponse = new ApiResponseService<ManualDto>
            {
                Data = manualToDeleteTest,
                Metadata = new()
                {
                    Message = "Manual deleted successfully"
                }
            };

            //3.Mocks and Stubs

            //Mocked API Response: ApiJsonResponse is the response that we are going to return when the API is called.
            var ApiJsonResponse = JsonSerializer.Serialize(apiExpectedResponse);

            //Moking Dependencies: Mock objects or stubs to simulate dependencies.
            //Mocks the HttpMessageHandler: to intercept calls made by HttpClient and returns a predefined response.
            //handlerMock is the mock object that we are going to use to intercept the call to the API, we are going to use it to return a predefined response.
            Mock<HttpMessageHandler> handlerMock = HttpMessageHandlerMock(expectedUri, ApiJsonResponse);

            // HttpClient with mocked handler
            // httpClient is a new instance of HttpClient that we are going to use to make the request to the API
            var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri(baseApiUrl) };

            //4.System Under Test (SUT)
            // service is the instance of the class that we are going to test and we pass the httpClient as a parameter to the constructor
            var service = new EHandBookApiService(httpClient);


            // you execute the method or functionality that you are testing. This is where you perform the action that you want to test. The Act section is typically straightforward and involves calling the method on the System Under Test (SUT) with the necessary parameters.
            // The result of the method call is captured in the result variable. This result will be used in the Assert section to verify the behavior of the method.
            var actualResult = await service.HardDeleteManualAsync(manualToDeleteTest);

            // Assert. is the part of the test where you verify that the method you are testing behaves as expected.

            Assert.NotNull(actualResult); //assert result is not null which means the result is not null thereby response was successful.
            Assert.Equal(apiExpectedResponse.Metadata.Message, actualResult.Metadata.Message); //the results's message is equal to the expected message
            handlerMock.Protected().Verify( //The Http request was sent once with the expected parameters, method and uri.
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Delete && req.RequestUri.ToString().EndsWith(expectedUri)),
                ItExpr.IsAny<CancellationToken>()
            );
        }


        [Fact]
        /*
        Endpoint: /api/V2/manuals/deleteBy/{Id}
        Http Method: Delete
        Parameters: Id* string($uuid)
        Request Body*: N/A
        */
        //Implementation Status:---  Best xUnit Test Method implement. for testing the CreateItemAsync method.
        public async Task HardDeleteItemByIdAsync_ReturnMessageSussess_WhenApiCallIsSuccessful()
        {
            // Arrange: In this part of the test, you set up everything you need to run the test.
            //In arrange section you should set up the expectedUri, the manualToCreate and the apiResponse that we are going to return when the API is called.
            //1.Test Data: Any data that your test will use.
            //2.Expected Results: Any expected results that you will compare against the actual results.
            //3.Mocks and Stubs: Mock objects or stubs to simulate dependencies.
            //4.System Under Test(SUT):he instance of the class or method you are testing.

            //1.Test Data
            var manualId = "65b927ed-b563-4db4-9594-002fd5379178";
            var expectedUri = $"api/V2/manuals/deleteBy/{manualId}"; //Expected URI

            //2.Expected Result
            //Any expected results that you will compare against the actual results.
            //apiResponse simulate the response that we are going to return when the API is called.
            var apiExpectedResponse = new ApiResponseService<ManualDto>
            {
                Metadata = new()
                {
                    Message = "Manual deleted successfully"
                }
            };

            //3.Mocks and Stubs
            //Mocked API Response: ApiJsonResponse is the response that we are going to return when the API is called.
            var ApiJsonResponse = JsonSerializer.Serialize(apiExpectedResponse);

            //Moking Dependencies: Mock objects or stubs to simulate dependencies.
            //Mocks the HttpMessageHandler: to intercept calls made by HttpClient and returns a predefined response.
            //handlerMock is the mock object that we are going to use to intercept the call to the API, we are going to use it to return a predefined response.
            Mock<HttpMessageHandler> handlerMock = HttpMessageHandlerMock(expectedUri, ApiJsonResponse);

            // HttpClient with mocked handler
            // httpClient is a new instance of HttpClient that we are going to use to make the request to the API
            var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri(baseApiUrl) };

            //4.System Under Test (SUT)
            // service is the instance of the class that we are going to test and we pass the httpClient as a parameter to the constructor
            var service = new EHandBookApiService(httpClient);

            // Act: Is the part of the test where you execute the method or functionality that you are testing. This is where you perform the action that you want to test. The Act section is typically straightforward and involves calling the method on the System Under Test (SUT) with the necessary parameters.

            // you execute the method or functionality that you are testing. This is where you perform the action that you want to test. The Act section is typically straightforward and involves calling the method on the System Under Test (SUT) with the necessary parameters.
            // The result of the method call is captured in the result variable. This result will be used in the Assert section to verify the behavior of the method.
            var actualResult = await service.HardDeleteManualByIdAsync(manualId);

            // Assert: is the part of the test where you verify that the method you are testing behaves as expected.
            Assert.NotNull(actualResult); //assert result is not null which means the result is not null thereby response was successful.
            Assert.Equal(apiExpectedResponse.Metadata.Message, actualResult.Metadata.Message); //the results's message is equal to the expected message
            handlerMock.Protected().Verify(//The Http request was sent once with the expected parameters, method and uri.
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Delete && req.RequestUri.ToString().EndsWith(expectedUri)),
                ItExpr.IsAny<CancellationToken>()
            );
        }
    }
}