using System.Text.Json;
using System.Text.Json.Nodes;
using WeatherServer.Models;


#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
namespace WeatherServer.Helpers
{
	public class WeatherAPI
	{
		private string API_Key = "";
		private readonly IConfiguration Configuration;


		public WeatherAPI(IConfiguration configuration)
		{
			Configuration = configuration;
            API_Key = Configuration["API_Key"];
        }

		public async Task<City> GetCityAsync(string Name)
		{
			if (String.IsNullOrEmpty(Name))
			{
				throw new ArgumentNullException(nameof(Name));
			}
			City city = new();

			var requestURI = Configuration["Server_URI"].Replace("%TOKEN%", API_Key).Replace("%NAME%", Name);
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, requestURI);
			var response = await client.SendAsync(request);
			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsStringAsync();
            JsonNode weatherNode = JsonNode.Parse(content);

            city.LastUpdated = DateTime.Now;
			city.Name = weatherNode!["location"]!["name"].GetValue<string>();
			city.Temperature = weatherNode!["current"]!["temp_c"].GetValue<float>();
			city.Condition = weatherNode!["current"]!["condition"]!["text"].GetValue<string>();
			city.WindSpeed = weatherNode!["current"]!["wind_kph"].GetValue<float>();
			city.WindDirection = weatherNode!["current"]!["wind_dir"].GetValue<string>();
			city.Humidity = weatherNode!["current"]!["humidity"].GetValue<float>();
			city.Precipitation = weatherNode!["current"]!["precip_mm"].GetValue<float>();
			city.UV = weatherNode!["current"]!["uv"].GetValue<float>();
			return city;
		}
	}
}