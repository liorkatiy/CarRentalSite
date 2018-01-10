using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity.SqlServer;

namespace Entity
{
    //base class to control the database
    public abstract class DataBase<Entity, Item> : IDisposable
        where Entity : DbContext, new()
        where Item : class
    {
        protected Entity db;

        public DataBase()
        {
            db = new Entity();
        }

        protected Item GetBy(Func<Item, bool> first)
        {
            return db.Set<Item>().FirstOrDefault(first);
        }

        /// <summary>
        /// add item to database return true if works
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual bool Add(Item item)
        {
            try
            {
                db.Set<Item>().Add(item);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }        
        }

        //update item in database with the override update method
        public bool Update(Func<Item, bool> where, Item updatedItem)
        {
            return Update(where, item => Update(item, updatedItem));
        }

        //update item in database ask action what to update
        public bool Update(Func<Item, bool> where, Func<Item,bool> action)
        {
            Item item = db.Set<Item>().FirstOrDefault(where);
            if (item != null)
            {
                if (action(item))
                {
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// delete item from database return false if item wasnt found
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual bool Delete(Func<Item, bool> where)
        {
            Item item = db.Set<Item>().FirstOrDefault(where);
            if (item != null)
            {
                Delete(item);
                db.Set<Item>().Remove(item);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// get all from database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="select"></param>
        /// <returns></returns>
        public T[] GetAll<T>(Func<Item, T> select)
        {
            return db.Set<Item>().Select(select).ToArray();
        }

        /// <summary>
        /// get all the items that match the search string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="search"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public T[] GetFromSearch<T>(string search, Func<Item, T> select)
        {
            //split the search into words
            string[] searchWords = search.Split(' ');
            //if empty than just get all
            bool getAll = searchWords.Length == 1 && searchWords[0] == ""; 
            if (getAll)
            {
                return db.Set<Item>().Select(select).ToArray();
            }
            else
            {
                return db.Set<Item>()
                    .Where(MySearch(searchWords))
                    .Select(select).ToArray(); ;
            }
        }

        //method for overiding the search result parameters
        //should be used with lambda to check if any item contains 
        //any part of the search array
        protected abstract Expression<Func<Item, bool>> MySearch(string[] search);

        //what parts to update
        protected abstract bool Update(Item item, Item updatedItem);

        //actions to take before deleting
        protected abstract void Delete(Item item);

        //IDisposable
        public void Dispose()
        {
            db.Dispose();
        }
    }
}