using System.ComponentModel.DataAnnotations;

namespace WeatherServer.Models;

public class City
{
	[Required]
	public int Id { get; set; }

	[Required]
	public string Name { get; set; } = "";

	public DateTime LastUpdated {get; set; }
	public float Temperature { get; set; }
	public string? Condition {get; set; }
	public float WindSpeed { get; set; }
	public string? WindDirection {get; set; }
	public float Humidity {get; set; }

	public float Precipitation {get; set; }
	public float UV {get; set; }
}