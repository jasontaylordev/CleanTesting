﻿using CleanTesting.Application.Common.Exceptions;
using CleanTesting.Application.TodoItems.Commands.CreateTodoItem;
using CleanTesting.Application.TodoLists.Commands.CreateTodoList;
using CleanTesting.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CleanTesting.Application.IntegrationTests.TodoItems.Commands
{
    public class CreateTodoItemTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateTodoItemCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateTodoItem()
        {
            var userId = await RunAsDefaultUserAsync();

            var listId = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            var command = new CreateTodoItemCommand
            {
                ListId = listId,
                Title = "Tasks"
            };

            var itemId = await SendAsync(command);

            var item = await FindAsync<TodoItem>(itemId);

            item.Should().NotBeNull();
            item.ListId.Should().Be(command.ListId);
            item.Title.Should().Be(command.Title);
            item.CreatedBy.Should().Be(userId);
            item.Created.Should().BeCloseTo(DateTime.Now, 10000);
            item.LastModifiedBy.Should().BeNull();
            item.LastModified.Should().BeNull();
        }
    }
}
