using System.Text.Json.Serialization;
public class FeatureCollection
{
    // TODO Problem 5 - ADD YOUR CODE HERE
    // Create additional classes as necessary
    [JsonPropertyName("features")]
    public List<Feature> Features { get; set; } = new List<Feature>();
}

public class Feature
{
    [JsonPropertyName("properties")]
    public Properties Properties { get; set; } = new Properties();
}

public class Properties
{
    [JsonPropertyName("place")]
    public string Place { get; set; } = string.Empty;

    [JsonPropertyName("mag")]
    public double? Mag { get; set; }  

    [JsonPropertyName("time")]
    public long Time { get; set; }
}