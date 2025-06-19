using System;
using System.Collections.Generic;
using System.Globalization;

namespace RutasApi.Models.Request {
    public class ConsultaRequest {
        public int[] Oficinas {get;set;}
        public int[] Padron {get;set;}
        public int[] Cuentas {get;set;}
        public int Geolocalizado {get;set;}
        public int Observaciones {get;set;}
        public int[] Estatus {get;set;}
        public int[] Situaciones {get;set;}
        public int[] Tarifas {get;set;}
        public int[] Giros {get;set;}
        public string FechaDesde {get;set;}
        public string FechaHasta {get;set;}
        
        public DateTime FechaDesdeP {
            get {
                try{
                    return DateTime.ParseExact(FechaDesde, "yyyyMMdd", new CultureInfo("es-MX"));
                }catch(Exception){
                    return new DateTime();
                }
            }
        }
        public DateTime FechaHastaP {
            get {
                try{
                    return DateTime.ParseExact(FechaHasta, "yyyyMMdd", new CultureInfo("es-MX"));
                }catch(Exception){
                    return new DateTime();
                }
            }
        }

        public ConsultaRequest(){
            Oficinas = new int[]{};
            Padron = new int[]{};
            Cuentas = new int[]{};
            Geolocalizado = 0;
            Observaciones = 0;
            Estatus = new int[]{};
            Situaciones = new int[]{};
            Tarifas = new int[]{};
            Giros = new int[]{};
            FechaDesde = "";
            FechaHasta = "";
        }

    }
}