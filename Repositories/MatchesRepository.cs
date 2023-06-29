using MatchInfo.API.DbContexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MatchInfo.API.Repositories
{
    /// <summary>
    /// A class for MatchesRepository.
    /// </summary>
    public class MatchesRepository : BaseRepository<Entities.Match>
    {
        /// <summary>
        /// The MatchOdd repository.
        /// </summary>
        internal MatchOddsRepository dbMatchOddsRepo;

        /// <summary>
        /// Ctor for MatchesRepository.
        /// </summary>
        /// <param name="context">EF Context.</param>
        /// <param name="mapper">The automapper to use for db-dto conversions.</param>
        public MatchesRepository(MatchInfoContext context, IMapper mapper, MatchOddsRepository _dbMatchOddsRepo) : base(context: context, mapper: mapper)
        {
            dbMatchOddsRepo = _dbMatchOddsRepo;
        }

        /// <summary>
        /// Get matches from db (as queryable).
        /// </summary>
        /// <param name="asNoTracking">Optionally request that matches are not tracked.</param>
        /// <returns></returns>
        public IQueryable<Entities.Match> Queryable(bool asNoTracking = false)
        {
            var q = (IQueryable<Entities.Match>)context.Matches;

            if (asNoTracking)
                q = q.AsNoTracking();

            return q;
        }

        /// <summary>
        /// Get matches from db (as queryable).
        /// </summary>
        /// <param name="asNoTracking">Optionally request that matches are not tracked.</param>
        /// <param name="includeMatchOdds">Optionally request that records include match odds for each match.</param>
        /// <returns></returns>
        public IQueryable<Entities.Match> GetQueryable(bool asNoTracking = false, bool includeMatchOdds = false)
        {
            var q = Queryable(asNoTracking: asNoTracking);

            if (includeMatchOdds)
                q = IncludeMatchOdds(q);

            return q;
        }

        /// <summary>
        /// Include match odds in the queryable.
        /// </summary>
        /// <param name="queryable">The queryable to expand.</param>
        /// <returns>The queryable which includes match odds.</returns>
        private static IQueryable<Entities.Match> IncludeMatchOdds(IQueryable<Entities.Match> queryable)
        {
            if (queryable is null) return queryable;

            queryable = queryable
                .Include(x => x.MatchOdds)                
            ;

            return queryable;
        }

        /// <summary>
        /// Gets all matches
        /// </summary>
        /// <param name="asNoTracking">Optionally request that records are not tracked.</param>
        /// <param name="includeMatchOdds">Optionally request that records include match odds for each match.</param>
        /// <returns>The match records</returns>
        public List<Models.MatchDto> GetAll(bool asNoTracking = false, bool includeMatchOdds = false)
        {
            var q = GetQueryable(asNoTracking: asNoTracking, includeMatchOdds: includeMatchOdds);

            q = q.OrderByDescending(x => x.MatchDateTime);

            List<Entities.Match> dbItems = q.ToList();

            var items = mapper.Map<List<Entities.Match>, List<Models.MatchDto>>(dbItems);

            return items;
        }

        /// <summary>
        /// Get match by identifier.
        /// </summary>
        /// <param name="id">The match identifier.</param>
        /// <param name="asNoTracking">Optionally request that records are not tracked.</param>
        /// <param name="includeMatchOdds">Optionally request that records include match odds for each match.</param>
        /// <returns>The match dto</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public Models.MatchDto GetById(long id, bool asNoTracking = false, bool includeMatchOdds = false)
        {
            var q = Queryable(asNoTracking: asNoTracking);
            if (includeMatchOdds)
                q = IncludeMatchOdds(q);
        
            var dbItem = q.FirstOrDefault(x => x.ID == id);

            if (dbItem == null)
                throw new KeyNotFoundException($"Match with id {id} does not exist");

            return mapper.Map<Entities.Match, Models.MatchDto>(dbItem);
        }
       
        /// <summary>
        /// Inserts new match.
        /// </summary>
        /// <param name="model">The match dto.</param>
        /// <returns></returns>
        public Models.MatchDto Insert(Models.MatchDto model)
        {
            if (model is null) return null;

            /* Map the dto on a new db istance to copy all essential data */
            Entities.Match dbItem = mapper.Map<Models.MatchDto, Entities.Match>(model);
            
            /* Insert match with match odds */
            dbItem.MatchOdds = mapper.Map<List<Models.MatchOddDto>, List<Entities.MatchOdd>>(model.MatchOddDtos);
            base.Insert(dbItem);

            return mapper.Map<Entities.Match, Models.MatchDto>(dbItem);
        }

        /// <summary>
        /// Updates an existing match.
        /// </summary>
        /// <param name="model">The match dto.</param>
        /// <returns>The updated match.</returns>
        /// <exception cref="KeyNotFoundException">Throws ArgumentNullException exception.</exception>
        /// <exception cref="KeyNotFoundException">Throws KeyNotFoundException exception.</exception>
        public Models.MatchDto Update(Models.MatchDto model)
        {
            if(model == null)
                throw new ArgumentNullException(nameof(model));

            List<Models.MatchOddDto> insertedMatchOddDtos;
            List<Models.MatchOddDto> updatedMatchOddDtos;
            List<Entities.MatchOdd> dbRemovedMatchOddItems;

            Entities.Match dbItem = GetQueryable(includeMatchOdds: true).FirstOrDefault(x => x.ID == model.ID);

            if (dbItem == null)
                throw new KeyNotFoundException(string.Format(cErrorNotFound, model.ID.ToString()));

            /* 1.Get inserted and removed shifts */
            insertedMatchOddDtos = model.MatchOddDtos.Where(x => x.ID == 0).ToList();
            dbRemovedMatchOddItems = dbItem.MatchOdds.Where(p => !model.MatchOddDtos.Any(p2 => p2.ID == p.ID)).ToList();
            dbRemovedMatchOddItems = dbItem.MatchOdds.Where(p => !model.MatchOddDtos.Any(p2 => p2.ID == p.ID)).ToList();
            updatedMatchOddDtos = model.MatchOddDtos.Where(p => dbItem.MatchOdds.Any(p2 => p2.ID == p.ID)).ToList();

            /* Delete match odds */
            if (dbRemovedMatchOddItems.Any())
                this.context.MatchOdds.RemoveRange(dbRemovedMatchOddItems);

            /* In case inserted match odds exist */
            if (insertedMatchOddDtos.Any())
            {
                List<Entities.MatchOdd> dbInsertedMatchOddItems = mapper.Map<List<Models.MatchOddDto>, List<Entities.MatchOdd>>(insertedMatchOddDtos);
                this.context.MatchOdds.AddRange(dbInsertedMatchOddItems);
            }

            /* Update match odds */
            if (updatedMatchOddDtos.Any())
            {
                foreach (var updatedMatchOddDto in updatedMatchOddDtos)
                {
                    this.dbMatchOddsRepo.Update(model: updatedMatchOddDto, applyChanges: false);
                }
            }
            /* Update match */
            mapper.Map(model, dbItem);
            base.Update(dbItem: dbItem);

            return this.GetById(model.ID, asNoTracking: true, includeMatchOdds: true);
        }

        /// <summary>
        /// Deletes the match by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="KeyNotFoundException">Throws KeyNotFoundException exception.</exception>
        public void Delete(int id)
        {
            /* Get database version of the same record */
            var dbItem = GetQueryable(includeMatchOdds: true).FirstOrDefault(x => x.ID == id);

            /* Verify db record exists (cannot update non existing record) */
            if (dbItem == null)
                throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture, cErrorNotFound, id));

            /* Delete related Match Odds */
            this.context.MatchOdds.RemoveRange(dbItem.MatchOdds);

            /* Delete Match */
            base.Delete(dbItem);            
        }
    }
}
