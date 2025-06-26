using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RutasApi.Data;
using RutasApi.Interfaces;
using RutasApi.Models.Entities.Sicem;
using RutasApi.Models.Request;

namespace RutasApi.Services {

    public class OperadorService {
        private readonly SicemContext sicemContext;
        private readonly ILogger<OperadorService> logger;

        public OperadorService(SicemContext c, ILogger<OperadorService> logger){
            this.sicemContext = c;
            this.logger = logger;
        }

        public Operadore? GetOperador(Guid sessionID){
            try {
                var sessionOperador = sicemContext.SesionOperadores
                    .Where(element => element.Id == sessionID)
                    // .Where(element => element.ValidTo.Date >= DateTime.Now.Date)
                    .FirstOrDefault();

                if(sessionOperador == null)
                {
                    return null;
                }
                
                return sicemContext.Operadores.FirstOrDefault( element => element.Id == sessionOperador.OperadorId );

            }catch(Exception err){
                logger.LogError(err, "Fail at attempting to get the operador by the session id:'{sessionID}'", sessionID.ToString());
                return null;
            }
        }

    }
}