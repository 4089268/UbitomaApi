using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RutasApi.Interfaces
{
    public interface IEnlace
    {
        public int Id { get;}
        public string Nombre { get; }
        public string ConnectionString {get;}            
        public string ConnectionStringMedia {get;}
            
    }
}