using CleanTesting.Application.TodoLists.Queries.GetTodos;
using CleanTesting.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace CleanTesting.Application.IntegrationTests.TodoLists.Queries
{
    using static Testing;

    public class GetTodosTests : TestBase
    {
        [Test]
        public async Task ShouldReturnPriorityLevels()
        {
            var query = new GetTodosQuery();

            var result = await SendAsync(query);

            result.PriorityLevels.Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldReturnAllTodoListsWithAssociatedTodoItems()
        {
            // Arrange
            await AddAsync(new TodoList
            {
                Title = "Shopping",
                Items =
                {
                    new TodoItem { Title = "Fresh fruit", Done = true },
                    new TodoItem { Title = "Bread", Done = true },
                    new TodoItem { Title = "Milk", Done = true },
                    new TodoItem { Title = "Toilet paper" },
                    new TodoItem { Title = "Tuna" },
                    new TodoItem { Title = "Pasta" }
                }
            });

            var query = new GetTodosQuery();
            
            // Act 
            var result = await SendAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Lists.Should().HaveCount(1);
            result.Lists.First().Items.Should().HaveCount(6);
        }
    }
}
