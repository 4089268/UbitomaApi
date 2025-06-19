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
using RutasApi.Models.Entities.Sicem;


namespace RutasApi.Controllers{

    [OperadorSession]
    [ApiController]
    [Route("[controller]")]
    public class RutasController : ControllerBase {

        private readonly ILogger<RutasController> logger;
        private readonly SicemService sicemService;
        private readonly ArquosService arquosService;
        
        public RutasController(ILogger<RutasController> logger, SicemService s, ArquosService a){
            this.logger = logger;
            this.sicemService = s;
            this.arquosService = a;
        }


        [HttpGet]
        [Route("/api/padron/{idOficina:int}")]
        public ActionResult<ApiResponse<List<CuentaModel>>> ObtenerCuentas(int idOficina, [FromQuery] int sb = 0, [FromQuery] int se = 0, [FromQuery] int ma = 0, [FromQuery] int a = 0){
            
            var _response = new ApiResponse<List<CuentaModel>>();

            // get the enlace
            var oficina = sicemService.ObtenerRuta(idOficina);
            if( oficina == null){
                _response.Ok = 0;
                _response.Mensaje = "Oficina no encontrada o no disponible.";
                return Ok(_response);
            }
            
            // get the padron data
            var datosCuentas = arquosService.ObtenerPadron(oficina, subsistema:sb, sector:se, manzana:ma, omitirUbicados:(a==0));
            if(datosCuentas == null){
                _response.Ok = 0;
                _response.Mensaje = $"Error al obtener el padron de la oficina {oficina.Oficina}, inténtelo más tarde;";
            }else{
                _response.Ok = 1;
                _response.Datos = datosCuentas.ToList();
            }

            return Ok(_response);
            
        }


        /// <summary>
        /// Store the captured data resived
        /// </summary>
        /// <param name="idOficina"></param>
        /// <param name="request"></param>
        /// <returns code="400"> The request is not valid</returns>
        /// <returns code="201"> Data stored succesfull</returns>
        [HttpPost]
        [Route("/api/padron/actualizar/{idOficina:int}")]
        public ActionResult<ApiResponse<RutasActualizacionResponse[]>> StoreTheCapturedData( [FromRoute] int idOficina, [FromBody] CapturaRequest[] request){
            
            // * validate the request
            if( !request.Any()){
                return BadRequest(request);
            }

            // * prepare the response
            var _response = new ApiResponse<IEnumerable<RutasActualizacionResponse>>();

            // * get the operator
            var operador = (Operadore)(HttpContext.Items["operador"] ?? throw new Exception("Fail at get the Operador"));

            //* get the office
            var oficina = sicemService.ObtenerRuta(idOficina);
            if( oficina == null){
                _response.Ok = 0;
                _response.Mensaje = "Oficina no encontrada o no disponible.";
                return Ok(_response);
            }

            try {
                
                var _tmpListResponse = new List<RutasActualizacionResponse>(); 
                
                // * store the captured record on the database
                foreach(var capturaRequest in request){

                    // * attempt to save the record
                    var _actualizado = 1;
                    try {

                        // * save the bitacora-record on the sicem
                        sicemService.RegistrarHistorialActualizacion(oficina, capturaRequest, operador.Id);

                        // * save the record on the arquos-db
                        arquosService.ActualizarPadronV2(oficina, capturaRequest);
                        
                    }catch(Exception){
                        _actualizado = 0;
                    }

                    _tmpListResponse.Add( new RutasActualizacionResponse(){
                        Id_Cuenta = "0",
                        Id_Padron = capturaRequest.IdPadron.ToString(),
                        Actualizado = _actualizado,
                        Mensaje = ""
                    });
                }

                _response.Ok = 1;
                _response.Datos = _tmpListResponse;


            }catch(Exception err) {
                logger.LogError(err, "Fail at update the padron");
                _response.Mensaje = $"Error: {err.Message}";
            }
            
            return Ok(_response);
        }

    }

}
