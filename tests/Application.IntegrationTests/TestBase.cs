using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CleanTesting.Application.IntegrationTests
{
    using static Testing;

    [TestFixture]
    public class TestBase
    {
        protected IServiceScope Scope { get; private set; }

        [SetUp]
        public async Task SetUp()
        {
            await ResetState();

            this.Scope = CreateScope();
        }

        [TearDown]
        public void BaseTearDown() 
        {
            if (Scope != null)
            {
                Scope.Dispose();
                Scope = null;
            }
        }

        protected virtual async Task<TEntity> FindAsync<TEntity>(int id)
            where TEntity : class
        {
            return await Testing.FindAsync<TEntity>(id, Scope);
        }

        protected virtual async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            await Testing.AddAsync<TEntity>(entity, Scope);
        }

        protected virtual async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            return await Testing.SendAsync<TResponse>(request, Scope);
        }

        protected virtual async Task<string> RunAsDefaultUserAsync()
        {
            return await Testing.RunAsDefaultUserAsync(Scope);
        }
    }
}
