using Newtonsoft.Json;

namespace DiTask4.TestData.Models;

public class TestDataModel
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; } 
    
    [JsonProperty("isActive")]
    public bool IsActive { get; set; }
}