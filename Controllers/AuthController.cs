using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RutasApi.Data;
using RutasApi.Models.Request;
using RutasApi.Helpers;
using System.Linq;
using RutasApi.Models.Entities.Sicem;

namespace RutasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ILogger<AuthController> logger;
        private readonly SicemContext sicemContext;
        private readonly UbitomaSettings ubitomaSettings;

        public AuthController(ILogger<AuthController> logger, SicemContext sicemContext, IOptions<UbitomaSettings> options){
            this.logger = logger;
            this.sicemContext = sicemContext;
            this.ubitomaSettings = options.Value;
        }


        /// <summary>
        /// </summary>
        /// <returns code="200">Authenticated</returns>
        /// <returns code="400">Bad request</returns>
        /// <returns code="401">Bad credentials</returns>
        [HttpPost]
        [Route("")]
        public IActionResult Login(AuthRequest authRequest){
            
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            // * get the user
            var _hashedPassword = HashData.GetHash(authRequest.Password!, ubitomaSettings.Key);
            var operador = sicemContext.Operadores.FirstOrDefault( item => item.Password == _hashedPassword && item.Usuario == authRequest.User);
            if( operador == null){
                return Unauthorized();
            }

            // * make a session
            var operadoreSession = new SesionOperadore {
                OperadorId = operador.Id,
                CreateAt = DateTime.Now,
                ValidTo = DateTime.Now.AddMonths(3)
            };
            sicemContext.SesionOperadores.Add(operadoreSession);
            sicemContext.SaveChanges();

            // * return the data
            return Ok( new {
                id = operador.Id,
                nombre = operador.Nombre,
                usuario = operador.Usuario,
                rutaID = operador.RutaId,
                sessionID = operadoreSession.Id,
            });
        }

    }
}
