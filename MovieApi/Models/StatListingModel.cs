using System;

namespace MovieApi.Models
{
    public class StatListingModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public Int64 AvgWatchDurationS { get; set; }
        public int Watches { get; set; }
        public int ReleaseYear { get; set; }
    }
}
