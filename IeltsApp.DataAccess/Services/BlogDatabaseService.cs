using IeltsApp.DataAccess.Interface;
using IeltsApp.DataAccess.Models;
using MongoDB.Driver;

namespace IeltsApp.DataAccess.Services
{
    public class BlogDatabaseService : IBlogDatabaseService
    {
        private readonly IMongoCollection<Blog> _blogsCollection;

        public BlogDatabaseService(IIeltsMasterDatabaseSettings blogDatabaseSettings,IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(blogDatabaseSettings.DatabaseName);
            _blogsCollection = database.GetCollection<Blog>(blogDatabaseSettings.CollectionName);
        }

        public async Task<List<Blog>> GetAsync() =>
            await _blogsCollection.Find(_ => true).ToListAsync();
        public async Task<Blog?> GetByTitleAsync(string title) =>
            await _blogsCollection.Find(x => x.Title == title).FirstOrDefaultAsync();

        public async Task CreateAsync(Blog newBook) =>
            await _blogsCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, Blog updatedBook) =>
            await _blogsCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _blogsCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<Blog?> GetByIdAsync(string id)
        {
            return await _blogsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
