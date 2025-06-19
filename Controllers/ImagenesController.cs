using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RutasApi.Data;
using RutasApi.Services;
using RutasApi.Models.Request;
using RutasApi.Models.Responses;
using RutasApi.Models.Entities.Sicem;


namespace RutasApi.Controllers {

    [OperadorSession]
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenesController : ControllerBase {
        private readonly ImagenesService imagenesService;
        private readonly SicemService sicemService;
        private readonly ILogger<ImagenesController> logger;

        public ImagenesController(ImagenesService i, SicemService s, ILogger<ImagenesController> l) {
            this.imagenesService = i;
            this.sicemService = s;
            this.logger = l;
        }

        /// <summary>
        /// Upload the imagen
        /// </summary>
        /// <param name="idOficina"></param>
        /// <param name="request"></param>
        /// <returns code="201"> The image was inserted</returns>
        /// <returns code="400"> The request is invalid</returns>
        /// <returns code="409"> Fail at attemp to insert the image on the database</returns>
        [HttpPost()]
        [Route("/api/[controller]/Subir/{idOficina:int}")]
        public IActionResult AlmacenarImagenes(int idOficina, [FromForm] ImagenesPadronRequest request ){

            // * get the operator
            var operador = (Operadore)(HttpContext.Items["operador"] ?? throw new Exception("Fail at get the Operador"));
            
            // * get the enlace of the office
            var oficina = sicemService.ObtenerRuta(idOficina);
            if( oficina == null){
                return BadRequest("Oficina no encontrada o no disponible.");
            }

            // * upload the image
            try {
                if(request.Imagen == null){
                    return BadRequest($"Parametro Imagen es nullo idPadron:{request.Id_Padron} Descripcion:{request.Descripcion}");
                }

                var description = string.IsNullOrEmpty(request.Descripcion)
                    ?$"Capturado por '{operador.Nombre}', operador de Ubitoma."
                    :request.Descripcion + $"\nCapturado por '{operador.Nombre}', operador de Ubitoma.";

                var result = imagenesService.AlmacenarImagenes(oficina, request.Id_Padron, request.Imagen, description );

                return result == 1
                    ?Ok()
                    :Conflict();
            }
            catch(Exception err){
                logger.LogError(err, $"Error al almacenar las imagenes de la padron: {request.Id_Padron}" );
                return BadRequest($"Error al almacener las imagenes\n\\tErr:{err.Message}\n\tStackTrace:{err.StackTrace}");
            }

            // TODO: make a record on the sicem, of the image inserted
        }


        /// <summary>
        /// return the imagens stored by ubitoma in the date range
        /// </summary>
        /// <param name="oficinaID"></param>
        /// <param name="desde">From date in format yyyyMMdd</param>
        /// <param name="hasta">To date in format yyyyMMdd</param>
        /// <returns code="400">Return the images</returns>
        /// <returns code="400">Bad request</returns>
        /// <returns code="409">Fail at attempt ot get the images</returns>
        [HttpGet()]
        [Route("/api/[controller]/{oficinaID:int}")]
        public IActionResult ObtenerImagenes([FromRoute] int oficinaID, [FromQuery] string desde, [FromQuery] string hasta ){

            // * get the enlace of the office
            var oficina = sicemService.ObtenerRuta(oficinaID);
            if( oficina == null){
                return BadRequest("Oficina no encontrada o no disponible");
            }

            // * parse dates params
            if(!DateTime.TryParseExact(desde, "yyyyMMdd", null, DateTimeStyles.None, out DateTime dateDesde)){
                return BadRequest("Fecha desde debe ser en formato yyyyMMdd");
            }
            if(!DateTime.TryParseExact(hasta, "yyyyMMdd", null, DateTimeStyles.None, out DateTime dateHasta)){
                return BadRequest("Fecha hasta debe ser en formato yyyyMMdd");
            }

            // * retrive the images
            IEnumerable<ImageInfoResponse>? images = this.imagenesService.ObtenerImagenes(oficina, dateDesde, dateHasta);
            if( images == null){
                return Conflict();
            }
            return Ok(images);
        }
    }
}
