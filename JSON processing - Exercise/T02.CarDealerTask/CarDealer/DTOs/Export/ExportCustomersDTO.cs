using Newtonsoft.Json;

namespace CarDealer.DTOs.Import;

public class ExportCustomersDTO
{
    [JsonProperty("Name")]
    public string Name { get; set; } = null!;

    [JsonProperty("BirthDate")]
    public string BirthDate { get; set; }

    [JsonProperty("IsYoungDriver")]
    public bool IsYoungDriver { get; set; }
}

