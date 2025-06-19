using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class OpenRouterChatService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenRouterChatService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenRouter:ApiKey"] ?? throw new InvalidOperationException("Hugging Face API key is not configured.");
        }

        public async Task<string> GetAIResponseAsync(string message)
        {
            var requestBody = new
            {
                model = "deepseek/deepseek-chat-v3-0324:free",
                messages = new[]
                {
            new { role = "user", content = message }
        },
                max_tokens = 500,
                temperature = 0.3
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://openrouter.ai/api/v1/chat/completions");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey); // Use OpenRouter API key
            request.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Status Code: {response.StatusCode}, Content: {content}");

                if (!response.IsSuccessStatusCode)
                {
                    return $"Error: {response.StatusCode} - {content}";
                }

                try
                {
                    using var doc = JsonDocument.Parse(content);

                    if (doc.RootElement.TryGetProperty("choices", out JsonElement choices) &&
                        choices.GetArrayLength() > 0 &&
                        choices[0].TryGetProperty("message", out JsonElement messageElem) &&
                        messageElem.TryGetProperty("content", out JsonElement contentElem))
                    {
                        return contentElem.GetString() ?? "No response content.";
                    }
                    else if (doc.RootElement.TryGetProperty("error", out JsonElement error))
                    {
                        return $"AI Error: {error.GetString()}";
                    }
                    else
                    {
                        return "Unexpected response format.";
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"JSON Parsing Error: {ex.Message}");
                    return "Failed to parse AI response.";
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network Error: {ex.Message}");
                return $"Network error: {ex.Message}";
            }
        }

    }
}
