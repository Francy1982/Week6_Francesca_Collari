using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaAgenti
{
    //interfacci generica
    internal interface IRepository<T>
    {
        List<T> GetAll();
        bool Aggiungi(T item);
        List<T> GetByArea(string area);

    }
}
