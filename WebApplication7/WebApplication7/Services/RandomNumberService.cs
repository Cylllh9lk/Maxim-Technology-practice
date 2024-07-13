namespace WebApplication7.Services
{
    public class RandomNumberService : IRandomNumberService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public RandomNumberService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<int> GetRandomNumberAsync(string input)
        {
            int max = input.Length;
            string baseUrl = _configuration["ApiSettings:BaseUrl"];
            string apiUrl = $"{baseUrl}?min=0&max={max - 1}&count=1";

            HttpResponseMessage response = await _client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            responseBody = new string(responseBody.Where(char.IsDigit).ToArray());

            if (int.TryParse(responseBody, out int randomNumber))
            {
                return randomNumber;
            }
            else
            {
                throw new FormatException($"Некорректный формат данных: не удалось преобразовать '{responseBody}' в число.");
            }
        }
    }

}
