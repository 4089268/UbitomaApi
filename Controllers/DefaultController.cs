using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RutasApi.Data;
using RutasApi.Models;
using RutasApi.Models.Responses;
using RutasApi.Models.Request;
using RutasApi.Models.Entities.Sicem;
using RutasApi.Services;
using RutasApi.Interfaces;

namespace RutasApi.Controllers{

    [ApiController]
    [Route("[controller]")]
    public class DefaultController : ControllerBase{
        private readonly SicemService sicemService;
        private readonly ArquosService arquosService;
        
        public DefaultController(SicemService s, ArquosService a) {
            this.sicemService = s;
            this.arquosService = a;
        }


        [HttpGet]
        [Route("/")]
        public ActionResult<string> Version() {
            return "Ubitoma API version 1.5.2";
        }
        
        
        [HttpGet]
        [Route("/api/oficinas")]
        public IActionResult ObtenerCatalogoOficinas(){
            var response = new ApiResponse<ICollection<object>>(){ Ok = 1 };
            var oficinas = sicemService.ObtenerCatalogoOficinas();
            if(oficinas ==  null){
                response.Mensaje = "Error al realizar la consulta, inténtelo mas tarde.";
                response.Ok = 0;
            }else{
                response.Datos = oficinas;
            }
            return Ok(response);
        }
        
        [HttpGet]
        [Route("/api/datosEmpresa/{idOficina:int}")]
        public IActionResult ObtenerDatosEmpresaOficina(int idOficina){
            var response = new ApiResponse<DatosEmpresa>(){ Ok = 1 };
            
            var oficina = sicemService.ObtenerRuta(idOficina);
            if( oficina == null){
                response.Ok = 0;
                response.Mensaje = "Oficina no encontrada o no disponible.";
                
            }else{

                var datosE = arquosService.ObtenerDatosEmpres(oficina);
                if(datosE == null){
                    response.Ok = 0;
                    response.Mensaje = "Error al tratar de obtener los datos de la empresa, inténtelo más tarde.";                    
                }else{
                    response.Datos = datosE;
                }
            }

            return Ok(response);
        }
        
    }

}
