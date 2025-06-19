using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RutasApi.Models.Entities.Sicem;

namespace RutasApi.Models
{
    public class CustomRouteDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public long OfficeId {get;set;}
        public string Office {get;set;} = null!;
        public int Accounts {get;set;}

        public static CustomRouteDTO FromModel(OprRutum oprRutum){
            var item = new CustomRouteDTO
            {
                Id = oprRutum.Id,
                Title = oprRutum.Title,
                Description = oprRutum.Description,
                CreatedAt = oprRutum.CreatedAt,
                OfficeId = oprRutum.Ruta.Id,
                Office = oprRutum.Ruta.Oficina ?? "Desconocido",
                Accounts = oprRutum.OprDetRuta.Count
            };

            return item;
        }
    }

}