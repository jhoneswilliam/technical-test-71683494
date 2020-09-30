using Data.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;

namespace Data.Repositories
{
    public class Repository
    {
        public UnityOfWork UnityOfWork { get; }

        public Repository()
        {

        }

        public Repository(UnityOfWork unityOfWork)
        {
            UnityOfWork = unityOfWork;
        }

        public virtual void CreateTrasaction(Action<IDbContextTransaction, UnityOfWork> func)
        {
            using (var transaction = UnityOfWork.CreateTrasaction())
            {
                try
                {
                    func(transaction, UnityOfWork);
                }
                catch (System.Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public virtual TEntidade Get<TEntidade>(Guid codigo)
            where TEntidade : Entity, new()
        {
            return UnityOfWork.Set<TEntidade>().Where(e => e.Id.Equals(codigo)).FirstOrDefault();
        }

        public virtual DbSet<TEntidade> GetAll<TEntidade>()
            where TEntidade : class
        {
            return UnityOfWork.Set<TEntidade>();
        }

        public virtual TEntidade Add<TEntidade>(TEntidade entidade)
            where TEntidade : class, new()
        {
            if (entidade is Entity && !(entidade as Entity).Id.Equals(default(Guid)))
            {
                UnityOfWork.SetModifiedState<TEntidade>(entidade);
            }
            else
            {
                UnityOfWork.Set<TEntidade>().Add(entidade);
            }

            UnityOfWork.SaveChanges();
            return entidade;
        }

        public virtual TEntidade Remove<TEntidade>(TEntidade entidade) where TEntidade : class
        {
            UnityOfWork.Remove<TEntidade>(entidade);
            UnityOfWork.SaveChanges();
            return entidade;
        }
    }
}
