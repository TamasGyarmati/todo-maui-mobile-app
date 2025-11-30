using ToDo.Models;

namespace ToDo.Database;

public interface IDatabase<in T> where T : class
{
    Task<List<Todo>> GetAllTodoAsync();
    Task CreateTodoAsync(T obj);
    Task UpdateTodoAsync(T obj);
    Task DeleteTodoAsync(T obj);
}