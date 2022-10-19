using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TeamHunterBackend.DB;
using TeamHunterBackend.Schemas;

namespace TeamHunterBackend.Services
{
    public class UserPhotoService
    {
        private readonly IMongoCollection<UserPhoto> _userPhotos;

        public UserPhotoService(IOptions<DBSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _userPhotos = mongoClient.GetDatabase(options.Value.DatabaseName).GetCollection<UserPhoto>(options.Value.CollectionName[1]);
        }

        public async Task AddUserPhoto(UserPhoto newUserPhoto) =>
            await _userPhotos.InsertOneAsync(newUserPhoto);

        public async Task<UserPhoto> GetUserPhotoById(int Id) =>
            await _userPhotos.Find(m => m.PhotoId == Id).FirstOrDefaultAsync();

        public async Task UpdateUserPhotoById(int Id, UserPhoto updateUserPhoto) => 
            await _userPhotos.ReplaceOneAsync(m => m.PhotoId == Id, updateUserPhoto);

        public async Task DeleteUserPhotoById(int Id) =>
            await _userPhotos.DeleteOneAsync(m => m.PhotoId == Id);
    }
}