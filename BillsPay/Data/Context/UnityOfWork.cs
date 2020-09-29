using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

namespace Data.Context
{
    public partial class UnityOfWork : DbContext
    {
        public UnityOfWork() : base()
        {

        }

        public UnityOfWork(DbContextOptions<UnityOfWork> options) : base(options)
        {

        }

        public IDbContextTransaction CreateTrasaction()
        {
            return Database.BeginTransaction();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly assemblyWithConfigurations = GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assemblyWithConfigurations);
        }

        public void SetModifiedState<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public TEntity Remove<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().Remove(entity);
            return entity;
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Unchanged;
        }

        public override DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
