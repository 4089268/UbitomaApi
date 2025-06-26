using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RutasApi.Data;
using RutasApi.Models.Entities.Sicem;
using RutasApi.Models.Request;
using RutasApi.Models.Responses;
using RutasApi.Services;

namespace RutasApi.Controllers
{

    [OperadorSession]
    [Route("/api/[controller]")]
    [ApiController]
    public class LoteNuevoController : ControllerBase
    {
        private readonly ILogger<LoteNuevoController> logger;
        private readonly SicemService sicemService;
        private readonly ArquosService arquosService;
        private readonly SicemContext sicemContext;

        public LoteNuevoController(ILogger<LoteNuevoController> l, SicemService s, ArquosService a, SicemContext sc)
        {
            this.logger = l;
            this.sicemService = s;
            this.arquosService = a;
            this.sicemContext = sc;
        }

        /// <summary>
        /// Store the captured data resived
        /// </summary>
        /// <param name="idOficina"></param>
        /// <param name="request"></param>
        /// <returns code="400"> The request is not valid</returns>
        /// <returns code="409">Operador u oficina no encontrados</returns>
        /// <returns code="201"> Data stored succesfull</returns>
        [HttpPost]
        [Route("{idOficina:int}")]
        public ActionResult<ApiResponse<RutasActualizacionResponse[]>> StoreTheCapturedData( [FromRoute] int idOficina, [FromBody] LoteNuevoRequest[] request)
        {
            // * validate the request
            if(!request.Any())
            {
                return BadRequest(request);
            }

            // * prepare the response
            var _response = new ApiResponse<IEnumerable<RutasActualizacionResponse>>();
            try
            {
                // * get the operator
                Operadore operador = (Operadore)(HttpContext.Items["operador"] ?? throw new ArgumentException("Fail at get the Operador", "operador"));
                logger.LogInformation("Registrando nuevo lote operador:{idOperador}-{operador}, oficina:{IdOficina}  registros:{total}", operador.Nombre, operador.Id, idOficina, request.Count());

                //* get the office
                Ruta oficina = sicemService.ObtenerRuta(idOficina) ?? throw new ArgumentException("La oficina no se encuentra o no esta disponible", "idOficina");

                var _tmpListResponse = new List<RutasActualizacionResponse>();

                // * store the captured record on the database
                sicemContext.ChangeTracker.AutoDetectChangesEnabled = false;
                foreach (LoteNuevoRequest loteNuevoRequest in request)
                {
                    // * attempt to save the record
                    var _actualizado = 1;
                    var _message = String.Empty;
                    try
                    {
                        this.sicemContext.OprLoteNuevos.Add(loteNuevoRequest.ToEntity(oficina.Id, operador.Id));
                        this.sicemContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        _actualizado = 0;
                        _message = ex.Message;
                    }

                    _tmpListResponse.Add(new RutasActualizacionResponse()
                    {
                        Id_Cuenta = "0",
                        Id_Padron = loteNuevoRequest.IdPadron.ToString(),
                        Actualizado = _actualizado,
                        Mensaje = _message
                    });
                }
                sicemContext.ChangeTracker.AutoDetectChangesEnabled = true;

                _response.Ok = 1;
                _response.Datos = _tmpListResponse;
                return Ok(_response);
            }
            catch (ArgumentException ae)
            {
                this.logger.LogError(ae, "Error al actualizar los lotes nuevos: {message}", ae.Message);
                return Conflict(new
                {
                    Title = "Error al actualizar los lotes nuevos",
                    Message = ae.Message,
                    Errors = new Dictionary<string, string> { { ae.ParamName ?? "Error", ae.Message } }
                });
            }
            catch (Exception err)
            {
                logger.LogError(err, "Fail at update the padron");
                return Conflict(new
                {
                    Title = "Error al actualizar los lotes nuevos",
                    Message = err.Message
                });
            }
        }

    }
}
