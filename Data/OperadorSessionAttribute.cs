using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RutasApi.Services;

namespace RutasApi.Data {

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class OperadorSessionAttribute : Attribute, IAuthorizationFilter {

        private static readonly string headerSessionName = "session-id";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            
            // * retrive the operador service
            var operadorService = context.HttpContext.RequestServices.GetService<OperadorService>()!;


            // * Retrieve the sessionID by the header
            var sessionID = context.HttpContext.Request.Headers[headerSessionName];
            if( string.IsNullOrEmpty(sessionID)){
                context.Result = new JsonResult( new {
                    Message = "Sesion token no encontrado."
                }){
                    StatusCode = StatusCodes.Status403Forbidden
                };
                return;
            }

            // * Validate the sessionId and retrive the operador
            var operador = operadorService.GetOperador(Guid.Parse(sessionID!));
            if( operador == null){
                context.Result = new JsonResult( new {
                    Message = "Sesion token no valido."
                }){
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }else{
                context.HttpContext.Items["session_id"] = sessionID;
                context.HttpContext.Items["operador"] = operador;
            }
        }

    }

}