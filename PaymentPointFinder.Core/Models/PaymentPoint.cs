using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace PaymentPointFinder.Core.Models;

public class PaymentPoint
{
    [JsonPropertyName("IDEXT")]
    [DataMember]
    public string IDEXT { get; set; }

    [JsonPropertyName("DENOM")]
    [DataMember]
    public string DENOM { get; set; }

    [JsonPropertyName("INDIRIZZO")]
    [DataMember]
    public string INDIRIZZO { get; set; }

    [JsonPropertyName("SOTTOCAT")]
    [DataMember]
    public string SOTTOCAT { get; set; }

    [JsonPropertyName("SERVIZIO")]
    [DataMember]
    public string SERVIZIO { get; set; }

    [JsonPropertyName("BRAND")]
    [DataMember]
    public string BRAND { get; set; }

    [JsonPropertyName("LINK")]
    [DataMember]
    public string LINK { get; set; }

    [JsonPropertyName("LINK3")]
    [DataMember]
    public string LINK3 { get; set; }
    
    // This string has been renamed due to a name collision.
    [JsonPropertyName("XWGS84String")]
    [DataMember]
    public string XWGS84String { get; set; }

    [JsonPropertyName("CAP")]
    [DataMember]
    public int CAP { get; set; }

    [JsonPropertyName("MUNICIPIO")]
    [DataMember]
    public int MUNICIPIO { get; set; }

    [JsonPropertyName("ID_NIL")]
    [DataMember]
    public int ID_NIL { get; set; }

    [JsonPropertyName("NIL")]
    [DataMember]
    public string NIL { get; set; }

    // Longitude
    [JsonPropertyName("xWGS84")]
    [DataMember]
    public double xWGS84 { get; set; }

    // Latitude
    [JsonPropertyName("YWGS84")]
    [DataMember]
    public double YWGS84 { get; set; }

    [JsonPropertyName("Location")]
    [DataMember]
    public string Location { get; set; }
}