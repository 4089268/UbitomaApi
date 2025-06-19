using System;
using System.Collections.Generic;

namespace RutasApi.Models.Entities.Sicem
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Usuario1 { get; set; }
        public string? Contraseña { get; set; }
        public string? Opciones { get; set; }
        public string? Oficinas { get; set; }
        public bool? Administrador { get; set; }
        public bool? Inactivo { get; set; }
        public bool? CfgOfi { get; set; }
        public bool? CfgOpc { get; set; }
        public DateTime? UltimaModif { get; set; }
    }
}
