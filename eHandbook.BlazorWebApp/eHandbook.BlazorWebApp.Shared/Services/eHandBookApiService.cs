using eHandbook.BlazorWebApp.Shared.Domain.Entities;
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

        public Task CreateItemAsync(Manual item)
        {
            throw new NotImplementedException();
        }

        public Task<Manual> GetItem(string token)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTodoItemAsync(Manual item)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponsService<IEnumerable<Manual>>> GetItemsAsync()
        {
            try
            {
                var responseString = await _httpClient.GetStringAsync("api/V2/manuals");
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, // Makes the deserializer case-insensitive
                    DefaultIgnoreCondition = JsonIgnoreCondition.Never // Include null values
                };
                var apiResponse = JsonSerializer.Deserialize<ApiResponsService<IEnumerable<Manual>>>(responseString, options);
                return apiResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null; // Return null instead of a new instance.This can be appropiated to issue a message back to user if response was null.
            }
        }

        public async Task<ApiResponsService<Manual>> GetItemAsync(string id)
        {
            try
            {

                var responseString = await _httpClient.GetStringAsync($"api/V2/manuals/{id}");
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, // Makes the deserializer case-insensitive
                    DefaultIgnoreCondition = JsonIgnoreCondition.Never // Include null values
                };
                var apiResponse = JsonSerializer.Deserialize<ApiResponsService<Manual>>(responseString, options);
                return apiResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null; // Return null instead of a new instance.This can be appropiated to issue a message back to user if response was null.
            }
        }

        //crate amethod for delete item
        public async Task<ApiResponsService<Manual>> SoftDeleteItemAsync(string id)
        {
            try
            {
                var content = new StringContent(" ", Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/V2/manuals/delete/{id}", content);
                var responseString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, // Makes the deserializer case-insensitive
                    DefaultIgnoreCondition = JsonIgnoreCondition.Never // Include null values
                };
                var apiResponse = JsonSerializer.Deserialize<ApiResponsService<Manual>>(responseString, options);
                return apiResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null; // Return null instead of a new instance.This can be appropiated to issue a message back to user if response was null.
            }
        }
    }
}




