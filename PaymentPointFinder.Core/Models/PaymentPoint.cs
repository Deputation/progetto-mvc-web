using System.Collections;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace PaymentPointFinder.Core.Models;

public class PaymentPoint
{
    [JsonPropertyName("IDEXT")]
    public string IDEXT { get; set; }

    [JsonPropertyName("DENOM")]
    public string DENOM { get; set; }

    [JsonPropertyName("INDIRIZZO")]
    public string INDIRIZZO { get; set; }

    [JsonPropertyName("SOTTOCAT")]
    public string SOTTOCAT { get; set; }

    [JsonPropertyName("SERVIZIO")]
    public string SERVIZIO { get; set; }

    [JsonPropertyName("BRAND")]
    public string BRAND { get; set; }

    [JsonPropertyName("LINK")]
    public string LINK { get; set; }

    [JsonPropertyName("LINK3")]
    public string LINK3 { get; set; }
    
    // This string has been renamed due to a name collision.
    [JsonPropertyName("XWGS84String")]
    public string XWGS84String { get; set; }

    [JsonPropertyName("CAP")]
    public int CAP { get; set; }

    [JsonPropertyName("MUNICIPIO")]
    public int MUNICIPIO { get; set; }

    [JsonPropertyName("ID_NIL")]
    public int ID_NIL { get; set; }

    [JsonPropertyName("NIL")]
    public string NIL { get; set; }

    // Longitude
    [JsonPropertyName("xWGS84")]
    public double xWGS84 { get; set; }

    // Latitude
    [JsonPropertyName("YWGS84")]
    public double YWGS84 { get; set; }

    [JsonPropertyName("Location")]
    public string Location { get; set; }
    
    private Vector2 ToCartesian()
    {
        return new Vector2
        {
            X = (float)(6371 * Math.Cos(YWGS84) * Math.Cos(xWGS84)),
            Y = (float)(6371 * Math.Cos(YWGS84) * Math.Sin(xWGS84))
        };
    }
    
    public double Distance(PaymentPoint other)
    {
        var thisCartesian = ToCartesian();
        var otherCartesian = other.ToCartesian();
        var deltaX = thisCartesian.X - otherCartesian.X;
        var deltaY = thisCartesian.Y - otherCartesian.Y;
        
        return Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
    }
}