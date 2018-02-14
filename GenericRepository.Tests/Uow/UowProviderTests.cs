using Castle.Core.Logging;
using GenericRepository;
using GenericRepository.Context;
using GenericRepository.Repositories;
using GenericRepository.Tests._TestObjects;
using GenericRepository.Uow;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GenericRepository.Tests.Uow
{
    public class UowProviderTests
    {
        [Fact]
        public void TestGetCustomRepository()
        {

            var logger = new Moq.Mock<ILogger<DataAccess>>();
            var sp = new Moq.Mock<IServiceProvider>();

            var myContext = new InMemoryContext(new Microsoft.EntityFrameworkCore.DbContextOptions<InMemoryContext>());

            sp.Setup((o) => o.GetService(typeof(IEntityContext))).Returns(myContext);
            sp.Setup((o) => o.GetService(typeof(IFooRepository))).Returns(new FooRepository(logger.Object, myContext));

            var provider = new UowProvider(logger.Object, sp.Object);

            var uow = provider.CreateUnitOfWork();

            uow.GetCustomRepository<IFooRepository>();
        }

        [Fact]
        public void TestGetGenericRepository()
        {

            var logger = new Moq.Mock<ILogger<DataAccess>>();
            var sp = new Moq.Mock<IServiceProvider>();

            var myContext = new InMemoryContext(new Microsoft.EntityFrameworkCore.DbContextOptions<InMemoryContext>());

            sp.Setup((o) => o.GetService(typeof(IEntityContext))).Returns(myContext);
            sp.Setup((o) => o.GetService(typeof(IRepository<Foo,int>))).Returns(new GenericEntityRepository<Foo,int>(logger.Object));

            var provider = new UowProvider(logger.Object, sp.Object);

            var uow = provider.CreateUnitOfWork();

            uow.GetRepository<Foo,int>();
        }

    }
}
