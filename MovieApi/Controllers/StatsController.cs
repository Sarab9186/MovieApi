using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Data.Interfaces;
using MovieApi.Models;
using System.Collections.Generic;

namespace MovieApi.Controllers
{
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IRetriveMetaData _getStatService = null;
        private readonly IMapper _mapper = null;
        public StatsController(IRetriveMetaData getStatService,
                               IMapper mapper)
        {
            _getStatService = getStatService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("api/movies/[controller]")]
        public ActionResult<List<StatListingModel>> Stats()
        {
            List<StatListingModel> allMovieStats = new List<StatListingModel>();
            var returnMovieStatsModel = _getStatService.GetAllMoviesStatistics();
            if (returnMovieStatsModel.Success && returnMovieStatsModel.Result != null)
            {
                return Ok(_mapper.Map<List<StatListingModel>>(returnMovieStatsModel.Result));
            }
            else if (!returnMovieStatsModel.Success
                       && returnMovieStatsModel.Failed
                       && !returnMovieStatsModel.IsException
                       && !string.IsNullOrEmpty(returnMovieStatsModel.NonSuccessMessage))
                return StatusCode(StatusCodes.Status404NotFound, returnMovieStatsModel.NonSuccessMessage);
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
