using CsvHelper;
using Movie.Data.HelperClasses;
using Movie.Data.Interfaces;
using Movie.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Movie.Services
{
    public class RetriveMetaDataService : IRetriveMetaData
    {
        public OperationResult<List<StatModel>> GetAllMoviesStatistics()
        {
            List<MetaDataModel> metaDataList = new List<MetaDataModel>();
            List<StatModel> returnedstatsList = new List<StatModel>();
            try
            {
                using (var reader = new StreamReader($"../Movie.Data/SourceData/metadata.csv", Encoding.Default))
                {
                    using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csvReader.Context.RegisterClassMap<MetaDataModelMap>();
                        metaDataList = csvReader.GetRecords<MetaDataModel>().ToList();
                    }
                }
                using (var reader = new StreamReader($"../Movie.Data/SourceData/stats.csv", Encoding.Default))
                {
                    using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csvReader.Context.RegisterClassMap<StatModelMap>();
                        var statsList = csvReader.GetRecords<StatModel>().ToList();
                        var groupedStatsList = statsList.GroupBy(x => new { x.MovieId})
                                .Select(gsl => new {
                                    Count = gsl.Count(),
                                    AvgWatches = gsl.Sum(x=>x.AvgWatchDurationS) / gsl.Count() / 1000,
                                    ID = gsl.Key.MovieId
                                })
                                .OrderByDescending(x => x.Count)
                                .ToList();

                        foreach(var item in groupedStatsList)
                        {
                            var movieMetaData = metaDataList.Where(x => x.MovieId == item.ID);
                            if (movieMetaData != null && movieMetaData.Any())
                            {
                                StatModel stat = new StatModel()
                                {
                                    MovieId = item.ID,
                                    Title = movieMetaData.FirstOrDefault().Title,
                                    AvgWatchDurationS = item.AvgWatches,
                                    Watches = item.Count,
                                    ReleaseYear = movieMetaData.FirstOrDefault().ReleaseYear
                                };
                                returnedstatsList.Add(stat);
                            }
                        }
                    }
                }
                return OperationResult<List<StatModel>>.CreateSuccessResult(returnedstatsList
                                                                            .OrderByDescending(x=>x.Watches)
                                                                            .ThenByDescending(x=>x.ReleaseYear)
                                                                            .ToList());
            }
            catch(Exception ex)
            {
                var error = new GeneralError($"An exception has occurred. Could not load the meta data for movies.");
                return OperationResult<List<StatModel>>.CreateFailure(error.Error, ex);
            }
        }

        public OperationResult<List<MetaDataModel>> GetMetaDataByMovieId(int inputMovieId)
        {
            List<MetaDataModel> metaDataByMovieId = new List<MetaDataModel>();
            try
            {
                using (var reader = new StreamReader($"../Movie.Data/SourceData/metadata.csv", Encoding.Default))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Context.RegisterClassMap<MetaDataModelMap>();
                        var recordsList = csv.GetRecords<MetaDataModel>().ToList();
                        if (recordsList!=null && recordsList.Count() > 0)
                        {
                            metaDataByMovieId = recordsList.Where(x => x.MovieId == inputMovieId)
                                                            .OrderByDescending(x => x.Id)
                                                            .GroupBy(x => new { x.Language })
                                                            .Select(x => x.FirstOrDefault())
                                                            .OrderBy(x => x.Language)
                                                            .ToList();
                            return metaDataByMovieId.Count() > 0 ?
                                OperationResult<List<MetaDataModel>>.CreateSuccessResult(metaDataByMovieId) :
                                OperationResult<List<MetaDataModel>>.CreateFailure(new GeneralError($"No meta data is " +
                                                 $"available in a source meta data file for movie id " +
                                                $": '{inputMovieId}'").Error);
                        }
                        else
                            return OperationResult<List<MetaDataModel>>.CreateFailure(new GeneralError($"No meta data has been" +
                                                                                  $" posted for movie id " +
                                                                                  $": '{inputMovieId}'").Error);
                    }
                }
            }
            catch(Exception ex)
            {
                var error = new GeneralError($"Could not load the meta data for movie id : '{inputMovieId}'");
                return OperationResult<List<MetaDataModel>>.CreateFailure(error.Error, ex);
            }
        }
    }
}
