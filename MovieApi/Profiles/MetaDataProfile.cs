using AutoMapper;
using Movie.Data.Models;
using MovieApi.Models;

namespace MovieApi.Profiles
{
    public class MetaDataProfile : Profile
    {
        public MetaDataProfile()
        {
            CreateMap<MetaDataModel, MetaDataListingModel>();
        }
    }
}
