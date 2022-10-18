using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddDataAccess<TEntityContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction) where TEntityContext : DbContext,IEntityContext//EntityContextBase<TEntityContext>
        {
            RegisterDataAccess<TEntityContext>(services, optionsAction);
            return services;
        }

        private static void RegisterDataAccess<TEntityContext>(IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction) where TEntityContext : DbContext,IEntityContext//EntityContextBase<TEntityContext>
        {
            services.TryAddSingleton<IUowProvider, UowProvider>();
            DbContextOptionsBuilder<TEntityContext> optionBuilder = new DbContextOptionsBuilder<TEntityContext>();
            if (optionsAction != null)
            {
                optionsAction.Invoke(optionBuilder);
            }
            services.AddTransient<DbContextOptions<TEntityContext>>(p => optionBuilder.Options);
            services.TryAddTransient<IEntityContext, TEntityContext>();
            services.TryAddTransient(typeof(IRepository<>), typeof(GenericEntityRepository<>));
            services.AddTransient<IGenericService<TEntityContext>, GenericService<TEntityContext>>();
            services.AddTransient<TEntityContext>();
        }

        private static void ValidateMandatoryField(string field, string fieldName)
        {
            if (field == null) throw new ArgumentNullException(fieldName, $"{fieldName} cannot be null.");
            if (field.Trim() == String.Empty) throw new ArgumentException($"{fieldName} cannot be empty.", fieldName);
        }

        private static void ValidateMandatoryField(object field, string fieldName)
        {
            if (field == null) throw new ArgumentNullException(fieldName, $"{fieldName} cannot be null.");
        }
    }
}
