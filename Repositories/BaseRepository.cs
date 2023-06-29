using MatchInfo.API.DbContexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace MatchInfo.API.Repositories
{
    /// <summary>
    /// A class for BaseRepository
    /// </summary>
    /// <typeparam name="T">The T type</typeparam>
    public class BaseRepository<T> where T : class
    {
        //private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly MatchInfoContext _context;

        protected const string cErrorNotFound = "Record with id {0} has been deleted by another user or an incorrect id has been specified";

        /// <summary>
        /// The underlying dbSet
        /// </summary>
        private readonly DbSet<T> _dbSet;

        /// <summary>
        /// The automapper to use for db-dto conversions
        /// </summary>
        private readonly IMapper _mapper;


        /// <summary>
        /// EF Context
        /// </summary>
        protected MatchInfoContext context => _context;

        /// <summary>
        /// The underlying dbSet
        /// </summary>
        protected DbSet<T> dbSet => _dbSet;

        /// <summary>
        /// The automapper to use for db-dto conversions
        /// </summary>
        protected IMapper mapper => _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context">EF Context</param>
        /// <param name="mapper">The automapper to use for db-dto conversions</param>
        public BaseRepository(MatchInfoContext context, IMapper mapper)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            this._context = context;
            this._dbSet = context.Set<T>();
            this._mapper = mapper;
        }

        /// <summary>
        /// Insert a new Member Airport in a Route Group
        /// </summary>
        /// <param name="dbItem">The member airport</param>
        /// <param name="applyChanges">Apply save changes or not.</param>
        /// <returns>The number of affect records</returns>
        /// <exception cref="ArgumentNullException">dbItem was null</exception>
        /// <exception cref="DbUpdateException">Database threw an exception</exception>
        public virtual int Insert(T dbItem, bool applyChanges = true)
        {
            if (dbItem is null)
                throw new ArgumentNullException(nameof(dbItem));

           

            dbSet.Add(dbItem);
            int affected = 0;

            if (applyChanges)
            {
                affected = context.SaveChanges();
                if (affected > 0)
                {
                    var entry = _context.Entry(dbItem);
                                 }
            }
            return affected;
        }

        /// <summary>
        /// Update a member airport of a Route Group
        /// </summary>
        /// <param name="dbItem">The instance to delete</param>
        /// <param name="applyChanges">Apply save changes or not.</param>
        /// <returns>The number of affect records</returns>
        /// <exception cref="DbUpdateConcurrencyException">The record has been modified by another user</exception>
        /// <exception cref="DbUpdateException">Database threw an exception</exception>
        protected virtual int Update(T dbItem, bool applyChanges = true)
        {
            if (dbItem is null) return 0;

            var entry = _context.Entry(dbItem);
            int affected = 0;

            //proceed with update, only if any change has been detected on the actual data
            if (entry.State != EntityState.Unchanged)
            {
                if (applyChanges)
                    affected = _context.SaveChanges();
            }

            return affected;
        }


        /// <summary>
        /// Delete a member airport of a Route Group
        /// </summary>
        /// <param name="dbItem">The instance to delete</param>
        /// <param name="applyChanges">Apply save changes or not.</param>
        /// <returns>The number of affect records</returns>
        public virtual int Delete(T dbItem, bool applyChanges = true)
        {
            if (dbItem is null) throw new ArgumentNullException(nameof(dbItem));

            int affected = 0;

            _dbSet.Remove(dbItem);

            if (applyChanges)
                affected = _context.SaveChanges();

            return affected;
        }

    }
}
