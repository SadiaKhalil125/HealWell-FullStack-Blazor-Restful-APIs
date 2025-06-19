using Microsoft.JSInterop;

namespace HealWell.Models
{
    // TokenService.cs

    public class TokenService : ITokenService
    {
        private readonly IJSRuntime _js;

        public TokenService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task SetTokenAsync(string token)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", "authToken", token);
        }

        public async Task<string> GetTokenAsync()
        {
            return await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
        }

        public async Task RemoveTokenAsync()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", "authToken");
        }
    }

}
