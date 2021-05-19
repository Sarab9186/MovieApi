using Movie.Data.HelperClasses;
using Movie.Data.Interfaces;
using Movie.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Movie.Services
{
    public class AddMetaDataService : IAddMetaData
    {
        public OperationResult<MetaDataModel> InsertMetaData(MetaDataModel inputMetaData)
        {
            var randomId = new Random();
            try
            {
                var database = new List<MetaDataModel>()
                {
                    new MetaDataModel()
                    {
                        Id = randomId.Next(152,50000),
                        MovieId = inputMetaData.MovieId,
                        Title = inputMetaData.Title,
                        Language = inputMetaData.Language,
                        Duration = inputMetaData.Duration,
                        ReleaseYear = inputMetaData.ReleaseYear
                    }
                };
                if(database != null && database.Count()==1)
                    return OperationResult<MetaDataModel>.CreateSuccessResult(database.FirstOrDefault());
                else
                    return OperationResult<MetaDataModel>.CreateFailure(new GeneralError
                                                            ($"Could not add meta data for movie id : '{inputMetaData.MovieId}'")
                                                            .Error);
            }
            catch(Exception ex)
            {
                var error = new GeneralError($"Exception occurred. Could not add meta data for movie id : '{inputMetaData.MovieId}'");
                return OperationResult<MetaDataModel>.CreateFailure(error.Error,ex);
            }
            
        }
    }
}
