using System.Linq;
using FrattinaAPI.Model;

namespace FrattinaAPI.Repository
{
    public class ValidatePasswordRepository : IValidatePasswordRepository
    {
        public PasswordResponse ValidatePassword(PasswordRequest request)
        {
            PasswordResponse response = new PasswordResponse();

            // Condição 1: Nove ou mais caracteres
            if (request.Password.Length < 9)
            {
                response.AddError("A senha deve ter nove ou mais caracteres.");
            }

            // Condição 2: Ao menos 1 dígito
            if (!request.Password.Any(char.IsDigit))
            {
                response.AddError("A senha deve conter pelo menos um dígito.");
            }

            // Condição 3: Ao menos 1 letra minúscula
            if (!request.Password.Any(char.IsLower))
            {
                response.AddError("A senha deve conter pelo menos uma letra minúscula.");
            }

            // Condição 4: Ao menos 1 letra maiúscula
            if (!request.Password.Any(char.IsUpper))
            {
                response.AddError("A senha deve conter pelo menos uma letra maiúscula.");
            }

            // Condição 5: Ao menos 1 caractere especial
            if (!request.Password.Any(c => "!@#$%^&*()-+".Contains(c)))
            {
                response.AddError("A senha deve conter pelo menos um caractere especial.");
            }

            // Condição 6: Não possuir caracteres repetidos
            if (request.Password.Distinct().Count() != request.Password.Length)
            {
                response.AddError("A senha não pode conter caracteres repetidos.");
            }

            // Condição 7: Espaços em branco não são caracteres válidos
            if (request.Password.Any(char.IsWhiteSpace))
            {
                response.AddError("A senha não pode conter espaços em branco.");
            }

            // Se não houver erros, a senha é considerada válida
            response.IsValid = !response.Errors.Any();

            return response;
        }
    }
}
