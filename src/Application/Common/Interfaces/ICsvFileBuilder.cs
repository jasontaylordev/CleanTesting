using CleanTesting.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace CleanTesting.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
