using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using RutasApi.Models.Entities.Arquos;

namespace RutasApi.Models{
    public class CuentaModel{
        public string Ruta {get;set;}
        public long IdPadron {get;set;}
        public long IdCuenta {get;set;}
        public string Medidor {get;set;}
        public string RazonSocial {get;set;}
        
        public string Latitud {get;set;}
        public string Longitud {get;set;}
        
        public int Subsistema{get;set;}
        public int Sector{get;set;}
        public int Manzana {get;set;}
        public string Lote {get;set;}
        public string Localizacion {get;set;}

        public string Direccion {get;set;}
        public string Colonia {get;set;}
        public string Calle {get;set;}
        public string CalleLateral1 {get;set;}
        public string CalleLateral2 {get;set;}
        public string NumeroExt {get;set;}
        public string NumeroInt {get;set;}

        public decimal Saldo {get;set;}
        
        public int IdSituacion{get;set;} = 0;
        public string Situacion {get;set;} = "";
        public int IdEstatus{get;set;} = 0;
        public string Estatus {get;set;} = "";
        public int IdTarifa{get;set;} = 0;
        public string Tarifa {get;set;} = "";
        public int IdGiro{get;set;} = 0;
        public string Giro {get;set;} = "";

        public string AnomaliaAct {get;set;} = "";
        public string TipoToma {get;set;} = "";
        public string TipoInstalacion {get;set;} = "";
        public string DiametroToma {get;set;} = "";
        public bool TienePozo {get;set;} = false;
        public bool EsClandestino {get;set;} = false;
        public bool DrenajeConectado {get;set;} = false;


        public CuentaModel(){
            this.Ruta = "";
            this.IdPadron = 0;
            this.IdCuenta = 0;
            this.Medidor = "";
            this.RazonSocial = "";
            this.Estatus = "";
            this.Situacion = "";
            this.Latitud = "";
            this.Longitud = "";
            this.Direccion = "";
            this.Colonia = "";
            this.Subsistema = 0;
            this.Sector = 0;
            this.Manzana = 0;
            this.Lote = "";
            this.Localizacion = "";
            this.Saldo = 0m;
            this.Tarifa = "";
            this.Calle = "";
            this.CalleLateral1 = "";
            this.CalleLateral2 = "";
            this.NumeroExt = "";
            this.NumeroInt = "";
            
        }
        public CuentaModel(VwCatPadron p){
            this.IdPadron = (long) p.IdPadron;
            this.IdCuenta = (long) p.IdCuenta;
            this.Medidor = p.IdMedidor ?? "";
            this.RazonSocial = p.RazonSocial ?? "";
            this.Latitud = (p.Latitud??0m).ToString();
            this.Longitud = (p.Longitud??0m).ToString();
            this.Direccion = p.Direccion ?? "";
            this.Colonia = p.Colonia ?? "";
            this.Subsistema = (int)(p.Sb??0);
            this.Sector = (int)(p.Sector??0);
            this.Manzana = (int)(p.Manzana??0);
            this.Lote = (p.Lote??0).ToString();
            this.Localizacion = p.Localizacion ?? "";
            this.Saldo = p.Total??0m;
            this.Ruta = string.Format("{0}-{1}-{2}", this.Subsistema, this.Sector, this.Manzana);
            this.IdSituacion = (int) (p.IdSituacion ?? 0m);
            this.Situacion = p.Situacion;
            this.IdEstatus = (int) (p.IdEstatus ?? 0m);
            this.Estatus = p.Estatus;
            this.IdTarifa = (int) (p.IdTarifa ?? 0m);
            this.Tarifa = p.Tipousuario;
            this.IdGiro = (int) (p.IdGiro ?? 0m);
            this.Giro = p.Giro ?? "";

            this.Calle = p.CallePpal ?? "";
            this.CalleLateral1 = p.CalleLat1 ?? "";
            this.CalleLateral2 = p.CalleLat2 ?? "";
            this.NumeroExt = p.NumExt ?? "";
            this.NumeroInt = p.NumInt ?? "";

            this.TienePozo = p.TienePozo ?? false;
            this.AnomaliaAct = p.AnomaliaAct ?? "";
            this.TipoToma = p.Tipotoma;
            this.TipoInstalacion = p.Tipoinstalacion;
            this.DiametroToma = p.Diametro;
        }

    }

}
