namespace WebApplication7.Services
{
    public interface IRandomNumberService
    {
        Task<int> GetRandomNumberAsync(string input);
    }

}
