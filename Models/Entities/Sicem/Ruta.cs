using System;
using System.Collections.Generic;
using RutasApi.Interfaces;

namespace RutasApi.Models.Entities.Sicem
{
    public partial class Ruta : IEnlace
    {
        public Ruta()
        {
            Operadores = new HashSet<Operadore>();
            OprActualizacions = new HashSet<OprActualizacion>();
            OprRuta = new HashSet<OprRutum>();
        }

        public int Id { get; set; }
        public string? Oficina { get; set; }
        public string? Ruta1 { get; set; }
        public bool? Inactivo { get; set; }
        public string? Servidor { get; set; }
        public string? BaseDatos { get; set; }
        public string? Usuario { get; set; }
        public string? Contraseña { get; set; }
        public short? IdZona { get; set; }
        public bool? Desconectado { get; set; }
        public bool? Alterno { get; set; }
        public string? ServidorA { get; set; }
        public string? Alias { get; set; }

        public string ConnectionString {
            get {
                var build = new System.Data.SqlClient.SqlConnectionStringBuilder();
                build.DataSource = (Alterno==true)?ServidorA:Servidor;
                build.InitialCatalog = BaseDatos;
                build.ApplicationName = "Sicem";
                build.ConnectTimeout = (int)TimeSpan.FromMinutes(10).TotalSeconds;
                build.IntegratedSecurity = false;
                build.UserID = Usuario;
                build.Password = Contraseña;
                return build.ToString();
            }
        }
        public string ConnectionStringMedia {
            get {
                var build = new System.Data.SqlClient.SqlConnectionStringBuilder();
                build.DataSource = (Alterno==true)?ServidorA:Servidor;
                build.InitialCatalog = BaseDatos + "Media";
                build.ApplicationName = "Sicem";
                build.ConnectTimeout = (int)TimeSpan.FromMinutes(10).TotalSeconds;
                build.IntegratedSecurity = false;
                build.UserID = Usuario;
                build.Password = Contraseña;
                return build.ToString();
            }
        }

        public string Nombre => this.Oficina??"";

        public virtual ICollection<OprActualizacion> OprActualizacions { get; set; }
        public virtual ICollection<OprRutum> OprRuta { get; set; }
        public virtual ICollection<Operadore> Operadores { get; set; }
    }
}
