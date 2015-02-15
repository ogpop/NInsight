using System;
using System.Data.Entity;

using NInsight.Core.Domain;

namespace NInsight.Persistence.EF
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///     Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        int Commit();
    }

    public sealed class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        ///     The DbContext
        /// </summary>
        private DbContext _dbContext;

        /// <summary>
        ///     Initializes a new instance of the UnitOfWork class.
        /// </summary>
        /// <param name="context">The object context</param>
        public UnitOfWork(DbContext context)
        {
            this._dbContext = context;
        }

        /// <summary>
        ///     Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        public int Commit()
        {
            // Save changes with the default options
            return this._dbContext.SaveChanges();
        }

        /// <summary>
        ///     Disposes the current object
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._dbContext != null)
                {
                    this._dbContext.Dispose();
                    this._dbContext = null;
                }
            }
        }
    }

    public class UnitOfWork2 : IDisposable
    {
        private readonly NInsightContext context = new NInsightContext();

        private GenericRepository<Application> applicationRepository;

        private bool disposed;

        private GenericRepository<Point> pointRepository;

        private GenericRepository<Run> runRepository;

        public GenericRepository<Point> PointRepository
        {
            get
            {
                if (this.pointRepository == null)
                {
                    this.pointRepository = new GenericRepository<Point>(this.context);
                }
                return this.pointRepository;
            }
        }

        public GenericRepository<Run> RunRepository
        {
            get
            {
                if (this.runRepository == null)
                {
                    this.runRepository = new GenericRepository<Run>(this.context);
                }
                return this.runRepository;
            }
        }

        public GenericRepository<Application> ApplicationRepository
        {
            get
            {
                if (this.applicationRepository == null)
                {
                    this.applicationRepository = new GenericRepository<Application>(this.context);
                }
                return this.applicationRepository;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}