using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

namespace RutasApi.Models {
    class CatAnomaliaPredio
    {
        public int Id {get;set;}
        public string Descripcion {get;set;}
        public string Observaciones {get; set;}
        public CatAnomaliaPredio(){
            Id = 0;
            Descripcion = "";
            Observaciones = "";
        }

        public static CatAnomaliaPredio FromSqlDataReader(SqlDataReader reader){
            var resp = new CatAnomaliaPredio();
            resp.Id = int.TryParse(reader["id_anomalia"].ToString(), out int tmpId)?tmpId:0;
            resp.Descripcion = reader["descripcion"].ToString() ?? string.Empty;
            resp.Observaciones = reader["observaciones"].ToString() ?? string.Empty;
            return resp;
        }
    }
}