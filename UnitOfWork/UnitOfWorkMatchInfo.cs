using AutoMapper;
using Microsoft.Extensions.Logging;
using System;

namespace MatchInfo.API.UnitOfWork
{
    /// <summary>
    /// A unitofwork class for match info database.
    /// </summary>
    public class UnitOfWorkMatchInfo : IDisposable
    {
        /// <summary>
        /// The dbContext for MatchInfo database.
        /// </summary>
        private readonly DbContexts.MatchInfoContext _matchInfoContext;


        /// <summary>
        /// The mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// The matches repository.
        /// </summary>
        private Repositories.MatchesRepository _matchesRepository;

        /// <summary>
        /// The matchOdds repository.
        /// </summary>
        private Repositories.MatchOddsRepository _matchOddsRepository;

        /// <summary>
        /// Gets or sets the matches repository.
        /// </summary>
        public Repositories.MatchesRepository MatchesRepository
        {
            get
            {
                if (_matchesRepository == null)  _matchesRepository = new Repositories.MatchesRepository(_matchInfoContext, _mapper, MatchOddsRepository);
                return _matchesRepository;
            }
        }


        /// <summary>
        /// Gets or sets the matchOdds repository.
        /// </summary>
        public Repositories.MatchOddsRepository MatchOddsRepository
        {
            get
            {
                if (_matchOddsRepository == null) _matchOddsRepository = new Repositories.MatchOddsRepository(_matchInfoContext, _mapper);
                return _matchOddsRepository;
            }
        }

        /// <summary>
        /// EF Context.
        /// </summary>
        public DbContexts.MatchInfoContext Context { get { return _matchInfoContext; } }

        /// <summary>
        /// Auto Mapper.
        /// </summary>
        public IMapper Mapper { get { return _mapper; } }

        public ILogger Logger { get { return _logger; } }

        /// <summary>
        /// Ctor for UnitOfWorkMatchInfo.
        /// </summary>
        /// <param name="_context">the context.</param>
        /// <param name="mapper">The automapper to use for db-dto conversions.</param>
        public UnitOfWorkMatchInfo(DbContexts.MatchInfoContext _context, IMapper mapper, ILogger logger)
        {
            _matchInfoContext = _context;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Saves all changes of a context.
        /// </summary>
        public void SaveChanges()
        {
            _matchInfoContext.SaveChanges();
        }

        /// <summary>
        /// Indicates if matchInfoContext is disposed.
        /// </summary>
        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Disposes the matchInfoContext.
        /// </summary>
        /// <param name="disposing">The disposing value.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _matchInfoContext.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Disposes the matchInfoContext.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

    }
}
