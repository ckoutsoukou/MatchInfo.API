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
using System.Threading.Tasks;

namespace MatchInfo.API.Repositories
{
    /// <summary>
    /// A class for MatchOddsRepository
    /// </summary>.
    public class MatchOddsRepository : BaseRepository<Entities.MatchOdd>
    {
        /// <summary>
        /// Ctor for MatchOddsRepository.
        /// </summary>
        /// <param name="context">EF Context.</param>
        /// <param name="mapper">The automapper to use for db-dto conversions.</param>
        public MatchOddsRepository(MatchInfoContext context, IMapper mapper) : base(context: context, mapper: mapper)
        {
        }

        /// <summary>
        /// Gets dbSet of MatchOdd.
        /// </summary>
        /// <returns></returns>
        private IQueryable<Entities.MatchOdd> QueryableBase()
        {
            return context.MatchOdds;
        }

        /// <summary>
        /// Update MatchOdd.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="applyChanges">Indicates if apply changes to db.</param>
        /// <returns>The updated matchOdd item.</returns>
        /// <exception cref="ArgumentNullException">Throws KeyNotFoundException.</exception>
        /// <exception cref="KeyNotFoundException">Throws KeyNotFoundException.</exception>
        public Models.MatchOddDto Update(Models.MatchOddDto model, bool applyChanges = true)
        {
            if(model == null)
                throw new ArgumentNullException(nameof(model));

            var dbItem = QueryableBase().FirstOrDefault(x => x.ID == model.ID);

            //verify db record exists (cannot update non existing record)
            if (dbItem == null)
                throw new KeyNotFoundException(string.Format(cErrorNotFound, model.ID.ToString()));
          
            //write over the dbItem values from the dto
            mapper.Map(model, dbItem);

            base.Update(dbItem: dbItem, applyChanges: applyChanges);

            return mapper.Map<Entities.MatchOdd, Models.MatchOddDto>(dbItem);
        }
    }
}
