using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TeamHunter.DB;
using TeamHunter.Schemas;

namespace TeamHunter.Services
{
    public class UserPhotoService
    {
        private readonly IMongoCollection<Photo> _userPhotos;

        public UserPhotoService(IOptions<DBSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _userPhotos = mongoClient.GetDatabase(options.Value.DatabaseName).GetCollection<Photo>(options.Value.CollectionName[1]);
        }

        public async Task AddUserPhoto(Photo newUserPhoto) =>
            await _userPhotos.InsertOneAsync(newUserPhoto);

        public async Task<Photo> GetUserPhotoById(int Id) =>
            await _userPhotos.Find(m => m.Id == Id).FirstOrDefaultAsync();

        public async Task UpdateUserPhotoById(int Id, Photo updateUserPhoto) => 
            await _userPhotos.ReplaceOneAsync(m => m.Id == Id, updateUserPhoto);

        public async Task DeleteUserPhotoById(int Id) =>
            await _userPhotos.DeleteOneAsync(m => m.Id == Id);
    }
}