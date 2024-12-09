using System.Text.Json;
using RestSharp;

namespace TravelPlan.API
{
    public class WeatherService
    {
        private static readonly string ApiBaseUrl = "https://api.weatherapi.com/v1/forecast.json";
        private static readonly string ApiKey = "fe06063a5a5744a28c512325240912";

        public async Task<WeatherApiResponse> GetThreeDayForecast(string city)
        {
            // Initialize RestClient with the base URL
            var client = new RestClient(ApiBaseUrl);

            // Create the request
            var request = new RestRequest("", Method.Get);

            // Add query parameters
            request.AddQueryParameter("key", ApiKey);
            request.AddQueryParameter("q", city);
            request.AddQueryParameter("days", "3");

            // Execute the request
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful && !string.IsNullOrWhiteSpace(response.Content))
            {
                // Deserialize JSON response
                return JsonSerializer.Deserialize<WeatherApiResponse>(response.Content);
            }

            // Handle failed responses
            throw new Exception($"Failed to fetch weather data. Status code: {response.StatusCode}, Message: {response.Content ?? response.ErrorMessage}");
        }
    }
}