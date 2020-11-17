using DAO.Context;
using DAO.Interface;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAO.Repository
{
    public class BaseRepository<T> : ICrud<T> where T : class
    {
        TpDBContext context;
        DbSet<T> dbSet;
        public BaseRepository(TpDBContext contexto)
        {
            context = contexto;
            dbSet = context.Set<T>();
        }

        public T Guardar(T generics)
        {
           T generico =  dbSet.Add(generics);
            context.SaveChanges();
            return generico;
        }

        public T ObtenerPorID(int id)
        {
            return dbSet.Find(id);
        }


        public T Actualizar(T generics)
        {
            if (context.Entry(generics).State == EntityState.Modified)
            {
                context.SaveChanges();
            }

            return generics;
        }

        public List<T> ObtenerTodos()
        {
            return dbSet.ToList();
        }
    }
}
