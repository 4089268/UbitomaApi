using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RutasApi.Data;
using RutasApi.Models;
using RutasApi.Models.Request;
using RutasApi.Services;

namespace RutasApi.Controllers {
    
    [ApiController]
    public class ConsultaController : ControllerBase {
        private readonly SicemService sicemService;
        private readonly SicemContext sicemContext;
        

        public ConsultaController(SicemService s, SicemContext c){
            this.sicemService = s;
            this.sicemContext = c;
        }
        

        [HttpGet]
        [Route("api/Consulta")]
        public ActionResult<List<HistActualizacion>> Consulta([FromBody] ConsultaRequest request){
            
            // Validar request
            if(request == null){
                return BadRequest("Parametro filtro no encontrado.");
            }

            if(request.FechaDesde.Length <= 0 || request.FechaHasta.Length <= 0){
                return BadRequest("Es necesario establecer las fechas de rango.");
            }

            var _consultaRequest = new ConsultaModificaciones(request, sicemContext, sicemService);
            var _result = _consultaRequest.RealizarConsulta();
            return Ok(_result);
        }

    }
}