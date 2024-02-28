using FrattinaAPI.Model;

namespace FrattinaAPI.Repository
{
    public interface IValidatePasswordRepository
    {
        public PasswordResponse ValidatePassword(PasswordRequest request);
    }
}
