using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASPCoreSample.Models;

namespace PrototypeWebBlockchain.Repository
{
    public interface ITransactionRepository<T> where T : BaseEntity
    {
        void Add(T item);
        IEnumerable<T> FindByID(int id);
        IEnumerable<T> FindAll();
    }
}