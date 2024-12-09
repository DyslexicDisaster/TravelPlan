namespace TravelPlan.API;
using RestSharp;
using System.Text.Json;
using System.Threading.Tasks;

public class FlightService
{
    private static readonly string FlightUrl = "https://sky-scanner3.p.rapidapi.com/flights/auto-complete";
    private static readonly string ApiKey = "4ac221ab62msh45294704f41436ep15de59jsnc1087c1ec456";

    public async Task<string> GetFlightSuggestions(string query)
    {
        var client = new RestClient(FlightUrl);
        var request = new RestRequest("",Method.Get);

        // Add parameters and headers
        request.AddQueryParameter("query", query);
        request.AddHeader("x-rapidapi-key", ApiKey);
        request.AddHeader("x-rapidapi-host", "sky-scanner3.p.rapidapi.com");

        // Execute request
        var response = await client.ExecuteAsync(request);

        if (response.StatusCode == System.Net.HttpStatusCode.OK && !string.IsNullOrWhiteSpace(response.Content))
        {
            return response.Content; // Returns JSON response as string
        }
        else
        {
            throw new Exception($"Error fetching flight suggestions. Status Code: {response.StatusCode}, Message: {response.ErrorMessage}");
        }
    }
}