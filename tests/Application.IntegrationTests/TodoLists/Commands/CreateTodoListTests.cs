using CleanTesting.Application.Common.Exceptions;
using CleanTesting.Application.TodoLists.Commands.CreateTodoList;
using CleanTesting.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CleanTesting.Application.IntegrationTests.TodoLists.Commands
{
    using static Testing;

    public class CreateTodoListTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateTodoListCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueTitle()
        {
            await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            var command = new CreateTodoListCommand
            {
                Title = "New List"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateTodoList()
        {
            var userId = await RunAsDefaultUserAsync();

            var command = new CreateTodoListCommand
            {
                Title = "New List"
            };

            var listId = await SendAsync(command);

            var list = await FindAsync<TodoList>(listId);

            list.Should().NotBeNull();
            list.Title.Should().Be(command.Title);
            list.Created.Should().BeCloseTo(DateTime.Now, 10000);
            list.CreatedBy.Should().Be(userId);
        }
    }
}
