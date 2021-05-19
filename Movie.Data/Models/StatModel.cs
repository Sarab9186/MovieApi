using Newtonsoft.Json;
using System;

namespace Movie.Data.Models
{
    public class StatModel
    {
        [JsonProperty("movieId")]
        public int MovieId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("averageWatchDurationS")]
        public Int64 AvgWatchDurationS { get; set; }
        [JsonProperty("watches")]
        public int Watches { get; set; }
        [JsonProperty("releaseYear")]
        public int ReleaseYear { get; set; }
    }
}
