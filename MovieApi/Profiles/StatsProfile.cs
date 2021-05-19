using AutoMapper;
using Movie.Data.Models;
using MovieApi.Models;

namespace MovieApi.Profiles
{
    public class StatsProfile : Profile
    {
        public StatsProfile()
        {
            CreateMap<StatModel, StatListingModel>();
        }
    }
}
