using MatchInfo.API.Filters;
using MatchInfo.API.Models;
using MatchInfo.API.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MatchInfo.API.Controllers
{
    /// <summary>
    /// A class for MatchesController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : BaseApiController
    {
        /// <summary>
        /// Ctor for MatchesController.
        /// </summary>
        /// <param name="uow"></param>
        public MatchesController(UnitOfWork.UnitOfWorkMatchInfo uow) : base(uow)
        {
        }

        /// <summary>
        /// Get all matches.
        /// </summary>
        /// <param name="includeMatchOdds">Optionally request that records include match odds for each match.</param>
        /// <returns>A list of matches</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public ActionResult<IEnumerable<MatchDto>> GetAll(bool includeMatchOdds = false)
        {
            _logger.LogInformation($"GetAll matches [includeMatchOdds = {includeMatchOdds}]");
            var matchesDtos = this.uowMatchInfo.MatchesRepository.GetAll(asNoTracking: true, includeMatchOdds: includeMatchOdds);
            _logger.LogInformation($"GetAll matches OK");
            return Ok(matchesDtos);
        }

        /// <summary>
        /// Get match by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeMatchOdds">Optionally request that records include match odds for each match.</param>
        /// <returns>A match</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NotAcceptable)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("{id}")]
        public ActionResult<MatchDto> GetById([Required] int id,bool includeMatchOdds = false)
        {
            _logger.LogInformation($"GetById match [ID = {id}],  [includeMatchOdds = {includeMatchOdds}]");
            var matchDto = this.uowMatchInfo.MatchesRepository.GetById(id:id, asNoTracking: true, includeMatchOdds: includeMatchOdds);
            _logger.LogInformation($"GetById match OK");
            return Ok(matchDto);
        }

        /// <summary>
        /// Inserts a match.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>An ActionResult of type MatchDto.</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotAcceptable)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public ActionResult<MatchDto> Post([Required][FromBody] MatchDto model)
        {          
            _logger.LogInformation($"Post match [{model.TeamA}, {model.TeamB}, {model.MatchDate}, {model.MatchTime}]");
            var insertedItem = this.uowMatchInfo.MatchesRepository.Insert(model: model);
            _logger.LogInformation($"Post match OK");
            return Ok(insertedItem);
        }

        /// <summary>
        /// Updates an existing match.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>TAn ActionResult of type MatchDto.</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NotAcceptable)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public ActionResult<MatchDto> Put([Required]int id,[Required][FromBody] MatchDto model)
        {
            _logger.LogInformation($"Put match [{model.TeamA}, {model.TeamB}, {model.MatchDate}, {model.MatchTime}]");
            var updatedItem = uowMatchInfo.MatchesRepository.Update( model);
            _logger.LogInformation($"Put match OK");
            return Ok(updatedItem);
        }

        /// <summary>
        /// Deletes a match.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The action result.</returns>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NotAcceptable)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            _logger.LogWarning($"Delete match with id {id}");
            uowMatchInfo.MatchesRepository.Delete(id: id);
            _logger.LogWarning($"Delete match OK");
            return NoContent();
        }
    }
}
