using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RutasApi.Data;
using RutasApi.Interfaces;
using RutasApi.Services;

namespace RutasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {

        private readonly ICatalogService catalogService;
        private readonly SicemService sicemService;
        
        public CatalogController(ICatalogService catalogService, SicemService sicemService){
            this.catalogService = catalogService;
            this.sicemService = sicemService;
        }

        [HttpGet]
        [Route("estatus/{idOficina:int}")]
        public IActionResult ObtenerCatalogoEstatus([FromRoute] int idOficina){
            var response = new ApiResponse<ICollection<Dictionary<string,object>>>(){ Ok = 1 };
            IEnlace? ruta = sicemService.ObtenerRuta(idOficina);
            if( ruta == null){
                response.Ok = 0;
                response.Mensaje = "Oficina no encontrada o no disponible.";
            }else{
                response.Ok = 1;
                response.Datos = this.catalogService.Estatus(ruta) ?? [];
            }
            return Ok(response);
        }
        
        [HttpGet]
        [Route("tarifas/{idOficina:int}")]
        public IActionResult ObtenerCatalogoTarifas([FromRoute] int idOficina){
            var response = new ApiResponse<ICollection<Dictionary<string,object>>>(){ Ok = 1 };
            IEnlace? ruta = sicemService.ObtenerRuta(idOficina);
            if( ruta == null){
                response.Ok = 0;
                response.Mensaje = "Oficina no encontrada o no disponible.";
            }else{
                response.Ok = 1;
                response.Datos = this.catalogService.Tarifas(ruta) ?? [];
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("situacionesToma/{idOficina:int}")]
        public IActionResult ObtenerCatalogoSituaciones([FromRoute] int idOficina){
            var response = new ApiResponse<ICollection<Dictionary<string,object>>>(){ Ok = 1 };
            IEnlace? ruta = sicemService.ObtenerRuta(idOficina);
            if( ruta == null){
                response.Ok = 0;
                response.Mensaje = "Oficina no encontrada o no disponible.";
            }else{
                response.Ok = 1;
                response.Datos = this.catalogService.SituacionesPredio(ruta) ?? [];
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("giros/{idOficina:int}")]
        public IActionResult ObtenerCatalogoGiros([FromRoute] int idOficina){
            var response = new ApiResponse<ICollection<Dictionary<string,object>>>(){ Ok = 1 };
            IEnlace? ruta = sicemService.ObtenerRuta(idOficina);
            if( ruta == null){
                response.Ok = 0;
                response.Mensaje = "Oficina no encontrada o no disponible.";
            }else{
                response.Ok = 1;
                response.Datos = this.catalogService.Giros(ruta) ?? [];
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("anomaliasPredio/{idOficina:int}")]
        public IActionResult ObtenerCatalogoAnomaliasPredio([FromRoute] int idOficina){
            var response = new ApiResponse<ICollection<Dictionary<string,object>>>(){ Ok = 1 };
            IEnlace? ruta = sicemService.ObtenerRuta(idOficina);
            if( ruta == null){
                response.Ok = 0;
                response.Mensaje = "Oficina no encontrada o no disponible.";
            }else{
                response.Ok = 1;
                response.Datos = this.catalogService.AnomaliasPredio(ruta) ?? [];
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("diametrosToma/{idOficina:int}")]
        public IActionResult ObtenerCatalogoDiametros([FromRoute] int idOficina){
            var enlace = sicemService.ObtenerRuta(idOficina);
            var response = new ApiResponse<ICollection<Dictionary<string,object>>>(){ Ok = 1 };
            response.Datos = this.catalogService.DiametrosTomas(enlace!) ?? [];
            return Ok(response);
        }

        [HttpGet]
        [Route("marcasMedidores/{idOficina:int}")]
        public IActionResult ObtenerCatalogoMarcasMedidores([FromRoute] int idOficina){
            var enlace = sicemService.ObtenerRuta(idOficina);
            var response = new ApiResponse<ICollection<Dictionary<string,object>>>(){ Ok = 1 };
            response.Datos = this.catalogService.MarcasMedidores(enlace!) ?? [];
            return Ok(response);
        }

        [HttpGet]
        [Route("tiposToma/{idOficina:int}")]
        public IActionResult ObtenerCatalogoTipoTomas([FromRoute] int idOficina){
            var enlace = sicemService.ObtenerRuta(idOficina);
            var response = new ApiResponse<ICollection<Dictionary<string,object>>>(){ Ok = 1 };
            response.Datos = this.catalogService.TiposToma(enlace!) ?? [];
            return Ok(response);
        }

        [HttpGet]
        [Route("tiposInstalacion/{idOficina:int}")]
        public IActionResult ObtenerCatalogoTipoInstalacion([FromRoute] int idOficina){
            var enlace = sicemService.ObtenerRuta(idOficina);
            var response = new ApiResponse<ICollection<Dictionary<string,object>>>(){ Ok = 1 };
            response.Datos = this.catalogService.TiposIntalacion(enlace!) ?? [];
            return Ok(response);
        }
        
        [HttpGet]
        [Route("sectoresHidraulicos/{idOficina:int}")]
        public IActionResult ObtenerCatalogoSectoresHidraulicos( [FromRoute] int idOficina){
            var enlace = sicemService.ObtenerRuta(idOficina);
            var response = new ApiResponse<ICollection<Dictionary<string,object>>>(){ Ok = 1 };
            response.Datos = this.catalogService.SectoresHidraulicos(enlace!) ?? [];
            return Ok(response);
        }


    }
}