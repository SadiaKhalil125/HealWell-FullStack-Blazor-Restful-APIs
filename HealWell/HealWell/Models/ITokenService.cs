namespace HealWell.Models
{
    // ITokenService.cs
    public interface ITokenService
    {
        Task SetTokenAsync(string token);
        Task<string> GetTokenAsync();
        Task RemoveTokenAsync();
    }

}
