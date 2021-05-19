using Movie.Data.HelperClasses;
using Movie.Data.Models;

namespace Movie.Data.Interfaces
{
    public interface IAddMetaData
    {
        OperationResult<MetaDataModel> InsertMetaData(MetaDataModel inputMetaData);
    }
}
