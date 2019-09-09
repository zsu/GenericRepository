using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace GenericRepository.Extensions
{
    public static class DbContextExtensions
    {

        public static IEnumerable<string> FindPrimaryKeyNames<T>(this DbContext dbContext)
        {
            return dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Select(x => x.Name);
            //return from p in dbContext.FindPrimaryKeyProperties(entity)
            //       select p.Name;
        }
        public static IEnumerable<PropertyInfo> FindPrimaryKeys<T>(this DbContext dbContext)
        {
            var propertyNames = dbContext.FindPrimaryKeyNames<T>();
            List<PropertyInfo> properties=null;
            if (propertyNames != null && propertyNames.Count() > 0)
            {
                properties=new List<PropertyInfo>();
                foreach (var name in propertyNames)
                {
                    properties.Add(typeof(T).GetProperty(name));
                }
            }
            return properties;
        }

        public static IEnumerable<object> FindPrimaryKeyValues<T>(this DbContext dbContext, T entity)
        {
            return from p in dbContext.FindPrimaryKeyNames<T>()
                   select entity.GetPropertyValue(p);
        }

        static IReadOnlyList<IProperty> FindPrimaryKeyProperties<T>(this DbContext dbContext)
        {
            return dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties;
        }

        static object GetPropertyValue<T>(this T entity, string name)
        {
            return entity.GetType().GetProperty(name).GetValue(entity, null);
        }
    }
}
