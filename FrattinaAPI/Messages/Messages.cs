using FrattinaAPI.Model;

namespace FrattinaAPI.Messages
{
    public class Messages
    {
        public static string PasswordInvalid = "A solicitação deve conter uma senha";

        #region LOGS


        public string StartingValidation(PasswordRequest passwordRequest)
        {
            return $"Iniciando a validação para a senha {passwordRequest.Password}";
        }
       
        public string ErrorLog(PasswordResponse response)
        {
            var errorMessage = "Erro 404 pois a validação a seguir falhou: ";

            foreach (var erro in response.Errors)
            {
                errorMessage += $"\n {erro} \n";
            }

            return errorMessage ;
        }

        #endregion
    }
}
