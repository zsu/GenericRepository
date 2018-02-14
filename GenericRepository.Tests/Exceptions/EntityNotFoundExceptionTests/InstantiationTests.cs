using System;
using GenericRepository.Exceptions;
using Xunit;

namespace GenericRepository.Tests.Exceptions.EntityNotFoundExceptionTests
{
    public class InstantiationTests
    {
        [Fact]
        private void EntityNameIsSet()
        {
            var ex = new EntityNotFoundException("entity", 123);
            Assert.Equal("entity", ex.EntityName);
        }

        [Fact]
        private void EntityKeyIsSet()
        {
            var ex = new EntityNotFoundException("entity", 123);
            Assert.Equal(123, ex.EntityKey);
        }
    }
}
