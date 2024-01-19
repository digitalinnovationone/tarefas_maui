using SQLite;

namespace Tarefas.Servicos;

public class DatabaseServico<T> where T : new()
{
    private SQLiteAsyncConnection _database;

    public DatabaseServico(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<T>().Wait();
    }

    public Task<int> IncluirAsync(T item)
    {
        return _database.InsertAsync(item);
    }

    public Task<int> DeleteAsync(T item)
    {
        return _database.DeleteAsync(item);
    }

    public Task<int> AlterarAsync(T item)
    {
        return _database.UpdateAsync(item);
    }

    public Task<List<T>> TodosAsync()
    {
        return _database.Table<T>().ToListAsync();
    }

    public AsyncTableQuery<T> Query()
    {
        return _database.Table<T>();
    }

    public Task<int> QuantidadeAsync()
    {
        return _database.Table<T>().CountAsync();
    }
}