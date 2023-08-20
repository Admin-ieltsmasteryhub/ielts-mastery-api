using IeltsApp.DataAccess.Models;

namespace IeltsApp.DataAccess.Interface
{
    public interface IBlogDatabaseService
    {
        Task CreateAsync(Blog newBook);
        Task<List<Blog>> GetAsync();
        Task<Blog?> GetByTitleAsync(string title);
        Task<Blog?> GetByIdAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Blog updatedBook);
    }
}