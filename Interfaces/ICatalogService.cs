using System;
using System.Collections.Generic;
using System.Linq;

using RutasApi.Models.Entities.Sicem;

namespace RutasApi.Interfaces
{
    public interface ICatalogService
    {
        public ICollection<Dictionary<string,object>>? Estatus(IEnlace ruta);
        public ICollection<Dictionary<string,object>>? Tarifas(IEnlace ruta);
        public ICollection<Dictionary<string,object>>? SituacionesPredio(IEnlace ruta);
        public ICollection<Dictionary<string,object>>? Giros(IEnlace ruta);
        public ICollection<Dictionary<string,object>>? AnomaliasPredio(IEnlace ruta);
        public ICollection<Dictionary<string,object>>? MarcasMedidores(IEnlace ruta);
        public ICollection<Dictionary<string,object>>? DiametrosTomas(IEnlace ruta);
        public ICollection<Dictionary<string,object>>? TiposToma(IEnlace ruta);
        public ICollection<Dictionary<string,object>>? TiposIntalacion(IEnlace ruta);
        public ICollection<Dictionary<string,object>>? SectoresHidraulicos(IEnlace ruta);
    }
}