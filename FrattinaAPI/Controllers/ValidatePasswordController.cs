using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using FrattinaAPI.Model;
using FrattinaAPI.Repository;

namespace FrattinaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValidatePasswordController : ControllerBase
    {
        private IValidatePasswordRepository  _repository;
        private readonly Messages.Messages _messages;

        public ValidatePasswordController(IValidatePasswordRepository repository, Messages.Messages messages)
        {
            _repository = repository ?? throw new
                ArgumentNullException(nameof(repository));
            _messages = messages ?? throw new ArgumentNullException("Ocorreu um erro interno!");
        }


        /// <summary>
        /// Validar senhas
        /// </summary>
        [HttpPost]
        public IActionResult ValidatePassword([FromBody] PasswordRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(Messages.Messages.PasswordInvalid);
            }

            Log.Information($"{nameof(ValidatePassword)} - {_messages.StartingValidation(request)}");
            var IsValid = _repository.ValidatePassword(request);

            if (IsValid.IsValid)
            {
                return Ok(IsValid);
            }
            else
            {
                Log.Information($"{nameof(ValidatePassword)} - {_messages.ErrorLog(IsValid)}");
                return BadRequest(IsValid);
            }
        }
    }
}
