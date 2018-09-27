using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using GenericRepository.Context;
using GenericRepository.Paging;
using GenericRepository.Repositories;
using GenericRepository.Uow;
using Xunit;
using GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository.Tests.Startup.ServiceCollectionExtensionsTests
{
    public class AddDataAccessOptionsTests
    {
        private const string connectionstring= "Server = (localdb)\\mssqllocaldb;Database=GenericRepository;Trusted_Connection=True;MultipleActiveResultSets=true";
        [Fact]
        private void UowProviderIsRegisteredAsSingleton()
        {
            var services = new ServiceCollection();

            services.AddDataAccess<TestContext>(options=> options.UseInMemoryDatabase(connectionstring));

            var registrations = services.Where(sd => sd.ServiceType == typeof(IUowProvider)
                                               && sd.ImplementationType == typeof(UowProvider))
                                        .ToArray();
            Assert.Single(registrations);
            Assert.Equal(ServiceLifetime.Singleton, registrations[0].Lifetime);
        }

        [Fact]
        private void GenericRepositoryIsRegisteredAsTransient()
        {
            var services = new ServiceCollection();

            services.AddDataAccess<TestContext>(options => options.UseInMemoryDatabase(connectionstring));

            var registrations = services.Where(sd => sd.ServiceType == typeof(IRepository<>)
                                               && sd.ImplementationType == typeof(GenericEntityRepository<>))
                                        .ToArray();
            Assert.Single(registrations);
            Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
        }

        //[Fact]
        //private void DataPagerIsRegisteredAsTransient()
        //{
        //    var services = new ServiceCollection();

        //    services.AddDataAccess<TestContext>(options => options.UseInMemoryDatabase(connectionstring));

        //    var registrations = services.Where(sd => sd.ServiceType == typeof(IDataPager<>)
        //                                       && sd.ImplementationType == typeof(DataPager<>))
        //                                .ToArray();
        //    Assert.Single(registrations);
        //    Assert.Equal(ServiceLifetime.Transient, registrations[0].Lifetime);
        //}
    }
}
