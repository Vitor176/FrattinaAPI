using System.Collections.Generic;

namespace FrattinaAPI.Model
{
    public class PasswordResponse
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public void AddError(string erro)
        {
            Errors.Add(erro);
        }
    }
}
