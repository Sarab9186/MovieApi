using CsvHelper.Configuration;

namespace Movie.Data.Models
{
    public sealed class MetaDataModelMap : ClassMap <MetaDataModel> 
    {
        public MetaDataModelMap()
        {
            Map(x => x.Id).Name("Id");
            Map(x => x.MovieId).Name("MovieId");
            Map(x => x.Title).Name("Title");
            Map(x => x.Language).Name("Language");
            Map(x => x.Duration).Name("Duration");
            Map(x => x.ReleaseYear).Name("ReleaseYear");

        }
    }
}
