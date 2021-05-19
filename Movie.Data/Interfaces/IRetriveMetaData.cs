using Movie.Data.HelperClasses;
using Movie.Data.Models;
using System.Collections.Generic;

namespace Movie.Data.Interfaces
{
    public interface IRetriveMetaData
    {
        OperationResult<List<MetaDataModel>> GetMetaDataByMovieId(int inputMovieId);
        OperationResult<List<StatModel>> GetAllMoviesStatistics();
    }
}
