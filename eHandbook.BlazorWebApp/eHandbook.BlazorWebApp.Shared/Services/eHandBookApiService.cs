using eHandbook.BlazorWebApp.Shared.Domain.DTOs.Manual;
using eHandbook.BlazorWebApp.Shared.Domain.EntitiesModels;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace eHandbook.BlazorWebApp.Shared.Services
{
    /// <summary>
    /// Using Typed clients to encapsulate the logic for interacting with a specific web API,
    /// making our code cleaner and more maintainable.
    /// </summary>
    public class EHandBookApiService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<ApiResponsService<ManualDto>> CreateItemAsync(ManualToCreateDto manualToCreate)
        {
            //crate implementation for this method
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(manualToCreate), Encoding.UTF8, "application/json");
                //_httpClient.PostAsync sends a POST request to the specified Uri as an asynchronous operation.
                //Parameter Content is the HTTP request content sent to the server which is serialized to JSON.
                var response = await _httpClient.PostAsync($"api/V2/manuals/create", content);
                //verify was successful
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true, // Makes the deserializer case-insensitive
                        DefaultIgnoreCondition = JsonIgnoreCondition.Never // Include null values
                    };
                    var apiResponse = JsonSerializer.Deserialize<ApiResponsService<ManualDto>>(responseString, options);
                    return apiResponse;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null; // Return null instead of a new instance.This can be appropiated to issue a message back to user if response was null.
            }


        }

        public async Task<ApiResponsService<ManualDto>> UpdateManualAsync(ManualToUpdateDto manualToUpdate)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(manualToUpdate), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/V2/manuals/update", content);
                //verify if response was successful
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {

                    var responseString = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true, // Makes the deserializer case-insensitive
                        DefaultIgnoreCondition = JsonIgnoreCondition.Never // Include null values
                    };
                    var apiResponse = JsonSerializer.Deserialize<ApiResponsService<ManualDto>>(responseString, options);
                    return apiResponse;

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null; // Return null instead of a new instance.This can be appropiated to issue a message back to user if response was null.
            }
        }

        /// <summary>
        /// this method is used to get all manuals
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponsService<IEnumerable<ManualDto>>> GetManualsAsync()
        {
            try
            {
                var responseString = await _httpClient.GetStringAsync("api/V2/manuals");
                //verify if response was successful
                if (string.IsNullOrEmpty(responseString))
                {
                    return null;
                }

                else
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true, // Makes the deserializer case-insensitive
                        DefaultIgnoreCondition = JsonIgnoreCondition.Never // Include null values
                    };
                    var apiResponse = JsonSerializer.Deserialize<ApiResponsService<IEnumerable<ManualDto>>>(responseString, options);
                    return apiResponse;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null; // Return null instead of a new instance.This can be appropiated to issue a message back to user if response was null.
            }
        }

        /// <summary>
        /// This method is used to get a manual by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponsService<ManualDto>> GetItemAsync(string id)
        {
            try
            {

                var responseString = await _httpClient.GetStringAsync($"api/V2/manuals/{id}");
                //verify if response was successful
                if (string.IsNullOrEmpty(responseString))
                {
                    return null;
                }
                else
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true, // Makes the deserializer case-insensitive
                        DefaultIgnoreCondition = JsonIgnoreCondition.Never // Include null values
                    };
                    var apiResponse = JsonSerializer.Deserialize<ApiResponsService<ManualDto>>(responseString, options);
                    return apiResponse;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null; // Return null instead of a new instance.This can be appropiated to issue a message back to user if response was null.
            }
        }

        /// <summary>
        /// This method is used to create a new manual
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponsService<ManualDto>> SoftDeleteItemAsync(string id)
        {
            try
            {
                var content = new StringContent(" ", Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/V2/manuals/delete/{id}", content);
                //verify response was succefull
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true, // Makes the deserializer case-insensitive
                        DefaultIgnoreCondition = JsonIgnoreCondition.Never // Include null values
                    };
                    var apiResponse = JsonSerializer.Deserialize<ApiResponsService<ManualDto>>(responseString, options);
                    return apiResponse;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null; // Return null instead of a new instance.This can be appropiated to issue a message back to user if response was null.
            }
        }

        public async Task<ApiResponseService<ManualDto>> HardDeleteManualAsync(ManualToDeleteDto manualToDeleteDto)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(manualToDeleteDto), Encoding.UTF8, "application/json");

                // request is an instance of HttpRequestMessage that represents a HTTP request message including the method, headers, and content sent to the server.
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri("api/V2/manuals/delete",uriKind:UriKind.Relative),
                    Content = content
                };

                // Send the DELETE request to the server
                var response = await _httpClient.SendAsync(request);

                // Check if the response was successful, if not return null
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.Never
                    };
                    var apiResponse = JsonSerializer.Deserialize<ApiResponseService<ManualDto>>(responseString, options);
                    return apiResponse;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ApiResponseService<ManualDto>> HardDeleteManualByIdAsync(string manualId)
        {
            try
            {
                // request is an instance of HttpRequestMessage that represents a HTTP request message including the method, headers, and content sent to the server.
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete, // Set the HTTP method to DELETE
                    RequestUri = new Uri($"api/V2/manuals/deleteBy/{manualId}", uriKind: UriKind.Relative)
                };

                // Send the DELETE request to the server
                var response = await _httpClient.SendAsync(request);

                // Check if the response was successful, if not return null
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.Never
                    };
                    var apiResponse = JsonSerializer.Deserialize<ApiResponseService<ManualDto>>(responseString, options);
                    return apiResponse;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}




