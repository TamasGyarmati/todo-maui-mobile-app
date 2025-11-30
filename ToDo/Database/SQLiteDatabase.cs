using SQLite;
using ToDo.Models;

namespace ToDo.Database;

public class SQLiteDatabase : IDatabase<Todo>
{
    SQLiteOpenFlags flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create;

    SQLiteAsyncConnection db;

    string fullPath = Path.Combine(FileSystem.Current.AppDataDirectory, "todo.db3");

    public SQLiteDatabase()
    {
        db = new SQLiteAsyncConnection(fullPath, flags);
        Console.WriteLine("Path: " + fullPath);
        db.CreateTableAsync<Todo>().Wait();
    }

    public async Task<List<Todo>> GetAllTodoAsync() => await db.Table<Todo>().ToListAsync();
    public async Task CreateTodoAsync(Todo todo) => await db.InsertAsync(todo);
    public async Task UpdateTodoAsync(Todo todo) => await db.UpdateAsync(todo);
    public async Task DeleteTodoAsync(Todo todo) => await db.DeleteAsync(todo);
}