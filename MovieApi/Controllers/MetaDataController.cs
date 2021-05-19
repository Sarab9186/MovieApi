using Microsoft.AspNetCore.Mvc;
using Movie.Data.Interfaces;
using Movie.Data.Models;
using MovieApi.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using AutoMapper;

namespace MovieApi.Controllers
{
    [ApiController]
    public class MetaDataController : ControllerBase
    {
        private readonly IAddMetaData _addMetaData = null;
        private readonly IRetriveMetaData _getMetaData = null;
        private readonly IMapper _mapper;

        public MetaDataController(IAddMetaData addMetaData,
                                  IRetriveMetaData getMetaData,
                                  IMapper mapper)
        {
            _addMetaData = addMetaData;
            _getMetaData = getMetaData;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("api/movies/[controller]")]
        public ActionResult<MetaDataListingModel> MetaData([FromBody] MetaDataModel inputMetaData)
        {
            var metaDataModel  = _addMetaData.InsertMetaData(inputMetaData);
            if (metaDataModel.Success && metaDataModel.Result != null)
            {
                return Ok(_mapper.Map<MetaDataListingModel>(metaDataModel.Result));
            }
            else if (!metaDataModel.Success
                        && metaDataModel.Failed
                        && !metaDataModel.IsException
                        && !string.IsNullOrEmpty(metaDataModel.NonSuccessMessage))
                return StatusCode(StatusCodes.Status500InternalServerError, metaDataModel.NonSuccessMessage);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, metaDataModel.Exception);
        }
        [HttpGet]
        [Route("api/movies/{movieId}/[controller]")]
        public ActionResult<List<MetaDataListingModel>> MetaData(int movieId)
        {
            List<MetaDataListingModel> returnedMovieMetaData = new List<MetaDataListingModel>();
            var returnedMovieMetaDataModel = _getMetaData.GetMetaDataByMovieId(movieId);
            if(returnedMovieMetaDataModel.Success && returnedMovieMetaDataModel.Result!=null)
            {
                return Ok(_mapper.Map<List<MetaDataListingModel>>(returnedMovieMetaDataModel.Result));
            }
            else if(!returnedMovieMetaDataModel.Success
                        && returnedMovieMetaDataModel.Failed
                        && !returnedMovieMetaDataModel.IsException
                        && !string.IsNullOrEmpty(returnedMovieMetaDataModel.NonSuccessMessage))
                return StatusCode(StatusCodes.Status404NotFound, returnedMovieMetaDataModel.NonSuccessMessage);
            else
              return StatusCode(StatusCodes.Status500InternalServerError, returnedMovieMetaDataModel.Exception);
        }
    }
}
