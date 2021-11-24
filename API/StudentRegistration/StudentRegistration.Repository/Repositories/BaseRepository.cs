using StudentRegistration.Domain;
using System;
using System.Collections.Generic;
using System.Linq;


namespace StudentRegistration.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ConnectionEf _Db;
        public BaseRepository()
        {
        }

        public IEnumerable<T> GetAll()
        {
            return _Db.Set<T>().AsEnumerable();
        }

        public T GetById(int id)
        {
            return _Db.Set<T>().Find(id);
        }

        public IEnumerable<T> GetByParam(Func<T, bool> predicate)
        {
            return _Db.Set<T>().Where(predicate);
        }

        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _Db.Set<T>().Add(entity);
            _Db.SaveChanges();

            return entity;
        }

        public IEnumerable<T> InsertRange(IEnumerable<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _Db.Set<T>().AddRange(entity);
            _Db.SaveChanges();

            return entity;
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _Db.Set<T>().Update(entity);
            _Db.SaveChanges();

            return entity;
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _Db.SaveChanges();

            return entity;
        }

        public bool Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                _Db.Set<T>().Remove(entity);
                _Db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool DeleteRange(IEnumerable<T> entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                _Db.Set<T>().RemoveRange(entity);
                _Db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
