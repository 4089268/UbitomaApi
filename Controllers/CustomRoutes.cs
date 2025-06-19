using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using RutasApi.Data;
using RutasApi.Models;
using RutasApi.Models.Responses;
using RutasApi.Models.Request;
using RutasApi.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace RutasApi.Controllers{

    [ApiController]
    [Route("[controller]")]
    public class CustomRoutes : ControllerBase {

        private readonly ILogger<CustomRoutes> logger;
        private readonly SicemService sicemService;
        private readonly SicemContext sicemContext;
        private readonly ArquosService arquosService;
        
        public CustomRoutes(ILogger<CustomRoutes> logger, SicemService s, ArquosService a, SicemContext context, ArquosService arquosService){
            this.logger = logger;
            this.sicemService = s;
            this.sicemContext = context;
            this.arquosService = arquosService;
        }

        /// <summary>
        /// get the las custom routes
        /// </summary>
        /// <param name="idOficina"></param>
        /// <returns code="200"> Últimas rutas personalizadas de la oficina</returns>
        /// <returns code="400"> Peticion no valida</returns>
        /// <returns code="404"> Oficina no valida</returns>
        [HttpGet]
        [Route("/api/[controller]/{idOficina:int}")]
        public ActionResult<IEnumerable<CustomRouteDTO>?> GetCustomRoutes([FromRoute] int idOficina){
            
            // * get the enlace
            var oficina = sicemService.ObtenerRuta(idOficina);
            if( oficina == null){
                return NotFound( new {
                    Message = "Oficina no valida."
                });
            }

            // * get the data
            var rutas = this.sicemContext.OprRuta
                .Include( item => item.Ruta)
                .Include( item => item.OprDetRuta)
                .Where( item => item.RutaId == oficina.Id)
                .OrderByDescending( item => item.CreatedAt)
                .ToList();


            // * cast the data into DTO
            var response = new List<CustomRouteDTO>();
            response.AddRange( rutas.Select( item => CustomRouteDTO.FromModel(item) ).ToArray());
            return Ok(response);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idOficina"></param>
        /// <param name="customRouteID"></param>
        /// <returns code="404"> idOficina o customRouteID not found</returns>
        [HttpGet]
        [Route("/api/[controller]/{idOficina:int}/padron/{customRouteID:int}")]
        public ActionResult<IEnumerable<CuentaModel>> ObtenerCuentas([FromRoute] int idOficina, [FromRoute] int customRouteID ){
            
            // * get the enlace
            var oficina = sicemService.ObtenerRuta(idOficina);
            if( oficina == null){
                return NotFound( new {
                    Message = "Oficina no valida."
                });
            }

            // * get the custom-route
            var oprRoute = this.sicemContext.OprRuta
                .Where( item => item.Id == customRouteID)
                .Include( item => item.OprDetRuta)
                .FirstOrDefault();
            if(oprRoute == null){
                return NotFound( new {
                    Message = "Ruta personalizada no encontrada en esta oficina."
                });
            }

            // * get the padrob if the custom route
            var cuentasModel = this.arquosService.ObtenerPadron(oficina, oprRoute.OprDetRuta.Select( item => item.Cuenta).ToList<int>() );

            // * return the data
            return Ok( cuentasModel);

        }

    }

}
