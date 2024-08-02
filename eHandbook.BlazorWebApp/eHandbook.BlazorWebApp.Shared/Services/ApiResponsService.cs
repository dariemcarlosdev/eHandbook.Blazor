using eHandbook.BlazorWebApp.Shared.Domain.EntitiesModels;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace eHandbook.BlazorWebApp.Shared.Services
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class ApiResponsService<T>
    {
        [JsonPropertyName("metaData")]
        public Metadata? Metadata { get; set; }
        [JsonPropertyName("data")]
        public T? Data { get; set; }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
