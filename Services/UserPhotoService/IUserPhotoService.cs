using TeamHunterBackend.Schemas;

namespace TeamHunterBackend.Services
{
    public interface IUserPhotoService
    {
        public  Task AddUserPhoto(UserPhoto newUserPhoto);
        public  Task<UserPhoto> GetUserPhotoById(int Id);
        public  Task UpdateUserPhotoById(int Id, UserPhoto updateUserPhoto);
        public  Task DeleteUserPhotoById(int Id);
    }
}