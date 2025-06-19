// Services/ChatService.cs
using System.Net.Http.Json;

// ChatService.cs
public class ChatService 
{
    private readonly HttpClient _httpClient;

    public ChatService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ChatMessage>> GetMessagesAsync(int userId)
    {
        return await _httpClient.GetFromJsonAsync<List<ChatMessage>>($"https://localhost:7047/api/chat/{userId}") ?? new();
    }

    public async Task<ChatMessage> SendMessageAsync(ChatMessage message)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7047/api/chat", message);
        return await response.Content.ReadFromJsonAsync<ChatMessage>() ?? new();
    }
}

