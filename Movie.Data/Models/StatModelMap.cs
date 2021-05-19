using CsvHelper.Configuration;

namespace Movie.Data.Models
{
    public sealed class StatModelMap : ClassMap <StatModel> 
    {
        public StatModelMap()
        {
            Map(x => x.MovieId).Name("movieId");
            Map(x => x.AvgWatchDurationS).Name("watchDurationMs");
        }
    }
}
