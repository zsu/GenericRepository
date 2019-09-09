using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GenericRepository.Extensions;

namespace GenericRepository.Mvc
{
    public class GenericEntityRepository<TEntity> : EntityRepositoryBase<DbContext, TEntity> where TEntity : class, new()
    {
        public GenericEntityRepository() : base(null)
        { }
        public GenericEntityRepository(ILogger<DataAccess> logger) : base(logger, null)
        { }
        protected override IEnumerable<PropertyInfo> GetKeyProperties()
        {
            var type = typeof(TEntity);

            List<PropertyInfo> propertMetaData = new List<PropertyInfo>();

            var types = GetModelMetadataTypes(type);
            foreach (var t in types)
            {
                foreach (var p in t.GetProperties())

                    if (p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0)
                        propertMetaData.Add(p);
            }
            if (propertMetaData.Count() > 0)
                return propertMetaData;

            propertMetaData = Context.FindPrimaryKeys<TEntity>().ToList();
            return propertMetaData;
        }
        private Type GetModelMetadataType(Attribute attribute)
        {
            var type = attribute.GetType();
            if (type.FullName == "Microsoft.AspNetCore.Mvc.ModelMetadataTypeAttribute")
            {
                var property = type.GetProperty("MetadataType");
                if (property != null && property.CanRead)
                {
                    return property.GetValue(attribute, null) as Type;
                }
            }
            return null;
        }

        private Type[] GetModelMetadataTypes(Type type)
        {
            var query = from t in type.BaseTypesAndSelf()
                        from a in t.GetCustomAttributes(true).Cast<System.Attribute>()
                        let metaType = GetModelMetadataType(a)
                        where metaType != null
                        select metaType;
            return query.ToArray();
        }
    }
    public static partial class TypeExtensions
    {
        public static IEnumerable<Type> BaseTypesAndSelf(this Type type)
        {
            while (type != null)
            {
                yield return type;
                type = type.BaseType;
            }
        }
    }
}