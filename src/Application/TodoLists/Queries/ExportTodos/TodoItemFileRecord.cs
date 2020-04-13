using CleanTesting.Application.Common.Mappings;
using CleanTesting.Domain.Entities;

namespace CleanTesting.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
