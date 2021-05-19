using System.Text.Json.Serialization;

namespace MovieApi.Models
{
    public class MetaDataListingModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Duration { get; set; }
        public int ReleaseYear { get; set; }
        [JsonIgnore]
        public int Id { get; set; }
    }
}
